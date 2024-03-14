namespace ApolloBank.Models
{
    public class CreditCards
    {
        public float TotalCreditLimit { get; private set; }
        public float TotalCreditUsed { get; private set; }
        public float TotalAlocatedCredit { get; private set; }
        public List<CreditCard> creditCards = new List<CreditCard>();
        public CreditCards(float totalCreditLimit, float totalCreditUsed, float totalAlocatedCredit)
        {
            TotalCreditLimit = totalCreditLimit;
            TotalCreditUsed = totalCreditUsed;
            TotalAlocatedCredit = totalAlocatedCredit;
        }

        public void createCreditCard(CreditCard card)
        {
            creditCards.Add(card);
        }
        public void blockCreditCard(int cardId)
        {
            CreditCard cardToRemove = creditCards.FirstOrDefault(x => x.Id == cardId);
            if (cardToRemove != null)
            {
                creditCards.Remove(cardToRemove);
                Console.WriteLine($"Credit Card com ID {cardId} removido com sucesso.");
            }
            else
            {
                Console.WriteLine($"Credit Card com ID {cardId} não encontrado.");
            }
        }
    }

}