using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class UpdateInforAuthorDTOs
    {
        public string Name { get; set; }
        public string? Stage_Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Description { get; set; }
        public Author toAuthorEntity(int ID)
        {
            Author author = new Author();
            author.Id = ID;
            author.Name = Name;
            author.Stage_Name = Stage_Name;
            author.Birthday = Birthday;
            author.Description = Description;
            return author;
        }
    }
}