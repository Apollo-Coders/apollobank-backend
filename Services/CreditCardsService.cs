using ApolloBank.DTOs;
using ApolloBank.Repositories.Interfaces;
using AutoMapper;

namespace ApolloBank.Services
{
    public class CreditCardsService : ICreditCardsService
    {
        public ICreditCardsRepository _creditCardsRepository;
        private readonly IMapper _mapper;

        public CreditCardsService(ICreditCardsRepository creditCardsRepository, IMapper mapper)
        {        
            _creditCardsRepository = creditCardsRepository;
            _mapper = mapper;
        }


        public async Task<CreditCardsDetailsDTO> GetCreditCardsByAccountId(int accountId)
        {
            var creditCardsInfo = await _creditCardsRepository.GetCreditCardsByAccountId(accountId);

            return _mapper.Map<CreditCardsDetailsDTO>(creditCardsInfo);
        }


        public async Task<IEnumerable<CreditCardDetailsDTO>> GetAllCardByAccountId(int accountId)
        {
            var creditCards = await _creditCardsRepository.GetAllCardByAccountId(accountId);

            return _mapper.Map<IEnumerable<CreditCardDetailsDTO>>(creditCards); ;
        }


        public async Task<CreditCardDetailsDTO> GetCardByCardNumber(string cardNum)
        {
            var creditCard = await _creditCardsRepository.GetCardByCardNumber(cardNum);

            return _mapper.Map<CreditCardDetailsDTO>(creditCard);
        }


        public async Task<CreditCardDetailsDTO> CreateCreditCard(int accountId)
        {
           var creditCard = await _creditCardsRepository.CreateCreditCard(accountId);

            return _mapper.Map<CreditCardDetailsDTO>(creditCard);
        }


        public async Task<CreditCardDetailsDTO> BlockCreditCard(string cardNumber)
        {
            var creditCard = await _creditCardsRepository.BlockCreditCard(cardNumber);

            return _mapper.Map<CreditCardDetailsDTO>(creditCard);
        }


        public async Task SetCardLimit(double newLimit, int accountId, string cardNum)
        {
            await _creditCardsRepository.SetCardLimit(newLimit, accountId, cardNum);
        }

    }
}
