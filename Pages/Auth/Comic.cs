namespace Stripboek_Project.Pages.Auth;

public class Comic
{
    public int isbn { get; set; }
    public int series_id { get; set; }
    public string title { get; set; }
    public string image { get; set; }
    public string description { get; set; }
    public bool digital { get; set; }
    
    public List<Series> series { get; set; }
    public Collection collection { get; set; }
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

public class Collection 
{
    public int isbn { get; set; }
    public int user_id { get; set; }
    public string user_image_front { get; set; }
    public string user_image_back { get; set; }
    public float height { get; set; }
    public float thickness { get; set; }
    public float width { get; set; }
    public int rating { get; set; }
    public string quality { get; set; }
    public bool read { get; set; }
    public string place_in_shelf { get; set; }

    public List<Comic> comics { get; set; }
    public User user { get; set; }
}

public class User
{
    public int id { get; set; }
    public string username { get; set; }
    public List<Collection> collections { get; set; }
}
