using backend.Models.DTOs;
using backend.Models.Requests;

namespace backend.Services.Interfaces {
    public interface IProductService {
        public Task<List<ProductDTO>> Get(string? filterName);
        public Task<ProductDTO?> GetById(long id);
        public Task<ProductDTO> Create(CreateProductRequest req);
        public Task Update(long id,ProductDTO dto);
        public Task Delete(long id);
    }
}
