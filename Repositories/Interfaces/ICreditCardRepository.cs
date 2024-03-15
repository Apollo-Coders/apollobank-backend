using ApolloBank.Models;

namespace ApolloBank.Repositories.Interfaces
{
    public interface ICreditCardRepository
    {
        Task<CreditCard> CreateCreditCard(CreditCard creditCard);
        Task<CreditCard> BlockCreditCard(int cardId); 

        Task SetLimit(float limit);

        Task AddTransactionToInvoice(Transaction transaction);

        Task<bool> VerifyCreditLimit(int cardNumber); 

    }
}
