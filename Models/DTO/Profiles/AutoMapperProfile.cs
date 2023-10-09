using AutoMapper;
using Web.Models;
using WebApi.Models.DTO.RequestDTO;
using WebApi.Models.DTO.ResponseDTO;


namespace WebApi.Models.DTO.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // From DB => Client
            CreateMap<User, UserResponseDTO>();
            CreateMap<Posts, UserResponseDTO>();
            CreateMap<Comments, UserResponseDTO>();

            // From Client => DB
            CreateMap<UserRequestDTO, User>();
            CreateMap<PostRequestDTO, Posts>();
            CreateMap<CommentRequestDTO, Comments>();
        }
    }
}