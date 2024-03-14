namespace ApolloBank.Models
{
    public class CreditCards
    {
        public float TotalCreditLimit { get; private set; }
        public float TotalCreditUsed { get; private set; }
        public float TotalAlocatedCredit { get; private set; }
        public CreditCards(float totalCreditLimit, float totalCreditUsed, float totalAlocatedCredit){
            TotalCreditLimit = totalCreditLimit;
            TotalCreditUsed = totalCreditUsed;
            TotalAlocatedCredit = totalAlocatedCredit;
        }
    }
    
}