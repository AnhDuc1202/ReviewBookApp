using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.Data.Entities
{
    public class Review
    {
        public Review()
        {
            Content = string.Empty;
            Date = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }
        public int ID_Acc { get; set; }
        public int ID_Book { get; set; }
        [StringLength(2048)]
        public string Content { get; set; }
        public int Rate { get; set; }
        public DateTime Date { get; set; }
        public virtual Account Account { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Book Book { get; set; }
        public virtual List<ReviewChildren> reviewChildrens { get; set; }
    }
}