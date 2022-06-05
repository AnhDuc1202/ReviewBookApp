using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.Data.Entities
{
    public class MyTags
    {
        [Key]
        public int ID { get; set; }
        public int ID_Acc { get; set; }
        public int ID_Tag { get; set; }
        public virtual Account Account { get; set; }
        public virtual Tag Tag { get; set; }
    }
}