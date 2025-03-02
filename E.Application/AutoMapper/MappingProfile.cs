using AutoMapper;
using B.Domain.Entities;
using E.Application.CQRS.Users.DTOs;
using System.Data;
using static E.Application.CQRS.Users.Handlers.Command.Register;

namespace E.Application.AutoMapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        #region User
        CreateMap<Command, User>();
        CreateMap<User,RegisterDto>();

        #endregion
    }

}
