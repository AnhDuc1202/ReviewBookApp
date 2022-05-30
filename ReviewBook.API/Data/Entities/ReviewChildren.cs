

namespace ReviewBook.API.Data.Entities
{
    public class ReviewChildren
    {
        public ReviewChildren()
        {
            Content = string.Empty;
            Date = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public int ID_Acc { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int Id_parent { get; set; }
        public virtual Account Account { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual Review ReviewParent { get; set; }
    }
}