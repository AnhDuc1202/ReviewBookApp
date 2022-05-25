using ReviewBook.API.Data.Entities;
using ReviewBook.API.DTOs;
using ReviewBook.API.Models;

namespace ReviewBook.API.Services
{
    public interface IUserService
    {
        public Account CreateAccount(Account account);

        // public Account FindByNameAndPass(Account account);
        public AuthenticateResponse Authenticate(AuthenticateRequest model);
        // IEnumerable<User> GetAll();
        public Account? jwtTokenToAccount(string token);
        public Account GetById(int id);
        public List<UserReadReviewDTOs> readReviewbyIdBook(int idBook);

        public Review writeReview(UserWriteReviewDTOs review);

        public List<Book> searchForBookOrAuthor(String bookOrAuthor);
    }
}