
using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.Data.Entities
{
    public class Propose
    {

        public Propose()
        {
            BookName = string.Empty;
            Status = false;
            Tags = new List<Propose_Tag>();
            description = String.Empty;
            NewAut = String.Empty;
            NewPub = String.Empty;
            newTags = new List<Propose_NewTag>();
        }

        [Key]
        public int ID { get; set; }
        [StringLength(256)]
        public string BookName { get; set; }
        public int? ID_Aut { get; set; }
        public int? ID_Pub { get; set; }
        public int ID_Acc_Request { get; set; }
        public int PublishedYear { get; set; }
        public byte[]? Picture { get; set; }
        public String description { get; set; }
        public String NewAut { get; set; }
        public String NewPub { get; set; }
        public bool Status { get; set; }
        public virtual Author? Author { get; set; }
        public virtual Publisher? Publisher { get; set; }
        public virtual Account AccountRequest { get; set; }
        public virtual List<Propose_Tag> Tags { get; set; }
        public virtual List<Propose_NewTag> newTags { get; set; }
    }
}