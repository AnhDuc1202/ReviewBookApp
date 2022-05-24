using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ReviewBook.API.Data.Entities;

namespace ReviewBook.API.DTOs
{
    public class UserPropose_TagDTOs
    {
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(2048)]
        public string Description { get; set; }
    }
}