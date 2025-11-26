using System.Security.Cryptography;
using System.Text.Json;

namespace SAConstruction.Services
{
    public class StringService
    {
        public string GetRandomString()
        {
            int number = RandomNumberGenerator.GetInt32(0, 1000000);
            return number.ToString("D6");
        }

        public string GetMixedString(JsonElement body)
        {
            string input;

            // If there is a property "customString" AND it's a string, use it
            if (body.ValueKind == JsonValueKind.Object &&
                body.TryGetProperty("customString", out JsonElement customStringElement) &&
                customStringElement.ValueKind == JsonValueKind.String)
            {
                input = customStringElement.GetString() ?? "no custom string";
            }
            else
            {
                input = "no custom string";
            }

            return $"{input} - my string";
        }
    }
}
