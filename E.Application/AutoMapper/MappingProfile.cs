using A.Common.Secutiry;
using AutoMapper;
using B.Domain.Entities;
using B.Domain.Enums;
using E.Application.CQRS.Users.DTOs;
using System.Data;
using static E.Application.CQRS.Users.Handlers.Command.Register;

namespace E.Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region User
        //CreateMap<Command, User>();
        //CreateMap<User,RegisterDto>();
        CreateMap<Command, User>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (Gender)src.Gender))
            .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => (UserType)src.UserType))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => (src.Password)));

        CreateMap<User, RegisterDto>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (int)src.Gender))
            .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => (int)src.UserType))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));

        #endregion
    }

}
