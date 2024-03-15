namespace ApolloBank.Models
{
    public class CreditCards
    {
        public float TotalCreditLimit { get; private set; }
        public float TotalCreditUsed { get; private set; }
        public float TotalAlocatedCredit { get; private set; }
        public Account Account { get; set; }
        public CreditCards(float totalCreditLimit, float totalCreditUsed, float totalAlocatedCredit, Account? account)
        {
            TotalCreditLimit = totalCreditLimit;
            TotalCreditUsed = totalCreditUsed;
            TotalAlocatedCredit = totalAlocatedCredit;
            Account = account;
        }

        public CreditCard? createCreditCard(CreditCard card)
        {
            Account.CreditCard.Add(card);

            return card;
        }
        public CreditCard? blockCreditCard(int cardId)
        {
            CreditCard cardToRemove = Account.CreditCard.FirstOrDefault(x => x.Id == cardId);
            if (cardToRemove != null)
            {
                Account.CreditCard.Remove(cardToRemove);
                Console.WriteLine($"Credit Card com ID {cardId} removido com sucesso.");
            }
            else
            {
                Console.WriteLine($"Credit Card com ID {cardId} não encontrado.");
            }

            return cardToRemove;
        }
    }

}