namespace SAConstruction.DTO
{
    public class CreateUserRequest
    {
        // User info
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Permissions info
        public bool JobPostings { get; set; }
        public bool AccountManagement { get; set; }
        public bool ViewCandidates { get; set; }  
    }
}