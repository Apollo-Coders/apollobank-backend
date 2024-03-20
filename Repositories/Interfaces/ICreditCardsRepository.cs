using ApolloBank.DTOs;
using ApolloBank.Models;

namespace ApolloBank.Repositories.Interfaces
{
    public interface ICreditCardsRepository
    {
        Task<CreditCardDetailsDTO> CreateCreditCard(CreateCreditCardDTO createCreditCard);
        Task<CreditCard> BlockCreditCard(string cardNumber);
    }
}
