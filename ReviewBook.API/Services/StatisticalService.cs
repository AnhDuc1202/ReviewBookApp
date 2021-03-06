using Microsoft.EntityFrameworkCore;
using ReviewBook.API.Data;
using ReviewBook.API.Data.Entities;
using ReviewBook.API.DTOs;

namespace ReviewBook.API.Services
{
    public class StatisticalService : IStatisticalService
    {
        private readonly DataContext _context;
        private readonly IBookService _bookService;

        public StatisticalService(DataContext context, IBookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }

        public List<RateStatisticalDTOs> RateStatistical(int n)
        {
            List<RateStatisticalDTOs> kq = new List<RateStatisticalDTOs>();
            var books = _context.Books.Include(a => a.author).ToList();
            foreach (Book b in books)
            {
                double avg = _bookService.GetRateAvgBookByIdBook(b.Id);
                RateStatisticalDTOs k = new RateStatisticalDTOs(b, avg);
                kq.Add(k);
            }
            List<RateStatisticalDTOs> kqcuoi = kq.OrderByDescending(o => o.RateAvg).Take(n).ToList();
            return kqcuoi;
        }
        private long CountReview(int idBook)
        {
            long count = 0;
            var reviews = _context.Reviews.Where(b => b.ID_Book == idBook).ToList();
            if (reviews.Count() == 0) return count;
            count += reviews.Count();
            foreach (Review r in reviews)
            {
                count += _context.reviewChildrens.Where(a => a.Id_parent == r.Id).ToList().Count();
            }
            return count;
        }
        public List<ReviewStatisticalDTOs> ReviewStatistical(int n)
        {
            List<ReviewStatisticalDTOs> kq = new List<ReviewStatisticalDTOs>();
            var books = _context.Books.Include(a => a.author).ToList();
            foreach (Book b in books)
            {
                long count = CountReview(b.Id);
                ReviewStatisticalDTOs k = new ReviewStatisticalDTOs(b, count);
                kq.Add(k);
            }
            List<ReviewStatisticalDTOs> kqcuoi = kq.OrderByDescending(o => o.reviewCount).Take(n).ToList();
            return kqcuoi;
        }

        public List<ReadedStatisticalDTOs> ReadedStatistical(int n)
        {
            List<ReadedStatisticalDTOs> kq = new List<ReadedStatisticalDTOs>();
            var books = _context.Books.Include(a => a.author).ToList();
            foreach (Book b in books)
            {
                long count = _context.myBooks.Where(c => c.ID_Book == b.Id && c.StatusBook == 3).Count();
                ReadedStatisticalDTOs k = new ReadedStatisticalDTOs(b, count);
                kq.Add(k);
            }
            List<ReadedStatisticalDTOs> kqcuoi = kq.OrderByDescending(o => o.readed).Take(n).ToList();
            return kqcuoi;
        }
    }
}