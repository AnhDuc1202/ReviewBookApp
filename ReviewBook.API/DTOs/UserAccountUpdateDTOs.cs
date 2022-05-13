using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class UserAccountUpdateDtOs
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
        public byte[]? Picture { get; set; }

        public Account toAccountEntity()
        {
            if(this.Password == this.ConfirmPassword){
                Account acc = new Account();
                acc.UserName = this.UserName;
                acc.Password = this.Password;
                acc.FullName = this.FullName;
                acc.Birthday = this.Birthday;
                acc.Address = this.Address;
                acc.Picture = this.Picture;
                return acc;
            }
            else
                return null;
        }
    }
}