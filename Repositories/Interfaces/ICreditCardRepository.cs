using ApolloBank.DTOs;
using ApolloBank.Models;

namespace ApolloBank.Repositories.Interfaces
{
    public interface ICreditCardRepository
    {
        void SetLimit(float limit);

        void AddTransactionToInvoice(Transaction transaction);

        Task<bool> VerifyCreditLimit(int cardNumber); 

    }
}
