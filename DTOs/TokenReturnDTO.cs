
namespace ApolloBank.DTOs
{
    public class TokenReturnDTO
    {
        public string token { get; set; }
        public string userName { get; set; }
        public double balance { get; set; }
        public int accountNumber { get; set; }
        public int accountId { get; set; }

        public TokenReturnDTO(string token, string userName, double balance, int accountNumber, int accountId)
        {
            this.token = token;
            this.userName = userName;
            this.balance = balance;
            this.accountNumber = accountNumber;
            this.accountId = accountId;
        }

        public TokenReturnDTO()
        {
        }
    }
}