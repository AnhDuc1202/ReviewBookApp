using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewBook.API.DTOs
{
    public class UserProposeBookDTOs
    {
        [StringLength(256)]
        public string BookName { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }
        public int ID_Acc_Request { get; set; }
        public int PublishedYear { get; set; }
        public byte[]? Picture { get; set; }
        public bool Status { get; set; }

        public UserProposeBookDTOs toEntity(){
            UserProposeBookDTOs result = new UserProposeBookDTOs();
            result.BookName = this.BookName;
            result.Author = this.Author;
            result.Publisher = this.Publisher;
            result.PublishedYear = this.PublishedYear;
            result.Picture = this.Picture;
            result.Status = false;
            return result;
        }
    }
}