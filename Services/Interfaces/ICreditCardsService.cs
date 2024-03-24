using ApolloBank.DTOs;

namespace ApolloBank.Services
{
    public interface ICreditCardsService
    {
        public Task<CreditCardsDetailsDTO> GetCreditCardsByAccountId(int accountId);
        public Task<CreditCardDetailsDTO> GetCardByCardNumber(string cardNum);
        public Task<IEnumerable<CreditCardDetailsDTO>> GetAllCardByAccountId(int accountId);
        public Task<CreditCardDetailsDTO> CreateCreditCard(int accountId);
        public Task<CreditCardDetailsDTO> BlockCreditCard(string cardNumber);
        public Task SetCardLimit(double newLimit, int accountId, string cardNum);
    }
}
