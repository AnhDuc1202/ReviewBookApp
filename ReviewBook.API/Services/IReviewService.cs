
using ReviewBook.API.Data.Entities;
using ReviewBook.API.DTOs;

namespace ReviewBook.API.Services
{
    public interface IReviewService
    {
        public List<Review> GetAllReview();
        public List<Review> GetReviewByIdBook(int IdBook);
        public Review CreateReview(Review review);
        public Review? UpdateReview(Review review);
        public bool DeleteReview(int IdReview);
        public ReviewChildren CreateReviewChidren(ReviewChildren review);
        public ReviewChildren? UpdateReviewChidren(ReviewChildren review);
        public bool DeleteReviewChidren(int IdReview);
    }
}