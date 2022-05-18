using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class UserRegisterDTOs
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}