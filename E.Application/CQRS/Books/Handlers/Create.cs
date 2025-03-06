using A.Common.Exceptions;
using A.Common.GlobalResponses;
using A.Common.GlobalResponses.Generics;
using B.Domain.Entities;
using B.Domain.Enums;
using C.Repository.Common;
using E.Application.CQRS.Books.DTOs;
using MediatR;

namespace E.Application.CQRS.Books.Handlers;

public class Create
{
    public record struct Command : IRequest<Result<CreateBookDto>>
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid? CoverPhoto { get; set; }
        public User UserId { get; set; }
        public bool? ShowOnFirstScreen { get; set; }
        public Languages? Language { get; set; }
    }
    public sealed class Handler(IMediator mediator, IUnitOfWork unitOfWork) : IRequestHandler<Command, Result<CreateBookDto>>
    {
        private readonly IMediator _mediator = mediator;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result<CreateBookDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var isExist = await _unitOfWork.BookRepository.GetByIdAsync(request.Id);
            if (isExist == null)
            {
                throw new BadRequestException("Book is not found!");
            }
            var newBook = new Book
            {
                Id = request.Id,
                Author = request.Author,
                Description = request.Description,
                Price = request.Price,
                CoverPhoto = request.CoverPhoto,
                UserId = request.UserId,
                ShowOnFirstScreen = request.ShowOnFirstScreen,
                Language = Enum.TryParse<Languages>(isExist.Language.ToString(), out var languageValue) ? languageValue : Languages.az,
            };
            await _unitOfWork.BookRepository.AddAsync(newBook);

            var response = new CreateBookDto()
            {
                Author = newBook.Author,
                Description = newBook.Description,
                Price = newBook.Price,
                CoverPhoto = newBook.CoverPhoto,
                UserId = newBook.UserId,
                ShowOnFirstScreen = newBook.ShowOnFirstScreen,
                Language = (int)newBook.Language,

            };
            return new Result<CreateBookDto> { Errors = [], Data = response, IsSuccess = true };
        }
    }
}
