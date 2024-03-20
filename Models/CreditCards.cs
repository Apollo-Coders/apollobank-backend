namespace ApolloBank.Models
{
    public class CreditCards
    {
        public int? Id { get; set; }
        public double TotalCreditLimit { get; set; }
        public double TotalCreditUsed { get; set; }
        public double TotalAlocatedCredit { get; set; }
        public int? Account_Id { get; set; }
        public Account? Account { get; set; }
        public CreditCards(float totalCreditLimit, float totalCreditUsed, float totalAlocatedCredit)
        {
            TotalCreditLimit = totalCreditLimit;
            TotalCreditUsed = totalCreditUsed;
            TotalAlocatedCredit = totalAlocatedCredit;
        }

        /*public CreditCard? createCreditCard(CreditCard card)
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
        }*/
    }

}