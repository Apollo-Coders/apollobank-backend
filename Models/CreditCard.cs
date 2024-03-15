namespace ApolloBank.Models
{
    public class CreditCard
    {
        public int Id { get; private set; }
        public int Number { get; private set; }
        public int Cvc { get; private set; }
        public DateTime ExpirationTime { get; private set; }
        public double CreditUsed { get; private set; }
        public double CreditLimit { get; private set; }

        public int? Account_Id { get; set; }
        public Account? Account { get; set; }

        public CreditCard(int id, int number, int cvc, DateTime expirationTime, double creditUsed, double creditLimit){
            Id = id;
            Number = number;
            Cvc = cvc;
            ExpirationTime = expirationTime;
            CreditUsed = creditUsed;
            CreditLimit = creditLimit;
        }

        public void setLimit(float limit){
            CreditLimit = limit;
        }
    }
}
