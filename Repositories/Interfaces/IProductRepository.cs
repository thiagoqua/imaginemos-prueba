using backend.Models;

namespace backend.Repositories.Interfaces {
    public interface IProductRepository {
        public Task<List<Product>> Get(string? filterName);
        public Task<Product?> GetById(long id);
        public Task<Product> Create(Product product);
        public Task<int> Update(Product product);
        public Task<int> Delete(Product product);
    }
}
