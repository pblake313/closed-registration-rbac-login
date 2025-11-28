namespace SAConstruction.DTO
{
    public class ResetPasswordRequest
    {
        public string? Email {get; set;}
    }

    public class NewPasswordRequest
    {
        public string NewPassword {get; set;} = string.Empty;
        public string ConfirmPassword {get; set;} = string.Empty;

        public string ReferenceId {get; set;} = string.Empty;
    }

}