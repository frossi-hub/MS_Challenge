using System;
using System.Collections.Generic;
using System.Linq;
using MakingSense.Blogging.WebAPI.DataAccess;
using MakingSense.Blogging.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using MakingSense.Blogging.WebAPI.Commons;

namespace MakingSense.Blogging.WebAPI.Services
{
    public interface IPostService
    {

        Post Create(Post post);
        void Delete(int idPost, int idOwner);
        IEnumerable<Post> GetAll(int? idOwner = null);
        IEnumerable<Post> GetDraft(int idOwner);
        IEnumerable<Post> Filter(string text, int? idOwner = null);

    }
    public class PostService : IPostService
    {

        private DataContext _context;

        public PostService(DataContext context)
        {
            _context = context;
        }

        public Post Create(Post post)
        {
            if (string.IsNullOrWhiteSpace(post.Title))
            {
                throw new Exception("Title is required");
            }

            if (string.IsNullOrWhiteSpace(post.Content))
            {
                throw new Exception("Content is required");
            }
                
            post.CreationDate = DateTime.Now;

            _context.Posts.Add(post);
            _context.SaveChanges();

            return post;
        }

        public void Delete(int idPost, int idOwner)
        {
            var post = _context.Posts.Find(idPost);

            if(post.IdOwner != idOwner)
            {
                throw new Exception("Invalid action");
            }

            post.State = Enums.States.Delete;
            post.LastModificationDate = DateTime.Now;

            _context.SaveChanges();
        }

        public IEnumerable<Post> Filter(string text, int? idOwner = null)
        {
            text = text.ToLower().RemoveDiacritics();
            var query = GetAll(idOwner);

            if(!string.IsNullOrEmpty(text))
            {
                query = query.Where(x => 
                    x.Title.ToLower().RemoveDiacritics().Contains(text) ||
                    x.Content.ToLower().RemoveDiacritics().Contains(text));
            }

            return query;
        }

        public IEnumerable<Post> GetAll(int? idOwner = null)
        {

            if(idOwner.HasValue)
            {
                return _context.Posts.Include(x=>x.Owner).Where(x => x.State == Enums.States.Public || (x.State == Enums.States.Private && x.IdOwner == idOwner.Value));
            }

            return _context.Posts.Include(x => x.Owner).Where(x => x.State == Enums.States.Public);
        }

        public IEnumerable<Post> GetDraft(int idOwner)
        {
            return _context.Posts.Include(x => x.Owner).Where(x => x.State == Enums.States.Draft && x.IdOwner == idOwner);
        }
    }
}
