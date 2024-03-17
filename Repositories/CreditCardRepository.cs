using ApolloBank.Data;
using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Repositories
{
    public class CreditCardRepository: ICreditCardRepository
    {
        private readonly AppDbContext _appDbContext;

        public CreditCardRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public Task<CreditCard> CreateCreditCard(CreateCreditCardDTO createCreditCard)
        {
            DateTime expirationDate = DateTime.UtcNow; //corrigir
            double creditLimit = 0;
            double creditUsed = 0;
            string number = "31313013130919";
            int cvc = 231;
            CreditCard newCreditCard = new CreditCard(number, cvc, expirationDate, creditUsed, creditLimit, createCreditCard.AccountId);

            var creditCard = _appDbContext.CreditCard.AddAsync(newCreditCard);
            _appDbContext.SaveChanges();

            return creditCard //retornar a entidade criada
        }

        public Task<CreditCard> BlockCreditCard(/*esperar o numero do cartão*/)
        {
            throw new NotImplementedException();
        }
        
        public Task SetLimit(/*esperar o numero do cartão*/)
        {
            throw new NotImplementedException();
        }

        public Task AddTransactionToInvoice(Transaction transaction)
        {
            transaction.From 
                /*AQUI VAI ESTAR O NUMERO DO CARTÃO O QUAL FOI FEITO A TRANSAÇÃO, ADICIONAR O VALOR DA TRANSACTION NA FATURA DO CARTÃO E NA FATURA GERAL*/
            throw new NotImplementedException();
        }

        public Task<bool> VerifyCreditLimit(int cardNumber)
        {
            throw new NotImplementedException();
        }
    }
}
