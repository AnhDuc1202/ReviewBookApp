using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class CreatePublisherDTOs
    {
        public string Name { get; set; }
        public Publisher toAuthorEntity()
        {
            Publisher publisher = new Publisher();
            publisher.Name = Name;
            return publisher;
        }
    }
}