using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class CreateProposeDTOs
    {
        public string BookName { get; set; }
        public int? ID_Aut { get; set; }
        public int? ID_Pub { get; set; }
        public int ID_Acc_Request { get; set; }
        public int PublishedYear { get; set; }
        public byte[]? Picture { get; set; }
        public String description { get; set; }
        public List<int> List_ID_Tags { get; set; }
        public Propose toProposeEntity()
        {
            Propose b = new Propose();
            b.BookName = BookName;
            b.ID_Aut = ID_Aut;
            b.ID_Pub = ID_Pub;
            b.ID_Acc_Request = ID_Acc_Request;
            b.PublishedYear = PublishedYear;
            b.Picture = Picture;
            b.description = description;
            return b;
        }
    }

    public class UpadateProposeDTOs
    {
        public string BookName { get; set; }
        public int? ID_Aut { get; set; }
        public int? ID_Pub { get; set; }
        public int PublishedYear { get; set; }
        public byte[]? Picture { get; set; }
        public String description { get; set; }
        public List<int> List_ID_Tags_Remove { get; set; }
        public List<int> List_ID_Tags_Add { get; set; }
        public Propose toProposeEntity(int id)
        {
            Propose b = new Propose();
            b.ID = id;
            b.BookName = BookName;
            b.ID_Aut = ID_Aut;
            b.ID_Pub = ID_Pub;
            b.PublishedYear = PublishedYear;
            b.Picture = Picture;
            b.description = description;
            return b;
        }
    }
}