
using System.ComponentModel.DataAnnotations;
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class ReviewDTOs
    {
        public int ID_Acc { get; set; }
        public int ID_Book { get; set; }
        public string Content { get; set; }
        public Review toEntitiesReviewBook()
        {
            Review r = new Review();
            r.ID_Acc = ID_Acc;
            r.ID_Book = ID_Book;
            r.Content = Content;
            return r;
        }
        public Review toEntitiesReviewBook(int id)
        {
            Review r = new Review();
            r.Id = id;
            r.ID_Acc = ID_Acc;
            r.ID_Book = ID_Book;
            r.Content = Content;
            return r;
        }
    }
}