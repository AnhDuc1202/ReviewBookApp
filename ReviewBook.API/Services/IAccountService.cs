using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public interface IAccountService
    {
        public List<Account> GetAllAccounts();
        public List<Account> GetAllAccountsNoPassword();
        public Account? GetAccountById(int IdAcc);
        public Account? GetAccountByIdNoPassword(int IdAcc);
        public Account? CreateAccount(Account account);
        public Account? UpdatePasswordAccount(Account account);
        public Account? UpdateInforAccount(Account account);
        public bool DeleteAccount(int IdAcc);

        public Follow CreateFollow(Follow follow);
        public bool DeleteFollow(int ID);
    }
}