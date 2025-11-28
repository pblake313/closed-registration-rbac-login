namespace SAConstruction.Helpers
{
    public static class PasswordHelpers
    {
        public static bool IsValidPassword(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // Reject spaces outright
            if (password.Contains(' '))
                return false;

            bool hasCapital = password.Any(char.IsUpper);
            bool hasNumber = password.Any(char.IsDigit);
            bool longEnough = password.Length >= 9;

            // Special character = NOT letter or digit
            bool hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasCapital && hasNumber && longEnough && hasSpecial;
        }
    }
}
