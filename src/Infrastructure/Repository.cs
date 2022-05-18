using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly CinemaContext _context;
    private DbSet<T> _entities;

    public Repository(CinemaContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<T> GetByIdAsync(int id, string[] paths = null)
    {
        var model = await _entities.FindAsync(id);
        if (paths != null)
        {
            foreach (var path in paths)
            {
                _context.Entry(model).Reference(path).Load();
            }
        }
        return model;
    }

    public async Task<List<T>> GetListAsync(string[] paths = null)
    {

        var list = await _entities.ToListAsync();
        if (paths != null)
        {
            foreach (var model in list)
            {
                foreach (var path in paths)
                {
                    _context.Entry(model).Reference(path).Load();
                }
            }
        }
        return list;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _entities.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        _entities.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}