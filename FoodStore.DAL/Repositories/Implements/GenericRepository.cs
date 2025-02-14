using System.Linq.Expressions;
using FoodStore.Core.Entities.Common;
using FoodStore.Core.Repositories.Interfaces;
using FoodStore.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.DAL.Repositories.Implements;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
{
    private readonly FoodStoreDbContext _context;

    public GenericRepository(FoodStoreDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public IQueryable<T> GetAll(params string[] includes)
    {
        IQueryable<T> query = Table.Where(x => !x.IsDeleted);

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var entity = await Table.FindAsync(id);
        if (entity == null || entity.IsDeleted) return null;
        return entity;
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
    {
        return Table.Where(x => !x.IsDeleted).Where(expression);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await Table.Where(x => !x.IsDeleted).AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
    {
        return predicate is null
            ? await Table.CountAsync(x => !x.IsDeleted)
            : await Table.Where(x => !x.IsDeleted).CountAsync(predicate);
    }

    public async Task<T> AddAsync(T entity)
    {
        await Table.AddAsync(entity);
        return entity;
    }

    public Task Update(T entity)
    {
        Table.Update(entity);
        return Task.CompletedTask;
    }

    public Task Delete(T entity)
    {
        Table.Remove(entity);
        return Task.CompletedTask;
    }

    public Task SoftDelete(T entity)
    {
        entity.IsDeleted = true;
        Table.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}