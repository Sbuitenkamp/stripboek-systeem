namespace Stripboek_Project.Pages.Models;

public class Series
{
        public int id { get; set; }
        public string title { get; set; }
        public DateTime year { get; set; }
        public string description { get; set; }
        public int completeAmount { get; set; }

        public Comic comics { get; set; }
        public List<Author> authors { get; set; }
        public List<Theme> themes { get; set; }
}