using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class CreateReviewDTOs
    {
        public int ID_Book { get; set; }
        public int Rate { get; set; }
        public string Content { get; set; }
        public Review toEntitiesReviewBook(int ID_Acc)
        {
            Review r = new Review();
            r.ID_Acc = ID_Acc;
            r.ID_Book = ID_Book;
            r.Content = Content;
            r.Rate = Rate;
            return r;
        }
    }

    public class updateReviewDTOs
    {
        public int Rate { get; set; }
        public string Content { get; set; }
        public Review toEntitiesReviewBook(int idReview, int ID_Acc)
        {
            Review r = new Review();
            r.Id = idReview;
            r.ID_Acc = ID_Acc;
            r.Content = Content;
            r.Rate = Rate;
            return r;
        }
    }
}