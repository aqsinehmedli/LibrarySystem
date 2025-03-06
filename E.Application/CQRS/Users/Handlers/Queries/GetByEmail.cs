using A.Common.GlobalResponses;
using A.Common.GlobalResponses.Generics;
using B.Domain.Entities;
using B.Domain.Enums;
using C.Repository.Common;
using E.Application.CQRS.Users.DTOs;
using MediatR;

namespace E.Application.CQRS.Users.Handlers.Queries;

public class GetByEmail
{
    public record struct Query : IRequest<Result<GetByEmailDto>>
    {
        public string Email { get; set; }

    }
    public sealed class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Query, Result<GetByEmailDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result<GetByEmailDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
            if (currentUser == null)
            {
                throw new Exception("User is not exist with provided email");
            }
            var response = new GetByEmailDto()
            {
                Id = currentUser.Id,
                Name = currentUser.Name,
                UserName = currentUser.UserName,
                Surname = currentUser.Surname,
                FatherName = currentUser.FatherName,
                Email = currentUser.Email,
                Password = currentUser.PasswordHash,
                Address = currentUser.Address,
                MobilePhone = currentUser.MobilePhone,
                CardNumber = currentUser.CardNumber,
                TableNumber = currentUser.TableNumber,
                Birthdate = currentUser.Birthdate,
                DateOfEmployment = currentUser.DateOfEmployment,
                DateOfDissmissal = currentUser.DateOfDissmissal,
                Note = currentUser.Note,
                Gender = Enum.TryParse<Gender>(currentUser.Gender.ToString(), out var genderValue) ? genderValue : Gender.Male,
                UserType = Enum.TryParse<UserType>(currentUser.UserType.ToString(), out var userTypeValue) ? userTypeValue : UserType.User
            };
            return new Result<GetByEmailDto> { Data = response, Errors = [],IsSuccess=true };
        }
    }
}