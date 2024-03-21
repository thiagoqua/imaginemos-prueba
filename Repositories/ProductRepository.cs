using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class ProductRepository : IProductRepository {
        private ImaginemosDBContext _dbContext;

        public ProductRepository(ImaginemosDBContext ctx){
            _dbContext = ctx;
        }

        public async Task<Product> Create(Product product) {
            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<int> Delete(Product product) {
            return await Task.Run(async () => {
                _dbContext.Products.Remove(product);

                return await _dbContext.SaveChangesAsync();
            });
        }

        public async Task<List<Product>> Get(string? filterName) {
            if(filterName != null)
                return await _dbContext.Products
                                .Where(prod => prod.Name.Contains(filterName))
                                .ToListAsync();

            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetById(long id) {
            return await _dbContext.Products
                    .FirstOrDefaultAsync(prod => prod.Id == id);
        }

        public async Task<int> Update(Product product) {
            return await Task.Run(() => {
                _dbContext.Products.Update(product);

                return _dbContext.SaveChanges();
            });
        }
    }
}
