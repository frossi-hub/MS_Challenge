using System;


namespace MakingSense.Blogging.WebAPI.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public int IdOwner { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public Enums.States State { get; set; }
        public OwnerDto Owner { get; set; }
    }
}
