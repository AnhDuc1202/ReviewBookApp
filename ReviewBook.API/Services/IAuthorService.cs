
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public interface IAuthorService
    {
        public List<Author> GetAllAuthors();
        public Author? GetAuthorById(int ID);
        public Author CreateAuthor(Author author);
        public Author? UpdateAuthor(Author author);
        public bool DeleteAuthor(int ID);
        public int? CheckStageName(String Name);
    }
}