

using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.Data.Entities
{
    public class RateBook
    {
        [Key]
        public int ID { get; set; }
        public int ID_Acc { get; set; }
        public int ID_Book { get; set; }
        public int Rate { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Account Account { get; set; }
        public virtual Book Book { get; set; }
    }
}