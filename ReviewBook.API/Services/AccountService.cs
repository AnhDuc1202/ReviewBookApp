
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

        public Account? CreateAccount(Account account)
        {
            var currentAccount = _context.Accounts.FirstOrDefault(a => a.UserName == account.UserName);
            if (currentAccount != null) return null;
            _context.Accounts.Add(account);
            _context.SaveChanges();
            return account;
        }

        public Follow? CreateFollow(Follow follow)
        {
            var acc1 = GetAccountById(follow.ID_Follower);
            var acc2 = GetAccountById(follow.ID_Following);
            if (acc1 == null || acc2 == null || acc1.ID == acc2.ID)
                return null;
            _context.Follows.Add(follow);
            _context.SaveChanges();
            return follow;
        }

        public MyTags? CreateMyTag(MyTags myTags)
        {
            var mt = GetMyTagByIdAccAndIdTag(myTags.ID_Acc, myTags.ID_Tag);
            if (mt != null) return null;
            _context.myTags.Add(myTags);
            _context.SaveChanges();
            return myTags;
        }

        public bool DeleteAccount(int IdAcc)
        {
            var currentAcc = GetAccountById(IdAcc);
            if (currentAcc == null) return false;
            _context.Accounts.Remove(currentAcc);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteFollow(Follow follow)
        {
            var currentFollow = GetFollowByIdAccFollowerAndFollowing(follow.ID_Follower, follow.ID_Following);
            if (currentFollow == null) return false;
            _context.Follows.Remove(currentFollow);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteMyTag(MyTags myTags)
        {
            var mt = GetMyTagByIdAccAndIdTag(myTags.ID_Acc, myTags.ID_Tag);
            if (mt == null) return false;
            _context.myTags.Remove(mt);
            _context.SaveChanges();
            return true;
        }

        public Account? GetAccountById(int IdAcc)
        {
            return _context.Accounts
            .Include(a => a.role)
            .Include(b => b.myFollowers)
            .Include(c => c.myFollowings)
            .AsNoTracking()
            .FirstOrDefault(a => a.ID == IdAcc);
        }

        public Account? GetAccountByIdHasMyTag(int IdAcc)
        {
            return _context.Accounts
            .Include(a => a.role)
            .Include(b => b.myFollowers)
            .Include(c => c.myFollowings)
            .Include(d => d.myBooks)
                .ThenInclude(d1 => d1.book)
            .Include(d => d.myTags)
                .ThenInclude(d1 => d1.Tag)
            .AsNoTracking()
            .FirstOrDefault(a => a.ID == IdAcc);
        }
        public Account? GetAccountByIdNoPassword(int IdAcc)
        {
            var acc = _context.Accounts
            .Include(a => a.role)
            .Include(b => b.myFollowers)
            .Include(c => c.myFollowings)
            .AsNoTracking()
            .FirstOrDefault(a => a.ID == IdAcc);
            if (acc != null) acc.Password = String.Empty;
            return acc;
        }

        public List<Account> GetAllAccounts()
        {
            return _context.Accounts
            .Include(a => a.role)
            .Include(b => b.myFollowers)
            .Include(c => c.myFollowings)
            .AsNoTracking()
            .ToList();
        }

        public List<Account> GetAllAccountsNoPassword()
        {
            var accs = _context.Accounts
            .Include(a => a.role)
            .Include(b => b.myFollowers)
            .Include(c => c.myFollowings)
            .AsNoTracking()
            .ToList();
            accs.ForEach(c => c.Password = string.Empty);
            return accs;
        }

        public Follow? GetFollowByIdAccFollowerAndFollowing(int ID_Follower, int ID_Following)
        {
            return _context.Follows
            .FirstOrDefault(a => a.ID_Follower == ID_Follower && a.ID_Following == ID_Following);
        }

        public MyTags? GetMyTagByIdAccAndIdTag(int ID_Acc, int ID_Tag)
        {
            return _context.myTags.FirstOrDefault(c => c.ID_Acc == ID_Acc && c.ID_Tag == ID_Tag);
        }

        public Account? UpdateInforAccount(Account account)
        {
            var currentAcc = GetAccountById(account.ID);
            if (currentAcc == null) return null;
            currentAcc.Address = account.Address;
            currentAcc.Birthday = account.Birthday;
            currentAcc.FullName = account.FullName;
            //currentAcc.ID_Role = account.ID_Role;
            currentAcc.IsActive = account.IsActive;
            currentAcc.Picture = account.Picture;
            //currentAcc.UserName = account.UserName;
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