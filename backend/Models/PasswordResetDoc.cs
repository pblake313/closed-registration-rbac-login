public class PasswordResetDoc
{
    public int UserId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime ExpiresAt { get; set; }

    public string ReferenceId { get; set; } = string.Empty;
}
