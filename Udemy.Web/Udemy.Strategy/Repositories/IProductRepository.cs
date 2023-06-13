using System.Collections.Generic;
using System.Threading.Tasks;
using Udemy.Strategy.Models;

namespace Udemy.Strategy.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetById(string id);

        Task<List<Product>> GetAllByUserId(string userId);

        Task<Product> Add(Product product);

        Task Update(Product product);

        Task Delete(Product product);
    }
}
