using Microsoft.EntityFrameworkCore;
using ReviewBook.API.Data;
using ReviewBook.API.Data.Entities;
using ReviewBook.API.DTOs;

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
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        public Book_Tag? CreateBookTag(Book_Tag book_Tag)
        {
            var bt = _context.BookTags.FirstOrDefault(p => p.ID_Book == book_Tag.ID_Book && p.ID_Tag == book_Tag.ID_Tag);
            if (bt != null) return null;
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

        public Propose_NewTag CreateProposeNewTags(Propose_NewTag propose_NewTag)
        {
            _context.propose_NewTags.Add(propose_NewTag);
            _context.SaveChanges();
            return propose_NewTag;
        }

        public Propose_Tag CreateProposeTag(Propose_Tag propose_Tag)
        {
            _context.ProposeTags.Add(propose_Tag);
            _context.SaveChanges();
            return propose_Tag;
        }

        public bool DeleteBook(int ID)
        {
            var currentBook = _context.Books.FirstOrDefault(p => p.Id == ID);
            if (currentBook == null) return false;
            _context.Books.Remove(currentBook);
            _context.SaveChanges();
            return true;
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

        public bool DeleteProposeNewTags(int ID)
        {
            var p = GetProposeNewTagsById(ID);
            if (p == null) return false;
            _context.propose_NewTags.Remove(p);
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

        public List<Book> GetAllBooksByPage(int page)
        {
            int n = page - 1;
            int records = 10;
            return _context.Books
            .OrderBy(k => k.Id)
            .Include(a => a.Tags)
                .ThenInclude(a1 => a1.tag)
            .Include(b => b.author)
            .Include(c => c.publisher)
            .AsNoTracking()
            .Skip(n * records).Take(records)
            .ToList();
        }

        public List<Book_Tag> GetAllBookTagsByIdBook(int ID)
        {
            return _context.BookTags.Where(p => p.ID_Book == ID).ToList();
        }

        public List<Propose> GetAllProposes()
        {
            return _context.Proposes.Where(p => p.Status == false)
            .Include(a => a.Tags).ToList();
        }

        public List<Propose_Tag> GetAllProposeTagsByIdProppose(int ID)
        {
            return _context.ProposeTags.Where(p => p.ID_Propose == ID).ToList();
        }

        public Book? GetBookById(int ID)
        {

            return _context.Books
            .Include(a => a.Tags)
                .ThenInclude(a1 => a1.tag)
            .Include(b => b.author)
            .Include(c => c.publisher)
            .Include(d => d.reviews)
            .Include(k => k.reviews)
                .ThenInclude(e1 => e1.Account)
            .Include(k => k.reviews)
                .ThenInclude(e2 => e2.reviewChildrens)
                .ThenInclude(e21 => e21.Account)
            .AsNoTracking()
            .FirstOrDefault(p => p.Id == ID);
        }

        public Propose_Tag? GetBookProposeTagById(int ID)
        {
            return _context.ProposeTags.FirstOrDefault(p => p.ID == ID);
        }

        public Book_Tag? GetBookTagById(int ID)
        {
            return _context.BookTags.FirstOrDefault(p => p.ID == ID);
        }

        public Propose? GetProposeById(int ID)
        {
            return _context.Proposes
            .Where(p => p.Status == false)
            .Include(a => a.Tags)
                .ThenInclude(a1 => a1.tag)
            .Include(b => b.AccountRequest)
            .Include(c => c.Author)
            .Include(d => d.Publisher)
            .Include(e => e.newTags)
            .FirstOrDefault(p => p.ID == ID);
        }

        public List<Propose> GetProposeByIdUser(int ID)
        {
            return _context.Proposes
            .Where(p => p.Status == false && p.ID_Acc_Request == ID)
            .Include(a => a.Tags)
                .ThenInclude(a1 => a1.tag)
            .Include(b => b.AccountRequest)
            .Include(c => c.Author)
            .Include(d => d.Publisher)
            .ToList();
        }

        public Propose_NewTag? GetProposeNewTagsById(int ID)
        {
            return _context.propose_NewTags.FirstOrDefault(c => c.ID == ID);
        }

        public double GetRateAvgBookByIdBook(int idBook)
        {
            double rateAvg = 0;

            var rateBooks = _context.Reviews.Where(c => c.ID_Book == idBook).ToList();
            double sum = 0;
            if (rateBooks.Count() == 0) return rateAvg;
            foreach (Review b in rateBooks)
            {
                sum += b.Rate;
            }
            return sum / rateBooks.Count();
        }

        public Book? UpdateBook(Book book)
        {
            var currentBook = GetBookById(book.Id);
            if (currentBook == null) return null;
            currentBook.Name = book.Name;
            currentBook.ID_Aut = book.ID_Aut;
            currentBook.ID_Pub = book.ID_Pub;
            currentBook.Picture = book.Picture;
            currentBook.description = book.description;
            currentBook.PublishedYear = book.PublishedYear;

            _context.Books.Update(currentBook);
            _context.SaveChanges();
            return book;
        }

        public Propose? UpdatePropose(Propose propose)
        {
            var currentPropose = GetProposeById(propose.ID);
            if (currentPropose == null) return null;
            currentPropose.BookName = propose.BookName;
            currentPropose.ID_Aut = propose.ID_Aut;
            currentPropose.ID_Pub = propose.ID_Pub;
            currentPropose.Picture = propose.Picture;
            currentPropose.PublishedYear = propose.PublishedYear;
            currentPropose.Author = propose.Author;
            currentPropose.Status = propose.Status;
            currentPropose.description = propose.description;
            currentPropose.NewAut = propose.NewAut;
            currentPropose.NewPub = propose.NewPub;
            _context.Proposes.Update(currentPropose);
            _context.SaveChanges();
            return currentPropose;
        }
    }
}