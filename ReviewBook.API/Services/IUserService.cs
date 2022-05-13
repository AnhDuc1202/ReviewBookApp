using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReviewBook.API.Data.Entities;
using ReviewBook.API.Models;

namespace ReviewBook.API.Services
{
    public interface IUserService
    {
        public Account CreateAccount(Account account);

        // public Account FindByNameAndPass(Account account);
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        // IEnumerable<User> GetAll();
        Account GetById(int id);

        Account EditAccount(Account account);
    }
}