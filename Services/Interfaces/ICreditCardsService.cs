﻿using ApolloBank.DTOs;

namespace ApolloBank.Services
{
    public interface ICreditCardsService
    {
        public Task<CreditCardsDetailsDTO> GetCreditCardsByAccountId(int accountId);
        public Task<CreditCardDetailsDTO> GetCardByCardNumber(string cardNum);
        public Task<IEnumerable<CreditCardDetailsDTO>> GetAllCardByAccountId(int accountId);
        public Task<CreditCardDetailsDTO> CreateCreditCard(CreateCreditCardDTO creditCardDTO);
        public Task<CreditCardDetailsDTO> BlockCreditCard(string cardNumber);
        public Task SetCardLimit(double newLimit, int accountId, string cardNum);
        public Task AddAmountToUsedCredit(TransactionDTO transactionDetails, int accountId);
    }
}
