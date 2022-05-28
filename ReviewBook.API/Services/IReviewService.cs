
using ReviewBook.API.Data.Entities;
using ReviewBook.API.DTOs;

namespace ReviewBook.API.Services
{
    public interface IReviewService
    {
        public List<Review> GetAllReview();
        public List<Review> GetReviewByIdBook(int IdBook);
        public Review? GetReviewById(int Id);
        public Review CreateReview(Review review);
        public Review? UpdateReview(Review review);
        public bool DeleteReview(int IdReview);

        public ReviewChildren? GetReviewChildrenById(int Id);
        public ReviewChildren CreateReviewChildren(ReviewChildren review);
        public ReviewChildren? UpdateReviewChildren(ReviewChildren review);
        public bool DeleteReviewChildren(int IdReview);
    }
}