using A.Common.Secutiry;
using B.Domain.Entities;
using B.Domain.Enums;
using E.Application.CQRS.Users.DTOs;
using System.Data;
using static E.Application.CQRS.Users.Handlers.Command.Register;
using AutoMapper;

namespace E.Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region User
        CreateMap<Command, User>();
        CreateMap<User, RegisterDto>();
        CreateMap<E.Application.CQRS.Users.Handlers.Command.Update.Command,User>();
        CreateMap<User,UpdateDto>();    


        #endregion
    }

}
