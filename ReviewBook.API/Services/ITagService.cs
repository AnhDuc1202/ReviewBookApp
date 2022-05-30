using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.Services
{
    public interface ITagService
    {
        public List<Tag> GetAllTags();
        public List<Tag> GetAllTagsNoBook();
        public Tag? GetTagById(int ID);
        public Tag CreateTag(Tag tag);
        public Tag? UpdateTag(Tag tag);
        public bool DeleteTag(int ID);
    }
}