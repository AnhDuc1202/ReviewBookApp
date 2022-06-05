using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public interface IAccountService
    {
        public List<Account> GetAllAccounts();
        public List<Account> GetAllAccountsNoPassword();
        public Account? GetAccountById(int IdAcc);
        public Account? GetAccountByIdHasMyTag(int IdAcc);
        public Account? GetAccountByIdNoPassword(int IdAcc);
        public Account? CreateAccount(Account account);
        public Account? UpdatePasswordAccount(Account account);
        public Account? UpdateInforAccount(Account account);
        public bool DeleteAccount(int IdAcc);

        public Follow? GetFollowByIdAccFollowerAndFollowing(int ID_Follower, int ID_Following);
        public Follow? CreateFollow(Follow follow);
        public bool DeleteFollow(Follow follow);

        public MyTags? GetMyTagByIdAccAndIdTag(int ID_Acc, int ID_Tag);
        public MyTags? CreateMyTag(MyTags myTags);
        public bool DeleteMyTag(MyTags myTags);
    }
}