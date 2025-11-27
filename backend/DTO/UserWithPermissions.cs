namespace SAConstruction.DTO
{
    public class UserWithPermissionsDto
    {
        public int UserId { get; set; }

        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? PasswordHash {get; set;}

        public DateTime? DateCreated { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public bool JobPostings {get; set;}
        public bool AccountManagement {get; set;}
        public bool ViewCandidates {get; set;}
    }
}
