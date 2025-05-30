using Microsoft.EntityFrameworkCore;
using System;
using WebApiRedArbor.Context;
using WebApiRedArbor.Interfaces;

namespace WebApiRedArbor.Repositories
{
    public class RepositoryGeneric<T> : IRepositoryGeneric<T> where T : class
    {
        protected readonly ApplicationDbContext _contexto;
        protected readonly DbSet<T> _dbSet;

        public RepositoryGeneric(ApplicationDbContext contexto)
        {
            _contexto = contexto;
            _dbSet = _contexto.Set<T>();
        }

        public async Task<IEnumerable<T>> ListAsync() => await _dbSet.ToListAsync();

        public async Task<T> GetIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<T> AddAsync(T entidad)
        {
            await _dbSet.AddAsync(entidad);
            await _contexto.SaveChangesAsync();
            return entidad;
        }

        public async Task UpdateAsync(T entidad)
        {
            _dbSet.Update(entidad);
            await _contexto.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entidad = await GetIdAsync(id);
            _dbSet.Remove(entidad);
            await _contexto.SaveChangesAsync();
        }
    }
}

