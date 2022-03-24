namespace Stripboek_Project.Pages.Auth;

public class Comic
{
    public int isbn { get; set; }
    public int seriesId { get; set; }
    public string title { get; set; }
    public string image { get; set; }
    public string description { get; set; }
    public bool digital { get; set; }
    
    public List<Series> series { get; set; }
}

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

public class Author
{
    public int id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    
    public List<Series> series { get; set; }
}

public class Theme
{
    public string code { get; set; }
    public string name { get; set; }
    public bool nsfw { get; set; }
    
    public List<Series> series { get; set; }
}


