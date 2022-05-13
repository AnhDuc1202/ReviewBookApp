using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReviewBook.API.Data.Entities;
using ReviewBook.API.Data;
using ReviewBook.API.Models;
using ReviewBook.API.Helpers;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ReviewBook.API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext context;

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, DataContext context)
        {
            _appSettings = appSettings.Value;
            this.context = context;
        }
        // public UserService(DataContext context)
        // {
        //     this.context = context;
        // }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = this.context.Accounts.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public Account CreateAccount(Account account)
        {
            this.context.Accounts.Add(account);
            this.context.SaveChanges();
            return account;
        }

        public Account GetById(int id)
        {
           return this.context.Accounts.FirstOrDefault(x => x.ID == id);
        }

        public Account? GetAccountById(int IdAcc)
        {
            return this.context.Accounts.Include(a => a.role).FirstOrDefault(a => a.ID == IdAcc);
        }

        public Account? EditAccount(Account account){
            var currentAccount = GetAccountById(account.ID);
            if(currentAccount == null)
                return null;
            currentAccount.Address = account.Address;
            currentAccount.Birthday = account.Birthday;
            currentAccount.FullName = account.FullName;
            currentAccount.Password = account.Password;
            currentAccount.Picture = account.Picture;
            currentAccount.UserName = account.UserName;
            this.context.Accounts.Update(currentAccount);
            this.context.SaveChanges();
            return currentAccount;
            
        }

        // public Account FindByNameAndPass(Account account)
        // {
        //     return this.context.Accounts.Include(a => a.role).FirstOrDefault(a => a.UserName == account.UserName && a.Password == account.Password);
        // }
        private string generateJwtToken(Account user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}