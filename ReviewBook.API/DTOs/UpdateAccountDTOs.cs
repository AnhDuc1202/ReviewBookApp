using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class UpdateAccountDTOs
    {
        public string UserName { get; set; }

        public string Password { get; set; }
        public bool IsActive { get; set; }

        public string? FullName { get; set; }
        public DateTime? Birthday { get; set; }

        public string? Address { get; set; }
        public byte[]? Picture { get; set; }
        public int ID_Role { get; set; }
        public Account toAccountEntity(int ID)
        {
            Account acc = new Account();
            acc.ID = ID;
            acc.UserName = this.UserName;
            acc.Password = this.Password;
            acc.IsActive = this.IsActive;
            acc.FullName = this.FullName;
            acc.Birthday = this.Birthday;
            acc.Address = this.Address;
            acc.Picture = this.Picture;
            acc.ID_Role = this.ID_Role;
            return acc;
        }
    }
}