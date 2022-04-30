
using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.Data.Entities
{
    public class Role
    {
        public Role()
        {
            NameRole = string.Empty;
            Accounts = new List<Account>();
        }

        public Role(string nameRole)
        {
            NameRole = nameRole;
            Accounts = new List<Account>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(64)]
        public string NameRole { get; set; }

        public virtual List<Account> Accounts { get; set; }
    }
}