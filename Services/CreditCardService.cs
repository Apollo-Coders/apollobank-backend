using ApolloBank.DTOs;
using ApolloBank.Models;
using ApolloBank.Repositories;
using ApolloBank.Repositories.Interfaces;

namespace ApolloBank.Services
{
    public class CreditCardService
    {
        public ICreditCardRepository _creditCardRepository;
        public ICreditCardsRepository _creditCardsRepository;
        public IAccountRepository _accountRepository;
        public IInvoiceRepository _invoiceRepository;

        public CreditCardService(ICreditCardRepository creditCardRepository, IAccountRepository accountRepository, ICreditCardsRepository creditCardsRepository, IInvoiceRepository invoiceRepository)
        {
            _creditCardRepository = creditCardRepository;
            _accountRepository = accountRepository;
            _creditCardsRepository = creditCardsRepository;
            _invoiceRepository = invoiceRepository;
        }

        public async Task SetCardLimit(int accountNum, double newLimit, string cardNum)
        {
            var creditCard = await _creditCardRepository.GetCardByCardNumber(cardNum) ?? throw new Exception();
            var creditCards = new CreditCards(3232, 5121, 3232); /*await _creditCardsRepository.GetCreditById() ?? throw new Exception();*/

            double actualCardLimit = creditCard.CreditLimit - creditCard.CreditUsed;
            double actualTotalLimit = creditCards.TotalCreditLimit - creditCards.TotalAlocatedCredit;


            if (newLimit > actualCardLimit)
            {
                double availableLimit = newLimit - actualCardLimit;

                //Verificar se a conta tem esse novo limite livre
                if (actualTotalLimit > availableLimit)
                {
                    double newTotalLimit = actualTotalLimit - availableLimit;
                    await _creditCardRepository.SetLimit(newLimit, cardNum);
                    await _creditCardsRepository.SetTotalLimit(newTotalLimit, accountNum);
                    /*diminuir esse valor do limite total do creditCard*/
                }
                else
                {
                    throw new Exception();
                }

            } else
            {
                double availableLimit = actualCardLimit - newLimit;

                double newTotalLimit = actualTotalLimit + availableLimit;
                await _creditCardsRepository.SetTotalLimit(newTotalLimit, accountNum);
                /*aumentar esse valor do limite total do creditCard*/
            }
        }

        public async Task<double> VerifyCardLimit(string cardNum)
        {
            var creditCard = await _creditCardRepository.GetCardByCardNumber(cardNum) ?? throw new Exception();

            double availableLimit = creditCard.CreditLimit - creditCard.CreditUsed;

            return availableLimit;
        }

        public async Task AddTransactionToCreditCard(TransactionDTO transaction)
        {
            string? cardNum = transaction.From;

            if (cardNum == null) {
                throw new Exception("Transação invalida, preencha o campo From com o número do cartão!");
            }

            double amount = transaction.Amount;

            double availableLimit = await VerifyCardLimit(cardNum);

            if (availableLimit < amount)
            {
                throw new Exception("Compra reprovada: Cartão não possui limite suficiente");
            }

            await _creditCardRepository.AddAmountToLimit(amount, cardNum);
            await _creditCardsRepository.AddAmountToTotalLimit(amount, transaction.Account_Id);
            await _invoiceRepository.AddAmountToInvoice(amount, transaction.Account_Id);
        }
    }
}
