using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class CreateAccountDTOs
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ID_Role { get; set; }

        public Account toAccountEntity()
        {
            Account acc = new Account();
            acc.UserName = this.UserName;
            acc.Password = this.Password;
            acc.IsActive = true;
            acc.ID_Role = this.ID_Role;
            return acc;
        }
    }
}