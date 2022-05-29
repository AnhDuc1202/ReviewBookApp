using ReviewBook.API.DTOs;

namespace ReviewBook.API.Services
{
    public interface IStatisticalService
    {
        public List<RateStatisticalDTOs> RateStatistical(int n);
        public List<ReviewStatisticalDTOs> ReviewStatistical(int n);
    }
}