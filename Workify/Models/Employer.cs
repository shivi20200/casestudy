namespace Workify.Models
{
    public class Employer
    {
    
        
            public int EmployerId { get; set; }
            public int UserId { get; set; }
            public int CompanyId { get; set; }

            // Navigation Properties
            public User User { get; set; }
            public Company Company { get; set; }
            public ICollection<JobListing> JobListings { get; set; }
        

    }
}
