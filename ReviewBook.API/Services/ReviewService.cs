
using Microsoft.EntityFrameworkCore;
using ReviewBook.API.Data;
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public class ReviewService : IReviewService
    {
        private readonly DataContext _context;

        public ReviewService(DataContext context)
        {
            _context = context;
        }

        public Review CreateReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return review;
        }

        public ReviewChildren CreateReviewChildren(ReviewChildren review)
        {
            _context.reviewChildrens.Add(review);
            _context.SaveChanges();
            return review;
        }

        public bool DeleteReview(int IdReview)
        {
            var currentReview = _context.Reviews.FirstOrDefault(c => c.Id == IdReview);
            if (currentReview == null) return false;
            _context.Reviews.Remove(currentReview);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteReviewChildren(int IdReview)
        {
            var currentReview = _context.reviewChildrens.FirstOrDefault(c => c.Id == IdReview);
            if (currentReview == null) return false;
            _context.reviewChildrens.Remove(currentReview);
            _context.SaveChanges();
            return true;
        }

        public List<Review> GetAllReview()
        {
            return _context.Reviews
            .Include(c => c.reviewChildrens)
            .ThenInclude(d => d.Account)
            .AsNoTracking()
            .ToList();
        }

        public Review? GetReviewById(int Id)
        {
            return _context.Reviews
            .Include(c => c.reviewChildrens)
            .ThenInclude(d => d.Account)
            .AsNoTracking()
            .FirstOrDefault(k => k.Id == Id);
        }

        public List<Review> GetReviewByIdBook(int IdBook)
        {
            return _context.Reviews
            .Include(c => c.reviewChildrens)
            .ThenInclude(d => d.Account)
            .AsNoTracking()
            .Where(e => e.ID_Book == IdBook)
            .ToList();
        }

        public ReviewChildren? GetReviewChildrenById(int Id)
        {
            return _context.reviewChildrens.FirstOrDefault(c => c.Id == Id);
        }

        public Review? UpdateReview(Review review)
        {
            var currentReview = _context.Reviews.FirstOrDefault(c => c.Id == review.Id);
            if (currentReview == null) return null;
            currentReview.Content = review.Content;
            currentReview.Date = review.Date;
            _context.Reviews.Update(currentReview);
            _context.SaveChanges();
            return review;
        }

        public ReviewChildren? UpdateReviewChildren(ReviewChildren review)
        {
            var currentReview = _context.reviewChildrens.FirstOrDefault(c => c.Id == review.Id);
            if (currentReview == null) return null;
            currentReview.Content = review.Content;
            currentReview.Date = review.Date;
            _context.reviewChildrens.Update(currentReview);
            _context.SaveChanges();
            return review;
        }
    }
}