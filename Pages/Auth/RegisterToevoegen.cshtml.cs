using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Stripboek_Project.Pages.Auth;

public class RegisterToevoegen : PageModel
{
    public List<Series> series { get; set; }
    public List<Comic> comics { get; set; }

    public IActionResult OnGet()
    {
        var x = new RegisterRepository();
        series = x.GetSeries();
        comics = x.GetAllComic();
        return Page();
    }

    public IActionResult OnPostAddComic(int isbn, int series_id, string title, IFormFile file, bool digital, string description)
    {
        var x = new RegisterRepository();
        var imagePath = "/img/register/" + series_id;
        var extension = file.FileName.Split('.').Last();
        Directory.CreateDirectory(imagePath);
        imagePath += "/" + isbn + "." + extension;
        using (var fileStream = System.IO.File.Create("./wwwroot/" + imagePath))
        {
            file.CopyTo(fileStream);
        }
        x.AddComic(isbn, series_id, title, imagePath, digital, description);
        return Redirect("/Auth/RegisterToevoegen");
    }

    public IActionResult OnPostDeleteComic(int idDelete)
    {
        var x = new RegisterRepository();
        var imgPath = x.DeleteComic(idDelete);
        System.IO.File.Delete("./wwwroot/" + imgPath); // delete image to save space
        return Redirect("/Auth/RegisterToevoegen");
    }

    public IActionResult OnPostEditComic(int isbn, int series_id, string title, string image, bool digital, string description)
    {
        var x = new RegisterRepository();
        x.EditComic(isbn, series_id, title, image, digital, description);
        return Redirect("/Auth/RegisterToevoegen");
    }

    public IActionResult OnGetAllComic(int isbn)
    {
        var x = new RegisterRepository();
        return new JsonResult(x.GetSingleComic(isbn));
        
    }
}