using A.Common.GlobalResponses.Generics;
using B.Domain.Entities;
using C.Repository.Common;
using E.Application.CQRS.Users.Command;
using E.Application.CQRS.Users.Response;
using MediatR;

namespace E.Application.CQRS.Users.Handlers;

public class CreateUserHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserRequest, Result<CreateUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        User newUser = new(request.Name, request.UserName, request.Surname, request.FatherName, request.Email, request.Password, request.Address, request.MobilePhone, request.CardNumber, request.TableNumber, request.Birthdate, request.DateOfEmployment, request.DateOfDissmissal, request.Note, request.Gender, request.UserType);
        await _unitOfWork.UserRepository.RegisterAsync(newUser);
        CreateUserResponse response = new()
        {
            Id = newUser.Id,
            Name = newUser.Name,
            UserName = newUser.UserName,
            Surname = newUser.Surname,
            FatherName = newUser.FatherName,
            Email = newUser.Email,
            Password = newUser.PasswordHash
        };
    }
}