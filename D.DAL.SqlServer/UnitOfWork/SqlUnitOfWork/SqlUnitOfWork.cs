﻿using C.Repository.Common;
using C.Repository.Repositories;
using D.DAL.SqlServer.Context;
using D.DAL.SqlServer.Infrastructure;

namespace D.DAL.SqlServer.UnitOfWork.SqlUnitOfWork;

public class SqlUnitOfWork(string connectionString, AppDbContext context) : IUnitOfWork
{
    private readonly string _connectionString = connectionString;
    private readonly AppDbContext _context = context;
    public SqlUserRepository _userRepository;
    public SqlBookRepository _bookRepository;

    public IUserRepository UserRepository => _userRepository ?? new SqlUserRepository(_connectionString, _context);

    public IBookRepository BookRepository => _bookRepository ?? new SqlBookRepository(_connectionString, _context);

    public async Task<int> SaveChange()
    {
        return await _context.SaveChangesAsync();
    }
}
