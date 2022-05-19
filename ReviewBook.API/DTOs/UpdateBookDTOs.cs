
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class UpdateBookDTOs
    {
        public string Name { get; set; }
        public int ID_Aut { get; set; }
        public int ID_Pub { get; set; }
        public int PublishedYear { get; set; }
        public byte[]? Picture { get; set; }
        public List<int> List_ID_Tags_Remove { get; set; }
        public List<int> List_ID_Tags_Add { get; set; }
        public Book toBookEntity(int ID)
        {
            Book b = new Book();
            b.Id = ID;
            b.Name = Name;
            b.ID_Aut = ID_Aut;
            b.ID_Pub = ID_Pub;
            b.PublishedYear = PublishedYear;
            b.Picture = Picture;
            return b;
        }
    }
}