using ApolloBank.DTOs;
using ApolloBank.Repositories.Interfaces;
using AutoMapper;

namespace ApolloBank.Services
{
    public class CreditCardService
    {
        public ICreditCardsRepository _creditCardsRepository;
        private readonly IMapper _mapper;

        public CreditCardService(ICreditCardsRepository creditCardsRepository, IMapper mapper)
        {        
            _creditCardsRepository = creditCardsRepository;
            _mapper = mapper;
        }


        public async Task<CreditCardsDetailsDTO> GetCreditCardsByAccountId(int accountId)
        {
            var creditCardsInfo = await _creditCardsRepository.GetCreditCardsByAccountId(accountId);

            return _mapper.Map<CreditCardsDetailsDTO>(creditCardsInfo);
        }


        public async Task<CreditCardDetailsDTO> GetCardByCardNumber(string cardNum)
        {
            var creditCard = await _creditCardsRepository.GetCardByCardNumber(cardNum);

            return _mapper.Map<CreditCardDetailsDTO>(creditCard);
        }


        public async Task<CreditCardDetailsDTO> CreateCreditCard(CreateCreditCardDTO creditCardDTO)
        {
           var creditCard = await _creditCardsRepository.CreateCreditCard(creditCardDTO.AccountId);

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


        public async Task addAmountToUsedCredit(TransactionDTO transactionDetails, int accountId)
        {
            string? cardNum = transactionDetails.From;

            double amount = transactionDetails.Amount;

            if (cardNum == null)
            {
                throw new Exception("Transação inválida, preencha o campo From com o número do cartão!");
            }


            await _creditCardsRepository.addAmountToUsedCredit(amount, accountId, cardNum);
        }

    }
}
