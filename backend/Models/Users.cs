namespace SAConstruction
{
    public partial class User
    {
        public int UserId {get; set;}
        public string? Email {get; set;}
        public string? FirstName {get; set;}
        public string? LastName {get; set;}
        public string? PasswordHash {get; set;}
        public DateTime DateCreated { get; set; }
        public DateTime UpdatedAt { get; set; }

        
        public User()
        {
            // so we dont get a potential error...

            // if (FirstName == null)
            // {
            //     FirstName = "";
            // }
        }
    };

}