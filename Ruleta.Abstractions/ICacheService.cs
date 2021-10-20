using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ruleta.Abstractions
{
    public interface ICacheService<T> where T : IEntity
    {
        bool IsCacheableEntity();
        Task<T> GetOne(int id);
        void SetOne(T entity);

        Task<IList<T>> GetList();
        void SetList(IList<T> entity);

    }

}
