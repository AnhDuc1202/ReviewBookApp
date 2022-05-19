

using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class CreateBookDTOs
    {
        public string Name { get; set; }
        public int ID_Aut { get; set; }
        public int ID_Pub { get; set; }
        public int PublishedYear { get; set; }
        public List<int> List_ID_Tags { get; set; }
        public Book toBookEntity()
        {
            Book b = new Book();
            b.Name = Name;
            b.ID_Aut = ID_Aut;
            b.ID_Pub = ID_Pub;
            b.PublishedYear = PublishedYear;
            return b;
        }
    }
}