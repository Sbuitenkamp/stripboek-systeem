using System.Data;
    using MySql.Data.MySqlClient;
using Dapper;

namespace Stripboek_Project.Pages
{
    public class AuthorRepository
    {
        private IDbConnection Connect()
        {
            return new MySqlConnection(
                "Server=127.0.0.1;Port=3306;" +
                "Database=comics;" +
                "Uid=root;Pwd=;"
            );
        }

        public List<Author> Get()
        {
            using var connection = Connect();
            var authors = connection
                .Query<Author>("SELECT * FROM author");
            return authors.ToList();
        }

        public List<Author> GetAuthor(int id)
        {
            using var connection = Connect();
            var authors = connection
                .Query<Author>("SELECT * FROM author WHERE id = @id", new {id=id});
            return authors.ToList();
        }

        public void AddAuthor(string AuthorName, string AuthorType)
        {
            using var connection = Connect();
            var users = connection
                .Query($"INSERT INTO author (name, type) VALUES (@name,@type)",
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

            connection.Query("UPDATE author SET name=@name, type=@type WHERE id = @id", new {name = name, type=type, id=id});
            return;
        }

        public void DeleteAuthor(int id)
        {
            using var connection = Connect();
            var authors = connection
                .Query<Author>("DELETE FROM author WHERE id = @id", new {id = id});
        }
    }
}
