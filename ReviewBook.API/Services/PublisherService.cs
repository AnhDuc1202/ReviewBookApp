using Microsoft.EntityFrameworkCore;
using ReviewBook.API.Data;
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly DataContext _context;

        public PublisherService(DataContext context)
        {
            _context = context;
        }

        public int? CheckName(string Name)
        {
            var check = _context.Publishers.FirstOrDefault(c => c.Name.Trim().ToLower() == Name.Trim().ToLower());
            if (check == null) return null;
            return check.ID;
        }

        public Publisher CreatePublisher(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
            return publisher;
        }

        public bool DeletePublisher(int ID)
        {
            var currentPublisher = GetPublisherById(ID);
            if (currentPublisher == null) return false;
            _context.Publishers.Remove(currentPublisher);
            _context.SaveChanges();
            return true;
        }

        public List<Publisher> GetAllPublishers()
        {
            return _context.Publishers
            .AsNoTracking()
            .ToList();
        }

        public Publisher? GetPublisherById(int ID)
        {
            return _context.Publishers
            .Include(a => a.Books)
                .ThenInclude(b => b.author)
            .Include(a => a.Books)
                .ThenInclude(c => c.Tags)
                    .ThenInclude(d => d.tag)
            .AsNoTracking()
            .FirstOrDefault(p => p.ID == ID);
        }

        public Publisher? UpdatePublisher(Publisher publisher)
        {
            var currentPublisher = _context.Publishers.FirstOrDefault(c => c.ID == publisher.ID);
            if (currentPublisher == null) return null;
            currentPublisher.Name = publisher.Name;
            currentPublisher.Email = publisher.Email;
            currentPublisher.Address = publisher.Address;
            currentPublisher.Telephone = publisher.Telephone;
            currentPublisher.Website = publisher.Website;

            _context.Publishers.Update(currentPublisher);
            _context.SaveChanges();
            return currentPublisher;
        }
    }
}