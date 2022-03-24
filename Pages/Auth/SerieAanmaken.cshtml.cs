using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboek_Project.Pages.Auth;

public class SerieAanmaken : PageModel
{
    public List<Series> series { get; set; }
    public IActionResult OnGet()
    {
        var x = new SerieRepository();
        series = x.GetSeries();
        var t = new Auth();
        return t.Check(HttpContext.Session.GetString("authed"));
    }

    public IActionResult OnPostSerieAanmaken(string serietitle, string seriedate, string seriebeschrijving, int serienummer)
    {
        var x = new SerieRepository();
        x.AddSerie(serietitle, seriedate, seriebeschrijving, serienummer);
        series = x.GetSeries();
        return Redirect("/auth/serieaanmaken");
    }

    public IActionResult OnPostDeleteSerie(int serieIdDelete)
    {
        var x = new SerieRepository();
        x.DeleteSerie(serieIdDelete);
        series = x.GetSeries();
        return Redirect("/auth/serieaanmaken");
    }

    public IActionResult OnGetAlleSeries(int id)
    {
        var x = new SerieRepository();
        return new JsonResult(x.GetSingleSeries(id));
    }

    public IActionResult OnPostEditSerie(int id, string title, string year, string description, int completeAmount)
    {
        var x = new SerieRepository();
        x.EditSerie(id, title, year, description, completeAmount);
        return Redirect("/auth/serieaanmaken");
    }
}