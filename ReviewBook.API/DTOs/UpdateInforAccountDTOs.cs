using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{

    public class UpdateInforAccountDTOs
    {
        public bool IsActive { get; set; }
        public string? FullName { get; set; }
        public DateTime? Birthday { get; set; }

        public string? Address { get; set; }
        public byte[]? Picture { get; set; }
        public Account toAccountEntity(int ID)
        {
            Account acc = new Account();
            acc.ID = ID;
            acc.IsActive = this.IsActive;
            acc.FullName = this.FullName;
            acc.Birthday = this.Birthday;
            acc.Address = this.Address;
            acc.Picture = this.Picture;
            return acc;
        }
    }
}