using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MakingSense.Blogging.WebAPI.Services;
using AutoMapper;
using MakingSense.Blogging.WebAPI.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MakingSense.Blogging.WebAPI.Commons;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using MakingSense.Blogging.WebAPI.Entities;

namespace MakingSense.Blogging.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;


        public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginDTO loginDTO)
        {
            var user = _userService.Authenticate(loginDTO.Username, loginDTO.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var userDTO = _mapper.Map<UserDto>(user);

            userDTO.Token = tokenString;

            return Ok(userDTO);
        }

        //// GET: api/Users
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    return Ok(_mapper.Map<List<UserDto>>(_userService.GetAll().ToList()));
        //}

        //// GET: api/Users/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/Users
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]RegisterDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);

            try
            {
                _userService.Create(user, registerDto.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



    }
}
