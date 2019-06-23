using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MakingSense.Blogging.WebAPI.DTOs;
using MakingSense.Blogging.WebAPI.Entities;
using MakingSense.Blogging.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakingSense.Blogging.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IPostService _postService;
        private IMapper _mapper;



        public PostsController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;

        }


        // GET: api/Posts
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Ok(_mapper.Map<List<PostDTO>>(_postService.GetAll().ToList()));
            }

            return Ok(_mapper.Map<List<PostDTO>>(_postService.GetAll(Convert.ToInt32(User.Identity.Name)).ToList()));
        }

        // GET: api/Posts/{filter}
        [AllowAnonymous]
        [HttpGet("{filter}")]
        public IActionResult Get(string filter)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Ok(_mapper.Map<List<PostDTO>>(_postService.Filter(filter).ToList()));
            }

            return Ok(_mapper.Map<List<PostDTO>>(_postService.Filter(filter, Convert.ToInt32(User.Identity.Name)).ToList()));
        }

        // POST: api/Posts
        [HttpPost]
        public IActionResult Post([FromBody] PostCrudDTO postDTO)
        {
            var post = _mapper.Map<Post>(postDTO);

            post.IdOwner = Convert.ToInt32(User.Identity.Name);

            try
            {
                _postService.Create(post);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //// PUT: api/Posts/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _postService.Delete(id, Convert.ToInt32(User.Identity.Name));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
