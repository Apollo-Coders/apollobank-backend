namespace ApolloBank.Models
{
    public class CreditCard
    {
        public int? Id { get; private set; }
        public bool? IsBlocked { get; private set; }
        public string Number { get; private set; }
        public int Cvc { get; private set; }
        public DateTime ExpirationTime { get; private set; }
        public double CreditUsed { get; private set; }
        public double CreditLimit { get; private set; }

        public int? Account_Id { get; set; }

        public CreditCard() { }
        public CreditCard(bool isBlocked, string number, int cvc, DateTime expirationTime, double creditUsed, double creditLimit, int account_Id)
        {
            IsBlocked = isBlocked;
            Number = number;
            Cvc = cvc;
            ExpirationTime = expirationTime;
            CreditUsed = creditUsed;
            CreditLimit = creditLimit;
            Account_Id = account_Id;
        }

        public void setLimit(float limit){
            CreditLimit = limit;
        }
    }
}
