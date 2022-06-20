using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class RateStatisticalDTOs
    {
        public RateStatisticalDTOs(Book b, double rateAvg)
        {
            ID_Book = b.Id;
            Name = b.Name;
            Picture = b.Picture;
            RateAvg = rateAvg;
            stageNameAuthor = b.author.Stage_Name;
        }

        public int ID_Book { get; set; }
        public string Name { get; set; }
        public string? stageNameAuthor { get; set; }
        public byte[]? Picture { get; set; }
        public double RateAvg { get; set; }

    }
}