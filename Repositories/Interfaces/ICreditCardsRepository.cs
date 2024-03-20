using ApolloBank.DTOs;
using ApolloBank.Models;

namespace ApolloBank.Repositories.Interfaces
{
    public interface ICreditCardsRepository
    {
        Task<CreditCardDetailsDTO> CreateCreditCard(CreateCreditCardDTO createCreditCard);
        public Task<CreditCard> BlockCreditCard(string cardNumber);
        public Task<CreditCards> GetCreditCardsByAccountId(int accountId);
        public Task AddAmountToTotalLimit(double amount, int accountId)
        public Task SetTotalLimit(double newTotalLimit, int accountId);
    }
}
