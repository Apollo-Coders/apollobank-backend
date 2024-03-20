using ApolloBank.DTOs;
using ApolloBank.Models;

namespace ApolloBank.Repositories.Interfaces
{
    public interface ICreditCardsRepository
    {
        Task<CreditCardDetailsDTO> CreateCreditCard(CreateCreditCardDTO createCreditCard);
        void BlockCreditCard(int cardId);

        public Task AddAmountToTotalLimit(double amount, int accountId);
    }
}
