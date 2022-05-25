using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
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