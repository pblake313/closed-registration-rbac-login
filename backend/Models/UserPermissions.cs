namespace SAConstruction
{
    public partial class UserPermissions
    {
        
        public int UserId {get; set;}

        public bool JobPostings {get; set;}
        public bool AccountManagement {get; set;}
        public bool ViewCandidates {get; set;}

        
        public UserPermissions()
        {
            // so we dont get a potential error...
            
            // if (FirstName == null)
            // {
            //     FirstName = "";
            // }
        }
    };

}