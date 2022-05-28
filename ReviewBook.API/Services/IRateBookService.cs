

using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public interface IRateBookService
    {
        public List<RateBook> GetAllRateBook();
        public double GetAllRateBookByIdBook(int idBook);
        public RateBook? GetRateBookById(int Id);
        public RateBook? GetRateBookByIdAccAndIdBook(int IdAcc, int idBook);
        public RateBook CreateOrUpdateRateBook(RateBook rateBook);
    }
}