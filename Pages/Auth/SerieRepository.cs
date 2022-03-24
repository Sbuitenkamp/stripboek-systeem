using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace Stripboek_Project.Pages.Auth;

public class SerieRepository
{
    private IDbConnection Connect()
    {
        return new MySqlConnection(
            "Server=127.0.0.1;Port=3306;" +
            "Database=comics;" +
            "Uid=root;Pwd=;"
        );
    }
    
    public void AddSerie(string title, string year, string description, int completeAmount)
    {
        using var connection = Connect();
        var users = connection
            .Query("INSERT INTO series (title, year, description, complete_amount) VALUES (@title,@year, @description, @complete_amount)",
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
            .Query<Series>("SELECT * FROM series");
        return series.ToList();
    }

    public void DeleteSerie(int id)
    {
        using var connection = Connect();
        var series = connection
            .Query("DELETE FROM series WHERE id = @id", new
            {
                id = id
            });
    }
    
    public List<Series> GetSingleSeries(int id)
    {
        using var connection = Connect();
        var series = connection
            .Query<Series>("SELECT * FROM series WHERE id = @id", new
            {
                id = id
            });
        return series.ToList();
    }

    public void EditSerie(int id, string title, string year, string description, int completeAmount)
    {
        using var connection = Connect();
        var series = connection
            .Query<Series>("UPDATE series SET title=@title, year=@year, description=@description, complete_amount=@completeAmount  WHERE id = @id", new
            {
                id = id,
                title = title,
                year = year,
                description = description,
                completeAmount = completeAmount
            });
    }
}