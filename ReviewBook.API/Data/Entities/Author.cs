using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.Data.Entities
{
    public class Author
    {
        public Author()
        {
            Name = string.Empty;
            Books = new List<Book>();
            Proposes = new List<Propose>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Stage_Name { get; set; }

        public DateTime? Birthday { get; set; }
        public string? Description { get; set; }

        public virtual List<Book> Books { get; set; }
        public virtual List<Propose> Proposes { get; set; }
    }
}