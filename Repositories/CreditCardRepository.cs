using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;

namespace ApolloBank.Repositories
{
    public class CreditCardRepository: ICreditCardRepository
    {
        public Task<CreditCard> CreateCreditCard(CreditCard creditCard)
        {
            throw new NotImplementedException();
        }

        public Task<CreditCard> BlockCreditCard(int cardId)
        {
            throw new NotImplementedException();
        }

        public Task SetLimit(float limit)
        {
            throw new NotImplementedException();
        }

        public Task AddTransactionToInvoice(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyCreditLimit(int cardNumber)
        {
            throw new NotImplementedException();
        }
    }
}
