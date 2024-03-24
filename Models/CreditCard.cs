namespace ApolloBank.Models
{
    public class CreditCard
    {
        public int? Id { get; set; }
        public bool IsBlocked { get; set; }
        public string Number { get; set; }
        public int Cvc { get; set; }
        public DateTime ExpirationTime { get; set; }
        public double CreditUsed { get; set; }
        public double CreditLimit { get; set; }

        public int? AccountId { get; set; }
        public Account Account { get; set; }

        public CreditCard() { }
        public CreditCard(string number, int cvc, DateTime expirationTime, double creditUsed, double creditLimit, int accountId)
        {
            IsBlocked = false;
            Number = number;
            Cvc = cvc;
            ExpirationTime = expirationTime;
            CreditUsed = creditUsed;
            CreditLimit = creditLimit;
            AccountId = accountId;
        }
    }
}
