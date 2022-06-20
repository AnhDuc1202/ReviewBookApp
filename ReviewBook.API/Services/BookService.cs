using Microsoft.EntityFrameworkCore;
using ReviewBook.API.Data;
using ReviewBook.API.Data.Entities;
using ReviewBook.API.DTOs;

namespace ReviewBook.API.Services
{
    public class BookService : IBookService
    {
        private readonly DataContext _context;
        private readonly IAuthorService _authorService;
        private readonly IPublisherService _publisherService;

        public BookService(DataContext context, IAuthorService authorService, IPublisherService publisherService)
        {
            _context = context;
            _authorService = authorService;
            _publisherService = publisherService;
        }
        public int? AddBookFromPropose(int Idpropose)
        {
            var propose = _context.Proposes.Where(p => p.Status == false)
                        .Include(a => a.Tags)
                        .Include(c => c.Author)
                        .Include(d => d.Publisher)
                        .FirstOrDefault(p => p.ID == Idpropose);
            if (propose == null) return null;
            if (String.IsNullOrEmpty(propose.BookName)) return -1;
            if (CheckName(propose.BookName) != null)
                return -2; // tên sách đã tồn tại

            Book book = new Book();
            book.Name = propose.BookName;
            book.PublishedYear = propose.PublishedYear;
            book.Picture = propose.Picture;
            book.description = propose.description;

            if (propose.Author != null)
                book.ID_Aut = propose.Author.Id;
            else
            {
                if (String.IsNullOrEmpty(propose.NewAut)) return -3;
                var aut = _authorService.CheckStageName(propose.NewAut);
                if (aut == null)
                {
                    Author a = new Author();
                    a.Stage_Name = propose.NewAut;
                    book.ID_Aut = _authorService.CreateAuthor(a).Id;
                }
                else book.ID_Aut = Int32.Parse(aut.ToString());
            }
            if (propose.Publisher != null)
                book.ID_Pub = propose.Publisher.ID;
            else
            {
                if (String.IsNullOrEmpty(propose.NewPub)) return -4;
                var pub = _publisherService.CheckName(propose.NewPub);
                if (pub == null)
                {
                    Publisher a = new Publisher();
                    a.Name = propose.NewPub;
                    book.ID_Pub = _publisherService.CreatePublisher(a).ID;
                }
                else book.ID_Pub = Int32.Parse(pub.ToString());
            }
            var newBook = CreateBook(book);
            var tags = propose.Tags;
            foreach (var tag in tags)
            {
                Book_Tag k = new Book_Tag();
                k.ID_Book = newBook.Id;
                k.ID_Tag = tag.ID_Tag;
                var a = CreateBookTag(k);
            }
            DeletePropose(propose.ID);
            return newBook.Id;
        }

        public int? CheckName(string Name)
        {
            var currentBook = _context.Books.FirstOrDefault(c => c.Name.Trim().ToLower() == Name.Trim().ToLower());
            if (currentBook == null) return null;
            return currentBook.Id;
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
        public List<ProposeBasicDTOs> GetAllProposes()
        {
            List<ProposeBasicDTOs> kq = new List<ProposeBasicDTOs>();
            var Proposes = _context.Proposes.Include(a => a.Author).Include(a => a.Publisher).ToList();
            foreach (var p in Proposes)
            {
                ProposeBasicDTOs k = new ProposeBasicDTOs();
                k.Id = p.ID;
                k.BookName = p.BookName;
                if (p.Author == null) k.NewAut = p.NewAut;
                else k.NewAut = p.Author.Stage_Name;
                if (p.Publisher == null) k.NewPub = p.NewPub;
                else k.NewPub = p.Publisher.Name;
                kq.Add(k);
            }
            return kq;
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
            var currentBook = _context.Books.FirstOrDefault(c => c.Id == book.Id);
            if (currentBook == null) return null;
            currentBook.Name = book.Name;
            currentBook.ID_Aut = book.ID_Aut;
            currentBook.ID_Pub = book.ID_Pub;
            currentBook.Picture = book.Picture;
            currentBook.description = book.description;
            currentBook.PublishedYear = book.PublishedYear;

            _context.Books.Update(currentBook);
            _context.SaveChanges();
            return currentBook;
        }

    }
}