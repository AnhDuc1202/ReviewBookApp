

using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.Data.Entities
{
    public class Propose_Tag
    {
        [Key]
        public int ID { get; set; }
        public int ID_Propose { get; set; }
        public int ID_Tag { get; set; }
        public virtual Propose propose { get; set; }
        public virtual Tag tag { get; set; }
    }
}