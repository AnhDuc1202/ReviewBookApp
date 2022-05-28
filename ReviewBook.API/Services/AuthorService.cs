using Microsoft.EntityFrameworkCore;
using ReviewBook.API.Data;
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly DataContext _context;

        public AuthorService(DataContext context)
        {
            _context = context;
        }

        public Author CreateAuthor(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
            return author;
        }

        public bool DeleteAuthor(int ID)
        {
            var currentAuthor = GetAuthorById(ID);
            if (currentAuthor == null) return false;
            _context.Authors.Remove(currentAuthor);
            _context.SaveChanges();
            return true;
        }

        public List<Author> GetAllAuthors()
        {
            return _context.Authors
            .Include(a => a.Books)
            .AsNoTracking()
            .ToList();
        }

        public Author? GetAuthorById(int ID)
        {
            return _context.Authors
            .Include(a => a.Books)
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == ID);
        }

        public Author? UpdateAuthor(Author author)
        {
            var currentAuthor = GetAuthorById(author.Id);
            if (currentAuthor == null) return null;
            currentAuthor.Name = author.Name;
            currentAuthor.Stage_Name = author.Stage_Name;
            currentAuthor.Description = author.Description;
            currentAuthor.Birthday = author.Birthday;
            _context.Authors.Update(currentAuthor);
            _context.SaveChanges();
            return author;
        }
    }
}