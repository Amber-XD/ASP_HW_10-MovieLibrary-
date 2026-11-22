using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Repository
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task CreateAsync(T entity);
        public Task UpdateAsync(T entity, string userId);
        public Task DeleteAsync(T entity);

        public Task<T?> GetByIdAsync(int id);

        public Task SaveChangesAsync();
    }
}
