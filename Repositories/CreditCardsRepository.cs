﻿using ApolloBank.Data;
using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using ApolloBank.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApolloBank.Repositories
{
    public class CreditCardsRepository : ICreditCardsRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly RandomNumberService _randomNumberService;
        private readonly IInvoiceRepository _invoiceRepository;


        public CreditCardsRepository(AppDbContext appDbContext, RandomNumberService randomNumberService, IInvoiceRepository invoiceRepository)
        {
            _appDbContext = appDbContext;
            _randomNumberService = randomNumberService;
            _invoiceRepository = invoiceRepository;
        }


        // Retorna os cados de limites gerais da conta
        public async Task<CreditCards> GetCreditCardsByAccountId(int accountId)
        {
            var creditCard = await _appDbContext.CreditCards.FirstOrDefaultAsync(c => c.Account_Id == accountId);

            if (creditCard == null)
            {
                throw new Exception("Dados de limites de cartões não encontrado");
            }

            return creditCard;

        }


        // Retorna um cartão pelo seu número 
        public async Task<CreditCard> GetCardByCardNumber(string cardNum)
        {
            var creditCard = await _appDbContext.CreditCard.FirstOrDefaultAsync(c => c.Number == cardNum);
            if (creditCard == null)
            {
                throw new Exception("Número de cartão de crédito não encontrado");
            }

            return creditCard;
        }


        // Retorna todos os cartoes de uma conta
        public async Task<IEnumerable<CreditCard>> GetAllCardByCardNumber(int accountId)
        {
            var creditCards = await _appDbContext.CreditCard.Where(c => c.Account_Id == accountId).ToListAsync();

            return creditCards;
        }


        // Criar novo cartão
        public async Task<CreditCard> CreateCreditCard(int accountId)
        {
            int cvc = new Random().Next(100, 999);
            string number = _randomNumberService.GenerateRandomNumberString(16);
            
            DateTime dayOneOfThisMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

            DateTime expirationTime = dayOneOfThisMonth.AddYears(3).AddMonths(4);
            double creditUsed = 0.0d;
            double creditLimit = 0.0d;

            var creditCard = new CreditCard(number, cvc, expirationTime, creditUsed, creditLimit, accountId);

            var createdCreditCard = await _appDbContext.CreditCard.AddAsync(creditCard); 

            await _appDbContext.SaveChangesAsync();

            return createdCreditCard.Entity;


        }

        
        // Bloquear um cartão
        public async Task<CreditCard> BlockCreditCard(string cardNumber)
        {
            var creditCard = await GetCardByCardNumber(cardNumber);

            if (creditCard == null || creditCard.IsBlocked != false)
            {
                throw new Exception("Cartão inexistente ou já bloqueado");
            }

            creditCard.IsBlocked = true;

            _appDbContext.CreditCard.Update(creditCard);
            await _appDbContext.SaveChangesAsync();

            return creditCard;
        }


        //Esse método apenas seta um novo valor no limite do cartão
        public async Task SetCardLimit(double newLimit, int accountId, string cardNum)
        {
            var creditCard = await GetCardByCardNumber(cardNum);
            var creditCards = await GetCreditCardsByAccountId(accountId);

            double actualCardLimit = creditCard.CreditLimit - creditCard.CreditUsed;
            double availableTotalLimit = creditCards.TotalCreditLimit - creditCards.TotalAlocatedCredit;

            double limitChange = newLimit - actualCardLimit;

            // Verificando se o novo limite é maior ou menor que o limite disponível
            if (limitChange > 0)
            {
                if (availableTotalLimit < limitChange)
                {
                    throw new Exception("Você não tem limite em conta suficiente para adicionar a esse cartão, tente diminuir o limite de outros cartões ou pagar sua fatura");
                }

                await UpdateLimits(creditCard, creditCards, newLimit, limitChange);
            }
            else
            {
                await UpdateLimits(creditCard, creditCards, newLimit, limitChange);
            }

        }


        // Método usado em SetLimit, para manter a legibilidade, foi separado
        private async Task UpdateLimits(CreditCard creditCard, CreditCards creditCards, double newLimit, double limitChange)
        {
            using (var transaction = await _appDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    creditCard.CreditLimit = newLimit;
                    creditCards.TotalAlocatedCredit += limitChange;
                    await _appDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Houve um erro interno ao alterar o limite no cartão", ex);
                }
            }
        }


        // Método que contém a lógica de adicionar uma o valor de uma transação no limite usado de crédito (será usado na transaction)
        public async Task addAmountToUsedCredit(double amount, int accountId, string cardNum)
        {
            var creditCards = await GetCreditCardsByAccountId(accountId);
            var creditCard = await GetCardByCardNumber(cardNum);

            double availableLimit = VerifyCardLimit(creditCard);

            if (availableLimit < amount)
            {
                throw new Exception("Compra reprovada: Cartão não possui limite suficiente");
            }


            using (var transaction = await _appDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    creditCards.TotalCreditUsed += amount;
                    creditCard.CreditUsed += amount;

                    _appDbContext.CreditCards.Update(creditCards);
                    _appDbContext.CreditCard.Update(creditCard);
                    await _invoiceRepository.AddAmountToInvoice(amount, accountId);

                    await _appDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Houve um erro interno ao alterar o limite no cartão", ex);
                }

            }
        }

        private double VerifyCardLimit(CreditCard creditCard)
        {
            double availableLimit = creditCard.CreditLimit - creditCard.CreditUsed;

            return availableLimit;
        }

    }
}
