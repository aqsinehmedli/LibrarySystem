using A.Common.Exceptions;
using B.Domain.Entities;
using C.Repository.Repositories;
using D.DAL.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace D.DAL.SqlServer.Infrastructure;

public class SqlBookRepository : BaseSqlRepository, IBookRepository
{
    private readonly AppDbContext _context;
    public SqlBookRepository(string connectionString, AppDbContext context) : base(connectionString)
    {
        _context = context;
    }

    public async Task AddAsync(Book book)
    {
        book.CreatedDate = DateTime.Now;
        book.CreatedBy = 1;
        await _context.Books.AddAsync(book);
        _context.SaveChanges();
    }

    public IQueryable<Book> GetAll()
    {
        return _context.Books.Where(b=>b.IsDeleted == false);
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);
    }

    public void Update(Book book)
    {
        book.UpdatedDate = DateTime.Now;
        book.UpdatedBy = 1;
        _context.Books.Update(book);
        _context.SaveChanges();
    }

    public async Task<bool> Delete(int id)
    {
        var currentBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (currentBook == null)
        {
            throw new BadRequestException("User is not found!");
        }
        currentBook.IsDeleted = true;
        currentBook.DeletedBy = 1;
        _context.Update(currentBook);
        _context.SaveChanges();
        return true;
    }
}
