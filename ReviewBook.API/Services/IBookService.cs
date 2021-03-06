
using ReviewBook.API.Data.Entities;
using ReviewBook.API.DTOs;

namespace ReviewBook.API.Services
{
    public interface IBookService
    {
        public List<Book> GetAllBooksByPage(int page);
        public Book? GetBookById(int ID);
        public Book CreateBook(Book book);
        public Book? UpdateBook(Book book);
        public bool DeleteBook(int ID);
        public int? CheckName(String Name);

        public List<ProposeBasicDTOs> GetAllProposes();
        public List<Propose> GetProposeByIdUser(int ID);
        public Propose? GetProposeById(int ID);
        public Propose CreatePropose(Propose propose);
        public int? AddBookFromPropose(int Idpropose);
        public bool DeletePropose(int ID);
        public Book_Tag? CreateBookTag(Book_Tag book_Tag);
        public List<Book_Tag> GetAllBookTagsByIdBook(int ID);
        public bool DeleteBookTag(int ID);

        public Propose_Tag CreateProposeTag(Propose_Tag propose_Tag);

        public List<Propose_Tag> GetAllProposeTagsByIdProppose(int ID);
        public bool DeleteProposeTag(int ID);

        public double GetRateAvgBookByIdBook(int idBook);

    }
}