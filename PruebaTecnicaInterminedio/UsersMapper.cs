namespace PruebaTecnicaInterminedio
{
    using AutoMapper;
    using PruebaTecnicaInterminedio.Models;
    using PruebaTecnicaInterminedio.Models.Dto;
    using System.Runtime;

    public class UsersMapper : Profile
    {
        public UsersMapper()
        {
            CreateMap<Usuario, UserDto>().ReverseMap();
            CreateMap<Usuario, UpdateUserDto>().ReverseMap();
            CreateMap<Usuario, DeleteUserDto>().ReverseMap();
            CreateMap<Usuario, CreateUserDto>().ReverseMap();
        }
    }
}
