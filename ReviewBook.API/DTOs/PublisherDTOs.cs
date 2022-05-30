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

    public class UpdateInforPublisherDTOs
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public int? Telephone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }

        public Publisher toPublisherEntity(int ID)
        {
            Publisher publisher = new Publisher();
            publisher.ID = ID;
            publisher.Name = Name;
            publisher.Address = Address;
            publisher.Telephone = Telephone;
            publisher.Email = Email;
            publisher.Website = Website;
            return publisher;
        }
    }
}