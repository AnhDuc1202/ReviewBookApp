

namespace ReviewBook.API.DTOs
{
    public class BookAvgDTOs
    {
        public BookAvgDTOs(int iD_Book, double rate)
        {
            ID_Book = iD_Book;
            RateAvg = rate;
            this.time = DateTime.Now;
        }

        public int ID_Book { get; set; }
        public double RateAvg { get; set; }
        public DateTime time { get; set; }
    }
}