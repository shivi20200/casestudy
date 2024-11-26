namespace Workify.DTOs
{
    public class ApplicationMinimalDto
    {
        public string SeekerName { get; set; } // Name of the job seeker
        public string JobTitle { get; set; } // Title of the job listing
        public string Resume { get; set; } // Base64 encoded resume
        public string Status { get; set; } // Application status
        public DateTime AppliedOn { get; set; } // Date and time of application
    }
}
