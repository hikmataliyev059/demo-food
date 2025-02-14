using System.Linq.Expressions;
using FoodStore.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.Core.Repositories.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
    DbSet<T> Table { get; }

    IQueryable<T> GetAll(params string[] includes);

    Task<T?> GetByIdAsync(int id);

    IQueryable<T> GetWhere(Expression<Func<T, bool>> expression);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

    Task<T> AddAsync(T entity);

    Task Update(T entity);

    Task Delete(T entity);

    Task SoftDelete(T entity);

    Task<int> SaveChangesAsync();
}