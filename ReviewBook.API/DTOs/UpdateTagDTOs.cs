using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class UpdateTagDTOs
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Tag toTagEntity(int ID)
        {
            Tag tag = new Tag();
            tag.ID = ID;
            tag.Name = Name;
            tag.Description = Description;
            return tag;
        }
    }
}