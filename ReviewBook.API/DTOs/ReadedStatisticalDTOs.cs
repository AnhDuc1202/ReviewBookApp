using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class ReadedStatisticalDTOs
    {
        public ReadedStatisticalDTOs(Book b, long r)
        {
            ID_Book = b.Id;
            Name = b.Name;
            Picture = b.Picture;
            readed = r;
        }

        public int ID_Book { get; set; }
        public string Name { get; set; }
        public byte[]? Picture { get; set; }
        public long readed { get; set; }
    }
}