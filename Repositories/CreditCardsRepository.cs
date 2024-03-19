﻿using ApolloBank.Data;
using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;

namespace ApolloBank.Repositories
{
    public class CreditCardsRepository : ICreditCardsRepository
    {
        private readonly AppDbContext _appDbContext;

        public CreditCardsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<CreditCardDetailsDTO> CreateCreditCard(CreateCreditCardDTO createCreditCard)
        {
            bool isBlocked = createCreditCard.IsBlocked;
            DateTime expirationTime = createCreditCard.ExpirationTime;
            double creditLimit = createCreditCard.CreditLimit;
            double creditUsed = createCreditCard.CreditUsed;
            string number = createCreditCard.Number;
            int cvc = createCreditCard.Cvc;
            int accountId = createCreditCard.AccountId;
            CreditCard newCreditCard = new CreditCard(isBlocked, number, cvc, expirationTime, creditUsed, creditLimit, accountId);

            var createdCreditCard = await _appDbContext.CreditCard.AddAsync(newCreditCard);
            await _appDbContext.SaveChangesAsync();

            return new CreditCardDetailsDTO
            {
                Id = (int)createdCreditCard.Entity.Id,
                IsBlocked = (bool)createdCreditCard.Entity.IsBlocked,
                Number = createdCreditCard.Entity.Number,
                Cvc = createdCreditCard.Entity.Cvc,
                ExpirationTime = createdCreditCard.Entity.ExpirationTime,
                CreditUsed = createdCreditCard.Entity.CreditUsed,
                CreditLimit = createdCreditCard.Entity.CreditLimit,
                AccountId = (int)createdCreditCard.Entity.Account_Id
            };
        }

        public void BlockCreditCard(int cardId/*esperar o numero do cartão*/)
        {
            throw new NotImplementedException();
        }
    }
}
