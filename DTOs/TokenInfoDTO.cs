namespace ApolloBank.DTOs
{
    public class TokenInfoDTO
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public int AccountId { get; set; }

        public TokenInfoDTO() { }
    }

}
