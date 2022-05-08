using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class UpdatePasswordAccountDTOs
    {
        public string Password { get; set; }
        public Account toAccountEntity(int ID)
        {
            Account acc = new Account();
            acc.ID = ID;
            acc.Password = this.Password;
            return acc;
        }
    }
}