using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.Data.Entities
{
    public class Tag
    {
        public Tag()
        {
            Name = string.Empty;
            Description = string.Empty;
            Books = new List<Book_Tag>();
            Proposes = new List<Propose_Tag>();
            myTags = new List<MyTags>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(2048)]
        public string? Description { get; set; }
        public virtual List<Book_Tag> Books { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual List<Propose_Tag> Proposes { get; set; }
        public virtual List<MyTags> myTags { get; set; }
    }
}