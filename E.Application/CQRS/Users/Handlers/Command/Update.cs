using A.Common.GlobalResponses;
using A.Common.GlobalResponses.Generics;
using AutoMapper;
using B.Domain.Enums;
using C.Repository.Common;
using E.Application.CQRS.Users.DTOs;
using MediatR;

namespace E.Application.CQRS.Users.Handlers.Command;

public class Update
{
    public record struct Command : IRequest<Result<UpdateDto>>
    {
        public int Id { get; set; }
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
    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<Command, Result<UpdateDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;



        public async Task<Result<UpdateDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (currentUser == null) throw new Exception($"User is not found! { request.Id}");
            currentUser.Name = request.Name;
            currentUser.UserName = request.UserName;
            currentUser.Surname = request.Surname;
            currentUser.FatherName = request.FatherName;
            currentUser.Email = request.Email;
            currentUser.PasswordHash = request.Password;
            currentUser.Address = request.Address;
            currentUser.MobilePhone = request.MobilePhone;
            currentUser.CardNumber = request.CardNumber;
            currentUser.TableNumber = request.TableNumber;
            currentUser.DateOfEmployment = request.DateOfEmployment;
            currentUser.DateOfDissmissal = request.DateOfDissmissal;
            currentUser.Note = request.Note;
            if (Enum.TryParse<Gender>(request.Gender.ToString(), out var genderValue))
            {
                currentUser.Gender = genderValue;
            }
            else
            {
                currentUser.Gender = Gender.Male;
            }
            if (Enum.TryParse<UserType>(request.UserType.ToString(), out var userTypeValue))
            {
                currentUser.UserType = userTypeValue;
            }
            else
            {
                currentUser.UserType = UserType.User;
            }
            _unitOfWork.UserRepository.Update(currentUser);
            var result = _mapper.Map<UpdateDto>(currentUser);
            return new Result<UpdateDto>
            {
                Data = result,
                Errors = [],
                IsSuccess = true,

            };

        }
    }
}
