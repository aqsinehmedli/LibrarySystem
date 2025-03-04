using B.Domain.Entities;
using C.Repository.Repositories;
using D.DAL.SqlServer.Context;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace D.DAL.SqlServer.Infrastructure;

public class SqlUserRepository : BaseSqlRepository, IUserRepository
{
    private readonly AppDbContext _context;
    public SqlUserRepository(string connectionString, AppDbContext context) : base(connectionString)
    {
        _context = context;
    }
    public async Task<bool> Delete(int id, int deletedBy)
    {
        var checkSql = @"SELECT Id FROM Users WHERE Id=@id and isDeleted=0";
        var sql = $@"UPDATE Users
                  SET isDeleted = 1,
                  DeletedBy = @deletedBy,
                  DeletedDate = GETDATE()
                  WHERE Id = @id";
        using var conn = OpenConnection();
        using var transaction = conn.BeginTransaction();
        var userId = await conn.ExecuteScalarAsync<int?>(sql, new { id, deletedBy }, transaction);
        if (!userId.HasValue) return false;
        var affectedRows = await conn.ExecuteAsync(sql, new { id, deletedBy }, transaction);
        transaction.Commit();
        return affectedRows > 0;
    }
    public IQueryable<User> GetAll()
    {
        return _context.Users.OrderByDescending(c => c.CreatedDate).Where(c => c.IsDeleted == false);
    }
    public async Task<User> GetByEmailAsync(string email)
    {
        var sql = @"SELECT * FROM Users WHERE Email = @email and isDeleted = 0";
        using var conn = OpenConnection();
        return await conn.QueryFirstOrDefaultAsync<User>(sql, new { email });
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var checkSql = @"SELECT * FROM Users WHERE Id = @id and isDeleted = 0";
        using var conn = OpenConnection();
        return await conn.QueryFirstOrDefaultAsync<User>(checkSql, new { id });
    }

    public async Task RegisterAsync(User user)
    {
        var sql = @"INSERT INTO Users ([Name],[UserName],[Surname],[FatherName],[Email],[PasswordHash],[Address],[MobilePhone],[CardNumber],[TableNumber],[BirthDate],[DateOfEmployment],[DateOfDissmissal],[Note],[Gender],[UserType],[CreatedBy])
                  VALUES(@Name,@UserName,@Surname,@FatherName,@Email,@PasswordHash,@Address,@MobilePhone,@CardNumber,@TableNumber,@BirthDate,@DateOfEmployment,@DateOfDissmissal,@Note,@Gender,@UserType,@CreatedBy);SELECT SCOPE_IDENTITY()";
        using var conn = OpenConnection();
        var generetId = await conn.ExecuteScalarAsync<int>(sql, user);
        user.Id = generetId;
    }

    public void Update(User user)
    {
        var sql = @"UPDATE Users
                  SET Name = @Name,
                  UserName = @UserName,
                  Surname = @Surname,
                  FatherName = @FatherName,
                  Email = @Email,
                  PasswordHash = @PasswordHash,
                  Address = @Address,
                  MobilePhone = @MobilePhone,
                  CardNumber = @CardNumber,
                  TableNumber = @TableNumber,
                  BirthD ate = @BirthDate,
                  DateOfEmployment = @DateOfEmployment,
                  DateOfDissmissal = @DateOfDissmissal,
                  Note = @Note,
                  Gender = @Gender,
                  UserType = @UserType,
                  UpdatedBy = @UpdatedBy,
                  UpdatedDate = GETDATE()
                  WHERE Id = @Id";
        using var conn = OpenConnection();
        conn.Query(sql, user);
    }
}