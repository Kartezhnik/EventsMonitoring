namespace Common.Models.Entities
{
    public class Tokens
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; } 
    }

}
