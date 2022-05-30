using ReviewBook.API.Data.Entities;
namespace ReviewBook.API.DTOs
{
    public class UserAddMyBookDTOs
    {
        public int ID_Book { get; set; }

        public MyBooks toEntitiesMyBooks(int ID_Account)
        {
            MyBooks m = new MyBooks();
            m.ID_Acc = ID_Account;
            m.ID_Book = ID_Book;
            m.StatusBook = 1;
            return m;
        }
    }

    public class UserEditBookStatusDTOs
    {
        public int ID_Book { get; set; }
        public int Status { get; set; }
        public MyBooks toEntitiesMyBooks(int ID_Account)
        {
            MyBooks m = new MyBooks();
            m.ID_Acc = ID_Account;
            m.ID_Book = ID_Book;
            m.StatusBook = Status;
            return m;
        }
    }
}