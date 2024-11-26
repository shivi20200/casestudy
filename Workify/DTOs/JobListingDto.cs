namespace Workify.DTOs
{
    public class JobListingDto
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
    }
}

