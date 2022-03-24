﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboek_Project.Pages.Auth;

public class Overzicht : PageModel
{
    public List<Collection> collection { get; set; }
    public IActionResult OnGet(int id)
    {
        var x = new CollectionRepository();

        collection = x.Get(id);
        var t = new Auth();
        return t.Check(HttpContext.Session.GetString("authed"));
    }
}