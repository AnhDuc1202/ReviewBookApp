using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class ReviewChildrenDTOs
    {
        public int ID_Acc { get; set; }
        public string Content { get; set; }
        public DateTime time { get; set; }
        public int Id_parent { get; set; }
        public ReviewChildren toEntitiesReviewChildren()
        {
            ReviewChildren r = new ReviewChildren();
            r.ID_Acc = ID_Acc;
            r.Id_parent = Id_parent;
            r.Content = Content;
            r.Date = time;
            return r;
        }
        public ReviewChildren toEntitiesReviewChildren(int id)
        {
            ReviewChildren r = new ReviewChildren();
            r.Id = id;
            r.ID_Acc = ID_Acc;
            r.Id_parent = Id_parent;
            r.Content = Content;
            r.Date = time;
            return r;
        }
    }
}