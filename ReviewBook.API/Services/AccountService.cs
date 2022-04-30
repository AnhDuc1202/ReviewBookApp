
using Microsoft.EntityFrameworkCore;
using ReviewBook.API.Data;
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;

        public AccountService(DataContext context)
        {
            _context = context;
        }

        public Account CreateAccount(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
            return account;
        }

        public bool DeleteAccount(int IdAcc)
        {
            var currentAcc = GetAccountById(IdAcc);
            if (currentAcc == null) return false;
            _context.Accounts.Remove(currentAcc);
            _context.SaveChanges();
            return true;
        }

        public Account? GetAccountById(int IdAcc)
        {
            return _context.Accounts.Include(a => a.role).FirstOrDefault(a => a.ID == IdAcc);
        }

        public List<Account> GetAllAccounts()
        {
            return _context.Accounts.Include(a => a.role).ToList();
        }

        public Account? UpdateAccount(Account account)
        {
            var currentAcc = GetAccountById(account.ID);
            if (currentAcc == null) return null;
            currentAcc.Address = account.Address;
            currentAcc.Birthday = account.Birthday;
            currentAcc.FullName = account.FullName;
            currentAcc.ID_Role = account.ID_Role;
            currentAcc.IsActive = account.IsActive;
            currentAcc.Password = account.Password;
            currentAcc.Picture = account.Picture;
            currentAcc.UserName = account.UserName;
            _context.Accounts.Update(currentAcc);
            _context.SaveChanges();
            return currentAcc;
        }
    }
}