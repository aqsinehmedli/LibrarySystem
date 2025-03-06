using B.Domain.Entities;

namespace C.Repository.Repositories;

public interface IBookRepository
{
    Task AddAsync(Book book);
    void Update(Book book);
    Task<bool> Delete(int id);
    IQueryable<Book> GetAll();
    Task<Book> GetByIdAsync(int id);
}
