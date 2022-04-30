
using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.Data.Entities
{
    public class MyBooks
    {
        [Key]
        public int ID { get; set; }
        public int ID_Acc { get; set; }
        public int ID_Book { get; set; }
        public virtual Account Acc { get; set; }
        public virtual Book book { get; set; }
        public bool Status { get; set; }
    }
}