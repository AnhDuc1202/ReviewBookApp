using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class ReviewStatisticalDTOs
    {
        public ReviewStatisticalDTOs(Book b, long r)
        {
            ID_Book = b.Id;
            Name = b.Name;
            Picture = b.Picture;
            reviewCount = r;
        }

        public int ID_Book { get; set; }
        public string Name { get; set; }
        public byte[]? Picture { get; set; }
        public long reviewCount { get; set; }
    }
}