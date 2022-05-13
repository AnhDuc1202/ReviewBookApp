using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class CreateAuthorDTOs
    {
        public string Name { get; set; }
        public Author toAuthorEntity()
        {
            Author author = new Author();
            author.Name = Name;
            return author;
        }
    }
}