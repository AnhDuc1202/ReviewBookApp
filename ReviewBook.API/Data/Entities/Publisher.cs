

using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.Data.Entities
{
    public class Publisher
    {
        public Publisher()
        {
            Name = string.Empty;
            Books = new List<Book>();
            Proposes = new List<Propose>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(64)]
        public string Name { get; set; }
        [StringLength(256)]
        public string? Address { get; set; }
        public int? Telephone { get; set; }
        [StringLength(64)]
        public string? Email { get; set; }
        [StringLength(64)]
        public string? Website { get; set; }
        public virtual List<Book> Books { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual List<Propose> Proposes { get; set; }

    }
}