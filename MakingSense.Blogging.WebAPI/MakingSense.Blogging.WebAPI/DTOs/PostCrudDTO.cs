using System;


namespace MakingSense.Blogging.WebAPI.DTOs
{
    public class PostCrudDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Enums.States State { get; set; }
    }
}
