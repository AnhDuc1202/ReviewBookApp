using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public interface IAccountService
    {
        public List<Account> GetAllAccounts();
        public Account? GetAccountById(int IdAcc);
        public Account CreateAccount(Account account);
        public Account? UpdateAccount(Account account);
        public bool DeleteAccount(int IdAcc);
    }
}