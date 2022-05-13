using ReviewBook.API.Data;
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public class BookService : IBookService
    {
        private readonly DataContext _context;

        public BookService(DataContext context)
        {
            _context = context;
        }

        public Book CreateBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Book_Tag CreateBookTag(Book_Tag book_Tag)
        {
            _context.BookTags.Add(book_Tag);
            _context.SaveChanges();
            return book_Tag;
        }

        public Propose CreatePropose(Propose propose)
        {
            _context.Proposes.Add(propose);
            _context.SaveChanges();
            return propose;
        }

        public Propose_Tag CreateProposeTag(Propose_Tag propose_Tag)
        {
            _context.ProposeTags.Add(propose_Tag);
            _context.SaveChanges();
            return propose_Tag;
        }

        public bool DeleteBook(int ID)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBookTag(int ID)
        {
            var currentBookTag = _context.BookTags.FirstOrDefault(p => p.ID == ID);
            if (currentBookTag == null) return false;
            _context.BookTags.Remove(currentBookTag);
            _context.SaveChanges();
            return true;
        }

        public bool DeletePropose(int ID)
        {
            var currentPropose = GetProposeById(ID);
            if (currentPropose == null) return false;
            _context.Proposes.Remove(currentPropose);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteProposeTag(int ID)
        {
            var currentProposeTag = _context.ProposeTags.FirstOrDefault(p => p.ID == ID);
            if (currentProposeTag == null) return false;
            _context.ProposeTags.Remove(currentProposeTag);
            _context.SaveChanges();
            return true;
        }

        public List<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public List<Book_Tag> GetAllBookTags()
        {
            throw new NotImplementedException();
        }

        public List<Propose> GetAllProposes()
        {
            return _context.Proposes.ToList();
        }

        public List<Propose_Tag> GetAllProposeTags()
        {
            throw new NotImplementedException();
        }

        public Book? GetBookById(int ID)
        {
            throw new NotImplementedException();
        }

        public Propose_Tag? GetBookProposeTagById(int ID)
        {
            throw new NotImplementedException();
        }

        public Book_Tag? GetBookTagById(int ID)
        {
            throw new NotImplementedException();
        }

        public Propose? GetProposeById(int ID)
        {
            throw new NotImplementedException();
        }

        public Book? UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Propose? UpdatePropose(Propose propose)
        {
            throw new NotImplementedException();
        }
    }
}