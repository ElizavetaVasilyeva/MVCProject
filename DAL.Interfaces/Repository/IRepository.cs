using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity:IEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int key);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(int key);
    }
}
