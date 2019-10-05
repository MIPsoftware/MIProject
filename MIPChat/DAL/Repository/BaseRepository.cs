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

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<IEnumerable<TEntity>> FindAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> FindById(Guid Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
             _dbSet.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}