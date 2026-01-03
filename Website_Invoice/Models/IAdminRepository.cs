using System.Net;

namespace Website_Invoice.Models
{
    public interface IAdminRepository<T> where T : class
    {
        Task<IEnumerable<T>?> GetListAsync();
        Task<T?> GetAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<HttpStatusCode> UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
