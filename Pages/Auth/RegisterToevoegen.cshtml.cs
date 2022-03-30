using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripboek_Project.Pages.Models;
using Stripboek_Project.Pages.Repositories;

namespace Stripboek_Project.Pages.Auth;

public class RegisterToevoegen : PageModel
{
    public List<Series> Series { get; set; }
    public List<Comic> Comics { get; set; }

    private RegisterRepository RegisterRepo = new RegisterRepository();

    public IActionResult OnGet()
    {
        Series = RegisterRepo.GetRegisteredSeries();
        Comics = RegisterRepo.GetRegisteredComics();
        return Page();
    }

    public IActionResult OnPostAddComic(int isbn, int series_id, string title, IFormFile file, bool digital, string description)
    {
        // image path for comics in the register are /img/register/{seriesId}/{isbn}.extension
        string imagePath = "/img/register/" + series_id + "/";
        Console.WriteLine(imagePath);
        
        Directory.CreateDirectory("./wwwroot/" + imagePath); // create dir if it doesn't exist yet for that series
        imagePath += isbn + "." + file.FileName.Split('.').Last(); // add filename + retain original extenstion
        
        Console.WriteLine(imagePath);
        
        // copy the file to the server
        using (FileStream fileStream = System.IO.File.Create("./wwwroot/" + imagePath))
        {
            file.CopyTo(fileStream);
        }
        RegisterRepo.AddComic(isbn, series_id, title, imagePath, digital, description);
        return Redirect("/Auth/RegisterToevoegen");
    }

    public IActionResult OnPostDeleteComic(int idDelete)
    {
        string imgPath = RegisterRepo.DeleteComic(idDelete);
        System.IO.File.Delete("./wwwroot/" + imgPath); // delete image to save space
        return Redirect("/Auth/RegisterToevoegen");
    }

    public IActionResult OnPostEditComic(int isbn, int series_id, string title, string image, bool digital, string description)
    {
        RegisterRepo.EditComic(isbn, series_id, title, image, digital, description);
        return Redirect("/Auth/RegisterToevoegen");
    }

    public IActionResult OnGetAllComic(int isbn)
    {
        return new JsonResult(RegisterRepo.GetSingleComic(isbn));
        
    }
}