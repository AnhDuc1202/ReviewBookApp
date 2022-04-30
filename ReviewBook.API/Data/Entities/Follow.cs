using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.Data.Entities
{
    public class Follow
    {
        [Key]
        public int ID { get; set; }
        public int ID_Following { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Account Following { get; set; }
        public int ID_Follower { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Account Follower { get; set; }
    }
}