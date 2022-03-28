using Dapper;

namespace Stripboek_Project.Pages
{
    public class AuthorRepository: Repository
    {
        public List<Author> Get()
        {
            using var connection = Connect();
            var authors = connection
                .Query<Author>("SELECT * FROM Author");
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
            using var connection = Connect();
            var authors = connection
                .Query<Author>("DELETE FROM Author WHERE id = @id", new {id = id});
        }
    }
}
