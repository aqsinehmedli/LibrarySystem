﻿using A.Common;
using A.Common.GlobalResponses.Generics;
using AutoMapper;
using B.Domain.Entities;
using C.Repository.Common;
using E.Application.CQRS.Users.DTOs;
using MediatR;
using A.Common.Secutiry;
using B.Domain.Enums;
namespace E.Application.CQRS.Users.Handlers.Command;

public class Register
{
    public record struct Command : IRequest<Result<RegisterDto>>
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string CardNumber { get; set; }
        public string TableNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public DateTime DateOfDissmissal { get; set; }
        public string Note { get; set; }
        public int Gender { get; set; }
        public int UserType { get; set; }
    }
    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<Command, Result<RegisterDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;


        public async Task<Result<RegisterDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var isExist = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
            if (isExist != null) throw new Exception("User already registered with provided email");
            var newUser = new User
            {
                Name = request.Name,
                UserName = request.UserName,
                Surname = request.Surname,
                FatherName = request.FatherName,
                Email = request.Email,
                PasswordHash = request.Password, // Burada şifrenin hash'lenmiş değeri atanacak
                Address = request.Address,
                MobilePhone = request.MobilePhone,
                CardNumber = request.CardNumber,
                TableNumber = request.TableNumber,
                Birthdate = request.Birthdate,
                DateOfEmployment = request.DateOfEmployment,
                DateOfDissmissal = request.DateOfDissmissal,
                Note = request.Note,
                Gender = (Gender)request.Gender, // Gender enum dönüşümü
                UserType = (UserType)request.UserType // UserType enum dönüşümü
            };

            var hashPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);
            newUser.PasswordHash = hashPassword;
            await _unitOfWork.UserRepository.RegisterAsync(newUser);

            var response = new RegisterDto
            {
                Name = newUser.Name,
                UserName = newUser.UserName,
                Surname = newUser.Surname,
                FatherName = newUser.FatherName,
                Email = newUser.Email,
                Password = newUser.PasswordHash, // Burada şifrenin hash'lenmiş hali atanacak
                Address = newUser.Address,
                MobilePhone = newUser.MobilePhone,
                CardNumber = newUser.CardNumber,
                TableNumber = newUser.TableNumber,
                Birthdate = newUser.Birthdate,
                DateOfEmployment = newUser.DateOfEmployment,
                DateOfDissmissal = newUser.DateOfDissmissal,
                Note = newUser.Note,
                Gender = (int)newUser.Gender, // Gender enum'dan int'e dönüşüm
                UserType = (int)newUser.UserType // UserType enum'dan int'e dönüşüm
            };

            return new Result<RegisterDto> { Data = response, Errors = [], IsSuccess = true };
        }
    }
}
