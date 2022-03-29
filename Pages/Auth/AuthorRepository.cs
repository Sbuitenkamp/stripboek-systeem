using Dapper;

namespace Stripboek_Project.Pages.Auth
{
    public class AuthorRepository: Repository
    {
        public List<Author> GetAllAuthors()
        {
            using var connection = Connect();
            var authors = connection.Query<Author>("SELECT * FROM Author");
            return authors.ToList();
        }

        public List<Author> GetAuthor(int id)
        {
            using var connection = Connect();
            var authors = connection
                .Query<Author>("SELECT * FROM Author WHERE id = @id", new {id=id});
            return authors.ToList();
        }

        public void AddAuthor(string AuthorName, string AuthorType)
        {
            using var connection = Connect();
            var users = connection
                .Query($"INSERT INTO Author (name, type) VALUES (@name,@type)",
                    new
                    {
                        name = AuthorName,
                        type = AuthorType
                    });
            return;
        }

        public void EditAuthor(int id, string name, string type)
        {
            var connection = Connect();

            connection.Query("UPDATE Author SET name=@name, type=@type WHERE id = @id", new {name = name, type=type, id=id});
            return;
        }

        public void DeleteAuthor(int id)
        {
            Connect().Query<Author>("DELETE FROM Author WHERE id = @id", new {id = id});
        }

        public void AssignAuthor(List<HasA> data)
        {
            using var connection = Connect();
            foreach (HasA authorAssign in data) connection.Query("INSERT INTO has_a (series_id, author_id) VALUES (@series_id, @author_id)", new { authorAssign.series_id, authorAssign.author_id });
        }

        public void UnassignAuthor(int seriesId)
        {
            Connect().Query("DELETE FROM has_a WHERE series_id = @seriesId", new { seriesId });
        }
    }
}
