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
using ReviewBook.API.DTOs;

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
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", user.ID.ToString()),
                                                     new Claim("Username", user.UserName.ToString()),
                                                     new Claim("Password", user.Password.ToString()),
                                                     new Claim("Role", user.ID_Role.ToString())       
                                                    }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public Account CreateAccount(Account account)
        {
            this.context.Accounts.Add(account);
            this.context.SaveChanges();
            return account;
        }


        //Register account
        public String UserRegisterAccount(UserRegisterDTOs user){
            Account exits = this.context.Accounts.FirstOrDefault(a => a.UserName == user.UserName);
            if(exits != null)
                return "This user name is already registered!";
            else{
                if(user.Password == user.ConfirmPassword){
                    Account result = new Account();
                    result.UserName = user.UserName;
                    result.Password = user.Password;
                    CreateAccount(result);
                    return "Register successfully!";
                }
                else
                    return "Password and it's confirm are not the same!";
            }
        }

        public Account GetById(int id)
        {
            return this.context.Accounts.FirstOrDefault(x => x.ID == id);
        }

        public Account? GetAccountById(int IdAcc)
        {
            return this.context.Accounts.Include(a => a.role).FirstOrDefault(a => a.ID == IdAcc);
        }


        //Edit account
        public Account? EditAccount(Account account)
        {
            var currentAccount = GetAccountById(account.ID);
            if (currentAccount == null)
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

        //Read reviews
        public List<UserReadReviewDTOs> readReview(int idBook)
        {
            List<UserReadReviewDTOs> list = new List<UserReadReviewDTOs>();
            List<Review> reviews = this.context.Reviews.Where(r => r.ID_Book == idBook).ToList();
            foreach (Review review in reviews)
            {
                UserReadReviewDTOs result = new UserReadReviewDTOs();
                String UserName = GetAccountById(review.ID_Acc).UserName;
                result.UserName = UserName;
                result.Id = review.Id;
                result.ID_Acc = review.ID_Acc;
                result.ID_Book = review.ID_Book;
                result.Content = review.Content;
                result.Date = review.Date;
                result.Rate = review.Rate;
                list.Add(result);
            }
            return list;
        }

        //Write reviews
        public Review writeReview(UserWriteReviewDTOs review)
        {
            Review result = new Review();
            result.ID_Acc = review.ID_Acc;
            result.ID_Book = review.ID_Book;
            result.Content = review.Content;
            result.Date = review.Date;
            result.Rate = review.Rate;
            this.context.Reviews.Add(result);
            this.context.SaveChanges();
            return result;
        }

        //Search book
        public List<Book> searchForBookOrAuthor(String bookOrAuthor){
            List<Book> result = this.context.Books.Where(b => b.Name.ToLower().Contains(bookOrAuthor.ToLower()) || this.context.Authors.FirstOrDefault(a => a.Id == b.ID_Aut).Name.ToLower().Contains(bookOrAuthor.ToLower())).ToList();
            return result;
        }

        //Propose tag
        public UserPropose_TagDTOs proposeTag(UserPropose_TagDTOs proposeTag){
            Tag? check = this.context.Tag.FirstOrDefault(t => t.Name == proposeTag.Name);
            if(check != null)
                return null;
            else
                return proposeTag;
        }

        public UserProposeBookDTOs proposeBook(UserProposeBookDTOs proposeBook){
            Book? check = this.context.Books.FirstOrDefault(b => b.Name == proposeBook.BookName);
            if(check != null)
                return null;
            else
                return proposeBook;
        }
    }
}