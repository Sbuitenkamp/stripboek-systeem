using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Stripboek_Project.Pages.Auth;

public class Itemaanmaken : PageModel
{
    [Required] [BindProperty] public string AuthorName { get; set; } = string.Empty;

    [Required] [BindProperty] public string Type { get; set; } = string.Empty;

    public string Msg { get; set; } = string.Empty;

    public List<Author> Authors { get; set; } = new List<Author>();

    public IActionResult OnGet()
    {
        var author = new AuthorRepository();
        Authors = author.GetAllAuthors();

        var t = new Auth();
        return t.Check(HttpContext.Session.GetString("authed"));
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        var author = new AuthorRepository();
        author.AddAuthor(AuthorName, Type);
        Authors = author.GetAllAuthors();

        Msg = "Success";

        return Page();
    }

    public IActionResult OnGetEdit(int id)
    {
        var author = new AuthorRepository();

        return new JsonResult(author.GetAuthor(id));
    }

    public IActionResult OnPostEditAuthor(int id, string name, string type)
    {
        var author = new AuthorRepository();
    
        author.EditAuthor(id, name, type);
        return Page();
    }

    public IActionResult OnPostDelete(int id)
    {
        var author = new AuthorRepository();

        author.DeleteAuthor(id);

        return Redirect("/Auth/Itemaanmaken");
    }
}