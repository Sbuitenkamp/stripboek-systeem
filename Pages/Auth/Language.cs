namespace Stripboek_Project.Pages.Auth;

public class Language
{
    public string code { get; set; }
    public string name { get; set; }
}

public class IsWrittenIn
{
    public int series_id { get; set; }
    public string language_code { get; set; }
    
}