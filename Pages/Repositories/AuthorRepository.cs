using Dapper;
using Stripboek_Project.Pages.Models;

namespace Stripboek_Project.Pages.Repositories
{
    public class AuthorRepository: Repository
    {
        public List<Author> GetAllAuthors()
        {
            return Connect().Query<Author>("SELECT * FROM Author").ToList();
        }

        public List<Author> GetAuthor(int id)
        {
            return Connect().Query<Author>("SELECT * FROM Author WHERE id = @id", new { id }).ToList();
        }

        public void AddAuthor(string AuthorName, string AuthorType)
        {
            Connect().Query($"INSERT INTO Author (name, type) VALUES (@name,@type)",new { name = AuthorName, type = AuthorType });
        }

        public void EditAuthor(int id, string name, string type)
        {
            Connect().Query("UPDATE Author SET name=@name, type=@type WHERE id = @id", new { name, type, id});
        }

        public void DeleteAuthor(int id)
        {
            Connect().Query<Author>("DELETE FROM Author WHERE id = @id", new { id });
        }

        public void AssignAuthor(List<HasA> data)
        {
            using var connection = Connect();
            // create relations using the connection table has_a
            foreach (HasA authorAssign in data) connection.Query("INSERT INTO has_a (series_id, author_id) VALUES (@series_id, @author_id)", new { authorAssign.series_id, authorAssign.author_id });
        }

        public void UnassignAuthor(int seriesId)
        {
            // remove relations
            Connect().Query("DELETE FROM has_a WHERE series_id = @seriesId", new { seriesId });
        }
    }
}
