using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GenericService<T> : IGenericService<Product> where T : class
    {
        private readonly Infrastructure.Repositories.IGenericService<Product> _repository;

        public GenericService(Infrastructure.Repositories.IGenericService<Product> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(params Expression<Func<Product, object>>[] includes)
        {
            return await _repository.GetAllAsync(includes);
        }

        public async Task<Product> GetByIdAsync(int id, params Expression<Func<Product, object>>[] includes)
        {
            return await _repository.GetByIdAsync(id, includes);
        }

        public async Task AddAsync(Product entity)
        {
            // Misalnya ada business logic sebelum insert
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(Product entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}
