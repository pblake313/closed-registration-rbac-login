namespace SAConstruction.DTO
{
    public class EditUserRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool JobPostings { get; set; }
        public bool AccountManagement { get; set; }
        public bool ViewCandidates { get; set; }  
    }
}