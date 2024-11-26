namespace Workify.Models
{
    public class User
    {
       
        
            public int UserId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get; set; } // "Employer" or "JobSeeker"

            // Navigation Properties
            public Employer Employer { get; set; }
            public JobSeeker JobSeeker { get; set; }
        

    }
}
