namespace Stripboek_Project.Pages.Models;

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