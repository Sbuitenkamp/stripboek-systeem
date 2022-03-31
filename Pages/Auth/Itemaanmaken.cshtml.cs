using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Stripboek_Project.Pages.Models;
using Stripboek_Project.Pages.Repositories;

namespace Stripboek_Project.Pages.Auth;

public class Itemaanmaken : PageModel
{
    [Required] 
    [BindProperty] 
    public string AuthorName { get; set; } = string.Empty;

    [Required] 
    [BindProperty] 
    public string Type { get; set; } = string.Empty;

    public string Msg { get; set; } = string.Empty;

    public List<Author> Authors { get; set; } = new List<Author>();

    private AuthorRepository AuthorRepo = new AuthorRepository();

    public IActionResult OnGet()
    {
        Authors = AuthorRepo.GetAllAuthors();

        Models.Auth auth = new Models.Auth();
        return auth.Check(HttpContext.Session.GetString("authed"));
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        AuthorRepo.AddAuthor(AuthorName, Type);
        Authors = AuthorRepo.GetAllAuthors();

        Msg = "Success";

        return Page();
    }

    public IActionResult OnGetEdit(int id)
    {

        return new JsonResult(AuthorRepo.GetAuthor(id));
    }

    public IActionResult OnPostEditAuthor(int id, string name, string type)
    {
        AuthorRepo.EditAuthor(id, name, type);
        return Page();
    }

    public IActionResult OnPostDelete(int id)
    {
        AuthorRepo.DeleteAuthor(id);
        return Redirect("/Auth/Itemaanmaken");
    }
}