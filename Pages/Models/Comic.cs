using Stripboek_Project.Pages.Auth;

namespace Stripboek_Project.Pages.Models;

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