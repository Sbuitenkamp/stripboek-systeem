using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripboek_Project.Pages.Repositories;

namespace Stripboek_Project.Pages;

public class IndexModel : PageModel
{
    
    public Credential Credential { get; set; }
    
    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("authed") != null)
        {
            return Redirect("/Auth/Index");
        }
        else
        {
            return Page();
        }
    }
    
    public IActionResult OnPostRegister(string username, string password, string passwordher)
    {
        var user = new UsersRepository();
        user.AddUser(username,  password);
        return new JsonResult("test");
    }
    
    public IActionResult OnPostLogin(string username, string password)
    {
        var user = new UsersRepository();
        if (ModelState.IsValid)
        {
            var res = user.Auth(username, password).ToList();
            if (res[0].auth)
            {
                HttpContext.Session.SetInt32("authed", res[0].userid);
                return new JsonResult(new {page = "/Auth/Index"});
            }
            else
            {
                return new JsonResult(new { valid=false, message="Email of password komen niet overeen."}); 
            }
        }
        else
        {
            return new JsonResult(new { valid=false, message="Vul alle velden in."});
        }

    }
}

public class Credential
{
    [Required]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Passwordher { get; set; } 
}