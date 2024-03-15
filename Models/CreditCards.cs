namespace ApolloBank.Models
{
    public class CreditCards
    {
        public int Id { get; set; }
        public double TotalCreditLimit { get; private set; }
        public double TotalCreditUsed { get; private set; }
        public double TotalAlocatedCredit { get; private set; }

        public int? Account_Id { get; set; }
        public Account? Account { get; set; }

        public CreditCards(double totalCreditLimit, double totalCreditUsed, double totalAlocatedCredit){
            TotalCreditLimit = totalCreditLimit;
            TotalCreditUsed = totalCreditUsed;
            TotalAlocatedCredit = totalAlocatedCredit;
        }
    }
    
}