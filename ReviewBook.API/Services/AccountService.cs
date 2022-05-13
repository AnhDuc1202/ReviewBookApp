
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

        public Follow CreateFollow(Follow follow)
        {
            _context.Follows.Add(follow);
            _context.SaveChanges();
            return follow;
        }

        public bool DeleteAccount(int IdAcc)
        {
            var currentAcc = GetAccountById(IdAcc);
            if (currentAcc == null) return false;
            _context.Accounts.Remove(currentAcc);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteFollow(int ID)
        {
            var currentFollow = _context.Follows.FirstOrDefault(p => p.ID == ID);
            if (currentFollow == null) return false;
            _context.Follows.Remove(currentFollow);
            _context.SaveChanges();
            return true;
        }

        public Account? GetAccountById(int IdAcc)
        {
            return _context.Accounts
            .Include(a => a.role)
            .Include(b => b.myFollowers)
            .Include(c => c.myFollowings)
            .FirstOrDefault(a => a.ID == IdAcc);
        }

        public List<Account> GetAllAccounts()
        {
            return _context.Accounts
            .Include(a => a.role)
            .Include(b => b.myFollowers)
            .Include(c => c.myFollowings)
            .ToList();
        }

        public Account? UpdateInforAccount(Account account)
        {
            var currentAcc = GetAccountById(account.ID);
            if (currentAcc == null) return null;
            currentAcc.Address = account.Address;
            currentAcc.Birthday = account.Birthday;
            currentAcc.FullName = account.FullName;
            currentAcc.ID_Role = account.ID_Role;
            currentAcc.IsActive = account.IsActive;
            currentAcc.Picture = account.Picture;
            currentAcc.UserName = account.UserName;
            _context.Accounts.Update(currentAcc);
            _context.SaveChanges();
            return currentAcc;
        }
        public Account? UpdatePasswordAccount(Account account)
        {
            var currentAcc = GetAccountById(account.ID);
            if (currentAcc == null) return null;
            currentAcc.Password = account.Password;
            _context.Accounts.Update(currentAcc);
            _context.SaveChanges();
            return currentAcc;
        }
    }
}