using Ruleta.Abstractions;
using Ruleta.Repository;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ruleta.Application
{
    public interface IAplication<T> :  ICrud<T>
    {

    }
    public class Aplication<T> : IAplication<T> where T: IEntity
    {
        IRepository<T> _repository;
        public Aplication(IRepository<T> repository)
        {
            _repository = repository;
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
        public void DeleteAsync(int id)
        {
            _repository.DeleteAsync(id);
        }
        public IList<T> GetAll()
        {
            return _repository.GetAll();
        }
        public Task<IList<T>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }
        public T GetById(int id)
        {
            return _repository.GetById(id);
        }
        public Task<T> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }
        public T Save(T entity)
        {
            return _repository.Save(entity);
        }
        public Task<T> SaveAsync(T entity)
        {
            return _repository.SaveAsync(entity);
        }

    }
}
