namespace Stripboek_Project.Pages.Auth
{
    public class Author
    {
        public int id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
    }

    public class HasA
    {
        public int series_id { get; set; }
        public int author_id { get; set; }
    } 
}
