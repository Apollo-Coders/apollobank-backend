using ApolloBank.Data;
using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApolloBank.Repositories
{
    public class CreditCardRepository: ICreditCardRepository
    {
        private readonly AppDbContext _appDbContext;

        public CreditCardRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void SetLimit(float limit/*esperar o numero do cartão*/)
        {
            throw new NotImplementedException();
        }

        public void AddTransactionToInvoice(Transaction transaction)
        {
            //transaction.From 
                /*AQUI VAI ESTAR O NUMERO DO CARTÃO O QUAL FOI FEITO A TRANSAÇÃO, ADICIONAR O VALOR DA TRANSACTION NA FATURA DO CARTÃO E NA FATURA GERAL*/
            throw new NotImplementedException();
        }

        public Task<bool> VerifyCreditLimit(int cardNumber)
        {
            throw new NotImplementedException();
        }
    }
}
