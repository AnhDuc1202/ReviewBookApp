
using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.Data.Entities
{
    public class Propose_NewTag
    {
        [Key]
        public int ID { get; set; }
        public String nameNewTag { get; set; }
        public int ID_Propose { get; set; }
        public virtual Propose Propose { get; set; }
    }
}