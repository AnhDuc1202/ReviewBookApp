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

        public RateBook CreateOrUpdateRateBook(RateBook rateBook)
        {
            var currentRateBook = GetRateBookByIdAccAndIdBook(rateBook.ID_Acc, rateBook.ID_Book);
            if (currentRateBook != null)
            {
                currentRateBook.Rate = rateBook.Rate;
                _context.rateBooks.Update(currentRateBook);
                _context.SaveChanges();
                return currentRateBook;
            }
            _context.rateBooks.Add(rateBook);
            _context.SaveChanges();
            return rateBook;
        }

        public List<RateBook> GetAllRateBook()
        {
            return _context.rateBooks.ToList();
        }

        public double GetAllRateBookByIdBook(int idBook)
        {
            double rateAvg = 0;

            var rateBooks = _context.rateBooks.Where(c => c.ID_Book == idBook).ToList();
            double sum = 0;
            if (rateBooks.Count() == 0) return rateAvg;
            foreach (RateBook b in rateBooks)
            {
                sum += b.Rate;
            }
            return sum / rateBooks.Count();
        }

        public RateBook? GetRateBookById(int Id)
        {
            return _context.rateBooks.FirstOrDefault(c => c.ID == Id);
        }

        public RateBook? GetRateBookByIdAccAndIdBook(int IdAcc, int idBook)
        {
            return _context.rateBooks
            .FirstOrDefault(c => c.ID_Acc == IdAcc && c.ID_Book == idBook);
        }
    }
}