using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MakingSense.Blogging.WebAPI.Enums;

namespace MakingSense.Blogging.WebAPI.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public int IdOwner { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public States State { get; set; }
        [ForeignKey("IdOwner")]
        public User Owner { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
