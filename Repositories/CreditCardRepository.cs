using ApolloBank.Data;
using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApolloBank.Repositories
{
    public class CreditCardRepository: ICreditCardRepository
    {
        private readonly AppDbContext _appDbContext;

        public CreditCardRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<CreditCard?> GetCardByCardNumber(string cardNum)
        {
            var creditCard = await _appDbContext.CreditCard.FirstOrDefaultAsync(c => c.Number == cardNum);
            return creditCard;
        }

        //Esse método apenas seta um novo valor no limite do cartão
        public async Task SetLimit(double newLimit, string cardNum)
        {
            var creditCard = await _appDbContext.CreditCard.FirstOrDefaultAsync(c => c.Number == cardNum) ?? throw new Exception();

            creditCard.CreditLimit = newLimit;
            _appDbContext.CreditCard.Update(creditCard);
            await _appDbContext.SaveChangesAsync();
        }

        //Esse método vai adicionar o valor de uma transação para o crédito usado e retirar esse valor do limite
        public async Task AddAmountToLimit(double amount, string cardNum)
        {
            var creditCard = await _appDbContext.CreditCard.FirstOrDefaultAsync(c => c.Number == cardNum) ?? throw new Exception();

            creditCard.CreditUsed += amount;
            creditCard.CreditLimit -= amount;
            _appDbContext.CreditCard.Update(creditCard);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
