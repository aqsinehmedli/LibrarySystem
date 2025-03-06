using A.Common.GlobalResponses;
using A.Common.GlobalResponses.Generics;
using C.Repository.Common;
using MediatR;

namespace E.Application.CQRS.Users.Handlers.Command;

public class Delete
{
    public class Command : IRequest<Result<Unit>>
    {
        public int Id { get; set; }
    }
    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _unitOfWork.UserRepository.Delete(request.Id,1);
            return new Result<Unit> { Errors = [],IsSuccess=true };
        }
    }
}
