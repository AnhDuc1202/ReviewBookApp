
using System.ComponentModel.DataAnnotations;

namespace ReviewBook.API.DTOs
{
    public class UserWriteReviewDTOs
    {
        // public int Id { get; set; }
        public int ID_Acc { get; set; }
        public int ID_Book { get; set; }
        [StringLength(2048)]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int Rate { get; set; }
    }
}