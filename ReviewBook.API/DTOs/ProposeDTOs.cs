using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class CreateProposeDTOs
    {
        public string BookName { get; set; }
        public int? ID_Aut { get; set; }
        public int? ID_Pub { get; set; }
        public int PublishedYear { get; set; }
        public byte[]? Picture { get; set; }
        public String description { get; set; }
        public String NewAut { get; set; }
        public String NewPub { get; set; }
        public List<int> List_ID_Tags { get; set; }
        public List<String> List_new_tags { get; set; }
        public Propose toProposeEntity(int ID_Acc_Request)
        {
            Propose b = new Propose();
            b.BookName = BookName;
            if (ID_Aut != null)
                b.ID_Aut = ID_Aut;
            else b.NewAut = NewAut;
            if (ID_Pub != null)
                b.ID_Pub = ID_Pub;
            else b.NewPub = NewPub;
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
        public String NewAut { get; set; }
        public String NewPub { get; set; }
        public List<int> List_ID_Tags_Remove { get; set; }
        public List<int> List_ID_Tags_Add { get; set; }
        public List<int> List_ID_new_tags { get; set; }
        public Propose toProposeEntity(int id)
        {
            Propose b = new Propose();
            b.ID = id;
            b.BookName = BookName;
            if (ID_Aut != null)
                b.ID_Aut = ID_Aut;
            else b.NewAut = NewAut;
            if (ID_Pub != null)
                b.ID_Pub = ID_Pub;
            else b.NewPub = NewPub;
            b.PublishedYear = PublishedYear;
            b.Picture = Picture;
            b.description = description;
            return b;
        }
    }
}