namespace Workify.Models
{
    public class SearchHistory
    {
       
            public int SearchId { get; set; }
            public int SeekerId { get; set; }
            public string SearchCriteria { get; set; }
            public DateTime SearchDate { get; set; }

            // Navigation Property
            public JobSeeker JobSeeker { get; set; }
        

    }
}
