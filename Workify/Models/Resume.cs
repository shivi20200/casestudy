namespace Workify.Models
{
    public class Resume
    {
       
        
            public int ResumeId { get; set; }
            public int SeekerId { get; set; }
            public byte[] ResumeData { get; set; } // Store as binary
            public DateTime LastUpdated { get; set; }

            // Navigation Property
            public JobSeeker JobSeeker { get; set; }
        

    }
}
