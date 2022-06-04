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

        //Search book
        public List<Book> searchForBookOrAuthor(String bookOrAuthor)
        {
            List<Book> result = this.context.Books.Where(b => b.Name.ToLower().Contains(bookOrAuthor.ToLower()) || 
            this.context.Authors.FirstOrDefault(a => a.Id == b.ID_Aut).Name.ToLower().Contains(bookOrAuthor.ToLower()))
            .Include(a => a.author)
            .Include(b => b.publisher)
            .Include(c => c.Tags)
                .ThenInclude(d => d.tag)
            .ToList();
            return result;
        }

        public Account GetById(int id)
        {
            return this.context.Accounts.FirstOrDefault(x => x.ID == id);
        }

        public MyBooks AddMyBook(MyBooks value)
        {
            MyBooks current = this.context.myBooks.FirstOrDefault(m => m.ID_Book == value.ID_Book && m.ID_Acc == value.ID_Acc);
            if (current != null || !(value.StatusBook == 1 || value.StatusBook == 2 || value.StatusBook == 3))
                return null;
            this.context.myBooks.Add(value);
            this.context.SaveChanges();
            return value;
        }

        public MyBooks EditBookStatus(MyBooks value)
        {
            MyBooks current = this.context.myBooks.FirstOrDefault(m => m.ID_Book == value.ID_Book && m.ID_Acc == value.ID_Acc);
            if (current == null || !(value.StatusBook == 1 || value.StatusBook == 2 || value.StatusBook == 3))
                return null;
            current.StatusBook = value.StatusBook;
            context.myBooks.Update(current);
            this.context.SaveChanges();
            return current;
        }

        public List<MyBooks> GetAllMyBooksByIdAcc(int ID)
        {
            List<MyBooks> current = this.context.myBooks.ToList();
            if (current.Count == 0)
                return null;
            return this.context.myBooks.Where(a => a.ID_Acc == ID).Include(b => b.book).AsNoTracking().ToList();
        }

        public MyBooks GetMyBookByIdBook(MyBooks myBook)
        {
            MyBooks current = this.context.myBooks
                .Include(a => a.book).FirstOrDefault(m => m.ID_Book == myBook.ID_Book && m.ID_Acc == myBook.ID_Acc);
            if (current == null)
                return null;
            return current;
        }

        public bool DeleteBookById(MyBooks myBooks)
        {
            MyBooks current = this.context.myBooks.FirstOrDefault(m => m.ID_Acc == myBooks.ID_Acc && m.ID_Book == myBooks.ID_Book);
            if (current == null)
                return false;
            this.context.myBooks.Remove(current);
            this.context.SaveChanges();
            return true;
        }
    }
}