using ApolloBank.Data;
using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApolloBank.Repositories
{
    public class CreditCardsRepository : ICreditCardsRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper; 


        public CreditCardsRepository(
            AppDbContext appDbContext,
            IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        


        public async Task<CreditCardDetailsDTO> CreateCreditCardd(CreateCreditCardDTO createcreditCard)
        {

            CreditCard creditCard = _mapper.Map<CreditCard>(createcreditCard);

            await _appDbContext.CreditCard.AddAsync(creditCard); 

            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<CreditCardDetailsDTO>(creditCard);


        }



        public async Task<CreditCardDetailsDTO> CreateCreditCard(CreateCreditCardDTO createCreditCard)
        {
            bool isBlocked = createCreditCard.IsBlocked;
            DateTime expirationTime = createCreditCard.ExpirationTime;
            double creditLimit = createCreditCard.CreditLimit;
            double creditUsed = createCreditCard.CreditUsed;
            string number = createCreditCard.Number;
            int cvc = createCreditCard.Cvc;
            int accountId = createCreditCard.AccountId;
            CreditCard newCreditCard = new CreditCard(isBlocked, number, cvc, expirationTime, creditUsed, creditLimit, accountId);

            var createdCreditCard = await _appDbContext.CreditCard.AddAsync(newCreditCard);
            await _appDbContext.SaveChangesAsync();

            return new CreditCardDetailsDTO
            {
                Id = (int)createdCreditCard.Entity.Id,
                IsBlocked = (bool)createdCreditCard.Entity.IsBlocked,
                Number = createdCreditCard.Entity.Number,
                Cvc = createdCreditCard.Entity.Cvc,
                ExpirationTime = createdCreditCard.Entity.ExpirationTime,
                CreditUsed = createdCreditCard.Entity.CreditUsed,
                CreditLimit = createdCreditCard.Entity.CreditLimit,
                AccountId = (int)createdCreditCard.Entity.Account_Id
            };
        }











        public async Task<CreditCard> BlockCreditCard(string cardNumber)
        {
            var creditCard = await _appDbContext.CreditCard.FirstOrDefaultAsync(c => c.Number.Equals(cardNumber));

            if (creditCard == null || creditCard.IsBlocked != false)
            {
                throw new Exception("Inexistent or already blocked credit card");
            }

            creditCard.IsBlocked = true;

            _appDbContext.CreditCard.Update(creditCard);
            await _appDbContext.SaveChangesAsync();

            return creditCard;
        }
    }
}
