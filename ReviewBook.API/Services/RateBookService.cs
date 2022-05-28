using ReviewBook.API.Data;
using ReviewBook.API.Data.Entities;


namespace ReviewBook.API.Services
{
    public class RateBookService : IRateBookService
    {
        public readonly DataContext _context;

        public RateBookService(DataContext context)
        {
            _context = context;
        }

        public RateBook CreateRateBook(RateBook rateBook)
        {
            _context.rateBooks.Add(rateBook);
            _context.SaveChanges();
            return rateBook;
        }

        public bool DeleteRateBook(int Id)
        {
            var currentRateBook = GetRateBookById(Id);
            if (currentRateBook == null) return false;
            _context.rateBooks.Remove(currentRateBook);
            _context.SaveChanges();
            return true;
        }

        public List<RateBook> GetAllRateBook()
        {
            return _context.rateBooks.ToList();
        }

        public List<RateBook> GetAllRateBookByIdBook(int idBook)
        {
            return _context.rateBooks.Where(c => c.ID_Book == idBook).ToList();
        }

        public RateBook? GetRateBookById(int Id)
        {
            return _context.rateBooks.FirstOrDefault(c => c.ID == Id);
        }

        public RateBook? UpdateRateBook(RateBook rateBook)
        {
            var currentRateBook = GetRateBookById(rateBook.ID);
            if (currentRateBook == null) return null;
            currentRateBook.Rate = rateBook.Rate;
            _context.rateBooks.Update(currentRateBook);
            _context.SaveChanges();
            return rateBook;
        }
    }
}