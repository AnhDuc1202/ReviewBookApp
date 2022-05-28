using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.Data.Entities
{
    public class Book
    {
        public Book()
        {
            Name = string.Empty;
            Accounts = new List<MyBooks>();
            reviews = new List<Review>();
            Tags = new List<Book_Tag>();
            RateBooks = new List<RateBook>();
            description = String.Empty;
        }
        [Key]
        public int Id { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
        public int ID_Aut { get; set; }
        public int ID_Pub { get; set; }
        public int PublishedYear { get; set; }
        public byte[]? Picture { get; set; }
        public String description { get; set; }
        public virtual List<MyBooks> Accounts { get; set; }
        public virtual List<Review> reviews { get; set; }
        public virtual Author author { get; set; }
        public virtual Publisher publisher { get; set; }
        public virtual List<Book_Tag> Tags { get; set; }
        public virtual List<RateBook> RateBooks { get; set; }
    }
}