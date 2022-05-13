using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(Account user, string token)
        {
            Id = user.ID;
            Username = user.UserName;
            Token = token;
        }
    }
}