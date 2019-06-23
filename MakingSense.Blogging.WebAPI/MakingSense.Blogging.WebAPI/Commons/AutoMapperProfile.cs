using AutoMapper;
using MakingSense.Blogging.WebAPI.DTOs;
using MakingSense.Blogging.WebAPI.Entities;

namespace MakingSense.Blogging.WebAPI.Commons
{

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<User, RegisterDto>();
            CreateMap<RegisterDto, User>();
            CreateMap<User, OwnerDto>();
            CreateMap<OwnerDto, User>();
            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>();
            CreateMap<Post, PostCrudDTO>();
            CreateMap<PostCrudDTO, Post>();
        }
    }
}
