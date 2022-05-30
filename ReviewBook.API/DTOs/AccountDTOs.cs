using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class CreateAccountAdminDTOs
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

    public class CreateAccountDTOs
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public Account toAccountEntity()
        {
            Account acc = new Account();
            acc.UserName = this.UserName;
            acc.Password = this.Password;
            acc.IsActive = true;
            acc.ID_Role = 2;
            return acc;
        }
    }

    public class UpdateInforAccountDTOs
    {
        public string? FullName { get; set; }
        public DateTime? Birthday { get; set; }

        public string? Address { get; set; }
        public byte[]? Picture { get; set; }
        public Account toAccountEntity(int ID)
        {
            Account acc = new Account();
            acc.ID = ID;
            acc.IsActive = true;
            acc.FullName = this.FullName;
            acc.Birthday = this.Birthday;
            acc.Address = this.Address;
            acc.Picture = this.Picture;
            return acc;
        }
    }

    public class UpdatePasswordAccountDTOs
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public Account toAccountEntity(int ID)
        {
            Account acc = new Account();
            acc.ID = ID;
            acc.Password = this.NewPassword;
            return acc;
        }
    }
}