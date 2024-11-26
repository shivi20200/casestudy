using static System.Net.Mime.MediaTypeNames;

namespace Workify.Models
{
    public class JobListing
    {


        public int JobId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Qualifications { get; set; }
        public decimal Salary { get; set; }
        public string JobType { get; set; }
        public string ReqSkills { get; set; }
        public int EmployerId { get; set; }
        public string Location { get; set; }
        public string Industry { get; set; }


        // Navigation Properties
        public Employer Employer { get; set; }
      
        public ICollection<Application> Applications { get; set; }


    }
}
