

namespace Workify.DTOs
 
{
    public class ApplicationDto
    {
        public int SeekerId { get; set; }
        public int JobId { get; set; }
        public byte[] Resume { get; set; } // Store resume as binary data
        public string Status { get; set; } = "Applied"; // Default status
    }
}
