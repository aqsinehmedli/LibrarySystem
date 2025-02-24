using B.Domain.Entities;

namespace C.Repository.Repositories;

public interface IUserRepository
{
    Task RegisterAsync(User user);
    void Update(User user);
    Task<bool> Delete(int id, int deletedBy);
    IQueryable<User> GetAll();
    Task<User> GetByIdAsync(int id);
    Task<User> GetByEmailAsync(string email);

}
