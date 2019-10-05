using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIPChat.DAL
{
    interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> FindAll();
        Task<TEntity> FindById(Guid Id);
        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
        
    }
}
