using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MIPChat.DAL
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> FindAll(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "");
        TEntity FindById(Guid Id);
        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
        void Delete(Guid id);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        
    }
}
