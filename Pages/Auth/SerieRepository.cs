using Dapper;

namespace Stripboek_Project.Pages.Auth;

public class SerieRepository: Repository
{
    public void AddSerie(string title, string year, string description, int completeAmount)
    {
        using var connection = Connect();
        var users = connection
            .Query("INSERT INTO Series (title, year, description, complete_amount) VALUES (@title,@year, @description, @complete_amount)",
                new
                {
                    title = title,
                    year = year,
                    description = description,
                    complete_amount = completeAmount
                });
        return;
    }


    public List<Series> GetSeries()
    {
        using var connection = Connect();
        var series = connection
            .Query<Series>("SELECT * FROM Series");
        return series.ToList();
    }

    public void DeleteSerie(int id)
    {
        using var connection = Connect();
        var series = connection
            .Query("DELETE FROM Series WHERE id = @id", new
            {
                id = id
            });
    }
    
    public List<Series> GetSingleSeries(int id)
    {
        using var connection = Connect();
        var series = connection
            .Query<Series>("SELECT * FROM Series WHERE id = @id", new
            {
                id = id
            });
        return series.ToList();
    }

    public void EditSerie(int id, string title, string year, string description, int completeAmount)
    {
        using var connection = Connect();
        var series = connection
            .Query<Series>("UPDATE Series SET title=@title, year=@year, description=@description, complete_amount=@completeAmount  WHERE id = @id", new
            {
                id = id,
                title = title,
                year = year,
                description = description,
                completeAmount = completeAmount
            });
    }
}