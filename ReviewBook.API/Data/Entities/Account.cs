
using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.Data.Entities
{
    public class Account
    {
        public Account()
        {
            UserName = string.Empty;
            Password = string.Empty;
            IsActive = true;
            myFollowings = new List<Follow>();
            myFollowers = new List<Follow>();
            myBooks = new List<MyBooks>();
            reviews = new List<Review>();
            Proposes = new List<Propose>();
            ReviewChildrens = new List<ReviewChildren>();
        }
        [Key]
        public int ID { get; set; }
        [StringLength(64)]
        public string UserName { get; set; }
        [StringLength(64)]


        public string Password { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsActive { get; set; }
        [StringLength(128)]
        public string? FullName { get; set; }
        public DateTime? Birthday { get; set; }
        [StringLength(256)]
        public string? Address { get; set; }
        public byte[]? Picture { get; set; }
        public int ID_Role { get; set; }
        public virtual Role role { get; set; }
        public virtual List<Follow> myFollowings { get; set; }
        public virtual List<Follow> myFollowers { get; set; }
        public virtual List<MyBooks> myBooks { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual List<Review> reviews { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual List<ReviewChildren> ReviewChildrens { get; set; }
        public virtual List<Propose> Proposes { get; set; }
    }
}