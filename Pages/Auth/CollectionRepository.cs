using Dapper;

namespace Stripboek_Project.Pages.Auth;

public class CollectionRepository: Repository
{
    public List<Collection> Get(int id)
    {
        using var connection = Connect();
        var collections = connection
            .Query<Collection, Comic, User, Series, Collection>("SELECT * FROM collectionComic "+
            "INNER JOIN Comic ON collectionComic.isbn = Comic.isbn "+
            "INNER JOIN user ON collectionComic.user_id = user.id "+
            "INNER JOIN series ON Comic.series_id = series.id "+
            "WHERE collectionComic.user_id = @id",
                (collection, comic, user, series) =>
                {
                    collection.comics = new List<Comic>();
                    collection.comics.Add(comic);
                    collection.user = user;
                    comic.series = new List<Series>();
                    comic.series.Add(series);
                    return collection;
                }, splitOn: "isbn, isbn, id, id",
                param: new
                {
                    id = id
                });
        return collections.ToList();
    }
    
    
}