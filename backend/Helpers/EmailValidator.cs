using System.Net.Mail;

namespace SAConstruction.Helpers
{
    public static class EmailHelpers
    {
        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                // System.Net.Mail does basic RFC checking
                var addr = new MailAddress(email);

                // Extra safety: ensure original == normalized
                return addr.Address == email.Trim();
            }
            catch
            {
                return false;
            }
        }
    }
}
