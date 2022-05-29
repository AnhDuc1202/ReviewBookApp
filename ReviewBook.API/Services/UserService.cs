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
using ReviewBook.API.DTOs;

namespace ReviewBook.API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext context;
        public readonly IConfiguration _configuration;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, DataContext context, IConfiguration configuration)
        {
            _appSettings = appSettings.Value;
            this.context = context;
            _configuration = configuration;
        }

        //Authenticate
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = this.context.Accounts.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        private string generateJwtToken(Account user)
        {
            // generate token that is valid for 7 days
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("id", user.ID.ToString()),
                        new Claim("UserName", user.UserName.ToString()),
                        new Claim("Role", user.ID_Role.ToString())
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public Account? jwtTokenToAccount(string token)
        {
            var a = new JwtSecurityTokenHandler().ReadJwtToken(token);
            Account acc = new Account();
            var id = Int32.Parse(a.Claims.First(c => c.Type == "id").Value);
            var UserName = a.Claims.First(c => c.Type == "UserName").Value;
            var Role = Int32.Parse(a.Claims.First(c => c.Type == "Role").Value);
            if (id == null || UserName == null || Role == null) return null;
            acc.ID = id;
            acc.UserName = UserName;
            acc.ID_Role = Role;
            return acc;
        }
        public Account CreateAccount(Account account)
        {
            this.context.Accounts.Add(account);
            this.context.SaveChanges();
            return account;
        }

        //Search book
        public List<Book> searchForBookOrAuthor(String bookOrAuthor)
        {
            List<Book> result = this.context.Books.Where(b => b.Name.ToLower().Contains(bookOrAuthor.ToLower()) || this.context.Authors.FirstOrDefault(a => a.Id == b.ID_Aut).Name.ToLower().Contains(bookOrAuthor.ToLower())).ToList();
            return result;
        }

        public Account GetById(int id)
        {
            return this.context.Accounts.FirstOrDefault(x => x.ID == id);
        }
    }
}