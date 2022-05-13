using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public interface IPublisherService
    {
        public List<Publisher> GetAllPublishers();
        public Publisher? GetPublisherById(int ID);
        public Publisher CreatePublisher(Publisher publisher);
        public Publisher? UpdatePublisher(Publisher publisher);
        public bool DeletePublisher(int ID);
    }
}