using C.Repository.Repositories;

namespace C.Repository.Common;

public interface IUnitOfWork
{
    public IBookRepository BookRepository { get; }
    public IUserRepository UserRepository { get; }
    Task<int> SaveChange();
}
