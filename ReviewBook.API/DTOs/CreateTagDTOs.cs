using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class CreateTagDTOs
    {
        public string Name { get; set; }
        public Tag toTagEntity()
        {
            Tag tag = new Tag();
            tag.Name = Name;
            return tag;
        }
    }
}