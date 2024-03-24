namespace ApolloBank.Models
{
    public class CreditCards
    {
        public int? Id { get; set; }
        public double TotalCreditLimit { get; set; }
        public double TotalCreditUsed { get; set; }
        public double TotalAlocatedCredit { get; set; }
        public int? AccountId { get; set; }
        public Account? Account { get; set; }


    }

}