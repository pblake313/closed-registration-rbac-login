namespace SAConstruction.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? PasswordHash {get; set;}

        public DateTime? DateCreated { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DateTime? LastPasswordResetEmailSentAt {get; set;}
        public DateTime? LastLogin {get; set;}
    }
}
