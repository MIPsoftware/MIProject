using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MIPChat.DAL
{
    public class BaseRepository<TDBContext, TEntity> : IRepository<TEntity>
        where TDBContext : DbContext
        where TEntity : class
    {
        protected readonly TDBContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        public BaseRepository(TDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
            
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            foreach(TEntity entity in entities){
                 if (_context.Entry(entity).State == EntityState.Detached)
                 {
                    _dbSet.Attach(entity);
                 }
            }

            _dbSet.RemoveRange(entities);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> FindById(Guid Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
             _dbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}