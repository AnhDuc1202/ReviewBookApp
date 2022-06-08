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

        public int? CheckName(string Name)
        {
            var check = _context.Authors.FirstOrDefault(c => c.Name.Trim().ToLower() == Name.Trim().ToLower());
            if (check == null) return null;
            return check.Id;
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
            .AsNoTracking()
            .ToList();
        }

        public Author? GetAuthorById(int ID)
        {
            return _context.Authors
            .Include(a => a.Books)
                .ThenInclude(b => b.publisher)
            .Include(a => a.Books)
                .ThenInclude(c => c.Tags)
                    .ThenInclude(d => d.tag)
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == ID);
        }

        public Author? UpdateAuthor(Author author)
        {
            var currentAuthor = _context.Authors.FirstOrDefault(c => c.Id == author.Id);
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