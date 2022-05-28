

using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class RateBookDTOs
    {
        public int ID_Acc { get; set; }
        public int ID_Book { get; set; }
        public int Rate { get; set; }

        public RateBook toEntitiesRateBook()
        {
            RateBook r = new RateBook();
            r.ID_Acc = ID_Acc;
            r.ID_Book = ID_Book;
            r.Rate = Rate;
            return r;
        }
    }
}