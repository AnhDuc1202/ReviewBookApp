
using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.Data.Entities
{
    public class Book_Tag
    {
        [Key]
        public int ID { get; set; }
        public int ID_Book { get; set; }
        public int ID_Tag { get; set; }
        public virtual Book book { get; set; }
        public virtual Tag tag { get; set; }
    }
}