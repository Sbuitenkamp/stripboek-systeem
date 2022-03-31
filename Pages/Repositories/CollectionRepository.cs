using Dapper;
using Stripboek_Project.Pages.Auth;
using Stripboek_Project.Pages.Models;

namespace Stripboek_Project.Pages.Repositories;

public class CollectionRepository: Repository
{
    public List<Collection> GetCollection(int id)
    {
        return Connect().Query<Collection, Comic, User, Series, Collection>("SELECT * FROM CollectionComic INNER JOIN Comic ON CollectionComic.isbn = Comic.isbn INNER JOIN User ON CollectionComic.user_id = User.id INNER JOIN Series ON Comic.series_id = Series.id WHERE CollectionComic.user_id = @id", (collection, comic, user, series) => {
            collection.comics = new List<Comic> { comic };
            collection.user = user;
            comic.series = new List<Series> { series };
            return collection;
        }, splitOn: "isbn, isbn, id, id",
        param: new { id }).ToList();
    }
    
    
}