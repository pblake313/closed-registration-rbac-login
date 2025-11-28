using Mailjet.Client;
using SAConstruction.Models;
using Mailjet.Client.Resources;
using Newtonsoft.Json.Linq;

namespace SAConstruction.Services
{
    public class MailjetService
    {
        private readonly MailjetClient _client;
        private readonly string _fromEmail;
        private readonly string _fromName;
        private readonly string _frontendUrl;

        public MailjetService(IConfiguration config)
        {
            var apiKey = config["mailjet-api-key"];
            var secretKey = config["mailjet-api-secret"];

            if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(secretKey))
                throw new Exception("Mailjet API credentials missing. Add them to user-secrets.");

            _client = new MailjetClient(apiKey, secretKey);

            _fromEmail = config["mailjet-from-email"]
                ?? throw new Exception("Missing user-secret: mailjet-from-email");

            _fromName = config["mailjet-from-name"]
                ?? throw new Exception("Missing user-secret: mailjet-from-name");

            _frontendUrl = config["frontendUrl"]
                ?? throw new Exception("Missing user-secret: mailjet-from-name");
        }

        public async Task<bool> SendResetLinkEmail(SAConstruction.Models.User existingUser, string ReferenceId)
        {
            Console.WriteLine($"Found user: {existingUser.Email} -- Sending Reset Link");
            var resetLink = $"{_frontendUrl}/reset-password/{ReferenceId}";

            var html = $@"
                <h2>Password Reset</h2>
                <a href=""{resetLink}"">Reset Password</a>
            ";
            var request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, _fromEmail)
            .Property(Send.FromName, _fromName)
            .Property(Send.Subject, "Reset your password")
            .Property(Send.HtmlPart, html)
            .Property(Send.Recipients, new JArray {
                new JObject {
                    { "Email", existingUser.Email }
                }
            });

            var response = await _client.PostAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Mailjet error: {(int)response.StatusCode} {response.StatusCode} | " +
                    $"ErrorMessage: {response.GetErrorMessage()} | Body: {response.Content}"
                );
            }

            return true;
        }
    }
}
