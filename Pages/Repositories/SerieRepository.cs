using Dapper;
using Stripboek_Project.Pages.Auth;
using Stripboek_Project.Pages.Models;

namespace Stripboek_Project.Pages.Repositories;

public class SerieRepository: Repository
{
    public int AddSeries(string title, string year, string description, int completeAmount)
    {
        return Connect().Query<int>("INSERT INTO Series (title, year, description, complete_amount) VALUES (@title,@year, @description, @complete_amount); SELECT LAST_INSERT_ID();",new 
        {
            title,
            year,
            description,
            complete_amount = completeAmount
        }).Single(); // return the newly inserted ID
    }


    public List<Series> GetAllSeries()
    {
        return Connect().Query<Series>("SELECT * FROM Series").ToList();
    }

    public void DeleteSeries(int id)
    {
        Connect().Query("DELETE FROM Series WHERE id = @id", new { id });
    }
    
    public List<Series> GetSingleSeries(int id)
    {
        return Connect().Query<Series>("SELECT * FROM Series WHERE id = @id", new { id }).ToList();
    }

    public void EditSeries(int id, string title, string year, string description, int completeAmount)
    {
        Connect().Query<Series>("UPDATE Series SET title=@title, year=@year, description=@description, complete_amount=@completeAmount  WHERE id = @id", new
        {
            id,
            title,
            year,
            description,
            completeAmount
        });
    }
}