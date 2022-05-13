using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class UserRegisterDTOs
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public Account toAccountEntity()
        {
            if (this.Password == this.ConfirmPassword)
            {
                Account acc = new Account();
                acc.UserName = this.UserName;
                acc.Password = this.Password;
                acc.IsActive = true;
                acc.ID_Role = 1;
                return acc;
            }
            else
                return null;
        }
    }
}