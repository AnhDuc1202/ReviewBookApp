using ReviewBook.API.Data.Entities;
using ReviewBook.API.DTOs;
using ReviewBook.API.Models;

namespace ReviewBook.API.Services
{
    public interface IUserService
    {
        public AuthenticateResponse Authenticate(AuthenticateRequest model);
        public Account? jwtTokenToAccount(string token);
        public List<Book> searchForBookOrAuthor(String bookOrAuthor);
        public Account GetById(int id);

        public MyBooks AddMyBook(MyBooks value);

        public MyBooks EditBookStatus(MyBooks value);

        public List<MyBooks> GetAllMyBooksByIdAcc(int idAcc);

        public MyBooks GetMyBookByIdBook(MyBooks value);

        public bool DeleteBookById(MyBooks value);
    }
}