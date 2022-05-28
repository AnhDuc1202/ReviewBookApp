

using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public interface IRateBookService
    {
        public List<RateBook> GetAllRateBook();
        public List<RateBook> GetAllRateBookByIdBook(int idBook);
        public RateBook? GetRateBookById(int Id);
        public RateBook CreateRateBook(RateBook rateBook);
        public RateBook? UpdateRateBook(RateBook rateBook);
        public bool DeleteRateBook(int Id);
    }
}