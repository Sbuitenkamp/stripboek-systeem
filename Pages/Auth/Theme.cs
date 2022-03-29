namespace Stripboek_Project.Pages.Auth;

public class Theme
{
    public string code { get; set; }
    public string name { get; set; }
    public bool nsfw { get; set; }
}

public class ThemedAfter
{
    public int series_id { get; set; }
    public string theme_code { get; set; }
}