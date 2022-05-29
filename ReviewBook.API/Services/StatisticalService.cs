using Microsoft.EntityFrameworkCore;
using ReviewBook.API.Data;
using ReviewBook.API.Data.Entities;
using ReviewBook.API.DTOs;

namespace ReviewBook.API.Services
{
    public class StatisticalService : IStatisticalService
    {
        private readonly DataContext _context;
        private readonly IRateBookService _rateBookService;

        public StatisticalService(DataContext context, IRateBookService rateBookService)
        {
            _context = context;
            _rateBookService = rateBookService;
        }

        public List<RateStatisticalDTOs> RateStatistical(int n)
        {
            List<RateStatisticalDTOs> kq = new List<RateStatisticalDTOs>();
            var books = _context.Books.ToList();
            foreach (Book b in books)
            {
                double avg = _rateBookService.GetAllRateBookByIdBook(b.Id);
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
            var books = _context.Books.ToList();
            foreach (Book b in books)
            {
                long count = CountReview(b.Id);
                ReviewStatisticalDTOs k = new ReviewStatisticalDTOs(b, count);
                kq.Add(k);
            }
            List<ReviewStatisticalDTOs> kqcuoi = kq.OrderByDescending(o => o.reviewCount).Take(n).ToList();
            return kqcuoi;
        }
    }
}