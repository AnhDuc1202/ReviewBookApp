using Microsoft.EntityFrameworkCore;
using ReviewBook.API.Data;
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public class TagService : ITagService
    {
        private readonly DataContext _context;

        public TagService(DataContext context)
        {
            _context = context;
        }

        public Tag CreateTag(Tag tag)
        {
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return tag;
        }

        public bool DeleteTag(int ID)
        {
            var currentTag = GetTagById(ID);
            if (currentTag == null) return false;
            _context.Tags.Remove(currentTag);
            _context.SaveChanges();
            return true;
        }

        public List<Tag> GetAllTags()
        {
            return _context.Tags
            .Include(a => a.Books)
            .ThenInclude(b => b.book)
            .AsNoTracking()
            .ToList();
        }
        public List<Tag> GetAllTagsNoBook()
        {
            return _context.Tags
            .AsNoTracking()
            .ToList();
        }

        public Tag? GetTagById(int ID)
        {
            return _context.Tags
            .Include(a => a.Books)
                .ThenInclude(a1 => a1.book)
                    .ThenInclude(a2 => a2.author)
            .Include(a => a.Books)
                .ThenInclude(a1 => a1.book)
                    .ThenInclude(a3 => a3.publisher)
            .AsNoTracking()
            .FirstOrDefault(p => p.ID == ID);
        }

        public Tag? UpdateTag(Tag tag)
        {
            var currentTag = _context.Tags.FirstOrDefault(c => c.ID == tag.ID);
            if (currentTag == null) return null;
            currentTag.Name = tag.Name;
            currentTag.Description = tag.Description;
            _context.Tags.Update(currentTag);
            _context.SaveChanges();
            return currentTag;
        }
    }
}