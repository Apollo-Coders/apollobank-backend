﻿using ApolloBank.DTOs;
using ApolloBank.Models;

namespace ApolloBank.Repositories.Interfaces
{
    public interface ICreditCardsRepository
    {
        public Task<CreditCards> GetCreditCardsByAccountId(int accountId);
        public Task<CreditCard> GetCardByCardNumber(string cardNum);

        public Task<IEnumerable<CreditCard>> GetAllCardByCardNumber(int accountId);
        public Task<CreditCard> CreateCreditCard(int accountId);
        public Task<CreditCard> BlockCreditCard(string cardNumber);
        public Task SetCardLimit(double newLimit, int accountId, string cardNum);
        public Task addAmountToUsedCredit(double amount, int accountId, string cardNum);

    }
}
