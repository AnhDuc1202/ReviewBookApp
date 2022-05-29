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

        public bool Follow(UserFollowDTOs value)
        {
            Follow current = this.context.Follows.FirstOrDefault(f => f.ID_Following == value.IdFollowing || f.ID_Follower == value.IdFollower);
            if(current != null){
                this.context.Follows.Remove(current);
                this.context.SaveChanges();
                return false;
            }
            current = new Follow();
            current.ID_Following = value.IdFollowing;
            current.ID_Follower = value.IdFollower;
            this.context.Follows.Add(current);
            this.context.SaveChanges();
            return true;
        }

        public MyBooks AddMyBook(UserAddMyBookDTOs value)
        {
            MyBooks current = this.context.myBooks.FirstOrDefault(m => m.ID_Book == value.ID_Book);
            if(current != null)
                return null;
            current = new MyBooks();
            current.ID_Acc = value.ID_Account;
            current.ID_Book = value.ID_Book;
            current.Status = false;
            this.context.myBooks.Add(current);
            this.context.SaveChanges();
            return current;
        }

        public MyBooks EditBookStatus(UserEditBookStatusDTOs value)
        {
            MyBooks current = this.context.myBooks.FirstOrDefault(m => m.ID_Book == value.ID_Book);
            if(current == null)
                return null;
            if (value.selectIndex == 1)
                current.Status = false;
            else
                current.Status = true;
            this.context.SaveChanges();
            return current;
        }

        public List<MyBooks> GetAllMyBooks()
        {
            List<MyBooks> current = this.context.myBooks.ToList();
            if(current.Count == 0)
                return null;
            return this.context.myBooks.Include(a => a.Acc).Include(b => b.book).ToList();
        }

        public MyBooks GetMyBookById(int id)
        {
            MyBooks current = this.context.myBooks.Include(a => a.book).FirstOrDefault(m => m.ID_Book == id);
            if (current == null)
                return null;
            return current;
        }

        public bool DeleteBookById(int id)
        {
            MyBooks current = this.context.myBooks.FirstOrDefault(m => m.ID == id);
            if (current == null)
                return false;
            this.context.myBooks.Remove(current);
            this.context.SaveChanges();
            return true;
        }
    }
}