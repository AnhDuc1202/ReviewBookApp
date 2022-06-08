
using ReviewBook.API.Data.Entities;

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

        public List<Propose> GetAllProposes();
        public List<Propose> GetProposeByIdUser(int ID);
        public Propose? GetProposeById(int ID);
        public Propose CreatePropose(Propose propose);
        public Propose_NewTag CreateProposeNewTags(Propose_NewTag propose_NewTag);
        public Propose? UpdatePropose(Propose propose);
        public bool DeletePropose(int ID);
        public Propose_NewTag? GetProposeNewTagsById(int ID);
        public bool DeleteProposeNewTags(int ID);

        public Book_Tag? CreateBookTag(Book_Tag book_Tag);
        public List<Book_Tag> GetAllBookTagsByIdBook(int ID);
        public bool DeleteBookTag(int ID);

        public Propose_Tag CreateProposeTag(Propose_Tag propose_Tag);

        public List<Propose_Tag> GetAllProposeTagsByIdProppose(int ID);
        public bool DeleteProposeTag(int ID);

        public double GetRateAvgBookByIdBook(int idBook);

    }
}