
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public interface IBookService
    {
        public List<Book> GetAllBooks();
        public Book? GetBookById(int ID);
        public Book CreateBook(Book book);
        public Book? UpdateBook(Book book);
        public bool DeleteBook(int ID);

        public List<Propose> GetAllProposes();
        public Propose? GetProposeById(int ID);
        public Propose CreatePropose(Propose propose);
        public Propose? UpdatePropose(Propose propose);
        public bool DeletePropose(int ID);

        public List<Book_Tag> GetAllBookTags();
        public bool DeleteBookTag(int ID);

        public List<Propose_Tag> GetAllProposeTags();
        public bool DeleteProposeTag(int ID);
    }
}