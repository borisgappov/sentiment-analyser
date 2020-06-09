using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SentimentAnalyser.Data.Interfaces;

namespace SentimentAnalyser.Data.Core
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        [ExcludeFromCodeCoverage]
        protected GenericRepository()
        {
        }

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Attach(T model, EntityState? entityState = null)
        {
            _context.Attach(model);
            if (entityState != null) _context.Entry(model).State = entityState.Value;
        }

        public virtual void Create(T model)
        {
            _context.Entry(model).State = EntityState.Added;
        }

        public virtual void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Detach(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached) _dbSet.Attach(entityToDelete);

            _dbSet.Remove(entityToDelete);
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<List<T>> AllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public ApplicationDbContext GetContext()
        {
            return _context;
        }
    }
}