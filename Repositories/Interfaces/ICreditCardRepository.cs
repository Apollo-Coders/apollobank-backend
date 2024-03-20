using ApolloBank.DTOs;
using ApolloBank.Models;

namespace ApolloBank.Repositories.Interfaces
{
    public interface ICreditCardRepository
    {
        public Task<CreditCard?> GetCardByCardNumber(string cardNum);

        public Task SetLimit(double newLimit, string cardNum);

        public Task AddAmountToLimit(double amount, string cardN);

    }
}
