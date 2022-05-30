using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class CreateReviewChildrenDTOs
    {
        public string Content { get; set; }
        public int Id_parent { get; set; }
        public ReviewChildren toEntitiesReviewChildren(int ID_Acc)
        {
            ReviewChildren r = new ReviewChildren();
            r.ID_Acc = ID_Acc;
            r.Id_parent = Id_parent;
            r.Content = Content;
            return r;
        }
    }
    public class UpdateReviewChildrenDTOs
    {
        public string Content { get; set; }
        public ReviewChildren toEntitiesReviewChildren(int idReview, int ID_Acc)
        {
            ReviewChildren r = new ReviewChildren();
            r.Id = idReview;
            r.ID_Acc = ID_Acc;
            r.Content = Content;
            return r;
        }
    }
}