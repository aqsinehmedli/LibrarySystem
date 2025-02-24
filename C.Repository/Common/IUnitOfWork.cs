using C.Repository.Repositories;

namespace C.Repository.Common;

public class IUnitOfWork
{
    public IBookRepository BookRepository { get; }
    public IUserRepository UserRepository { get; }
}
