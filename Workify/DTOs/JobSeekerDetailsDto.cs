namespace Workify.DTOs
{
    public class JobSeekerDetailsDto
    {
        public int SeekerId { get; set; }
        public int UserId { get; set; }
        public string ProfileSummary { get; set; }
        public string Experience { get; set; }
        public string Skills { get; set; }

        // Include related User information
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}

