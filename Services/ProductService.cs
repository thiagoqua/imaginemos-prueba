using backend.Models;
using backend.Models.DTOs;
using backend.Models.Requests;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services {
    public class ProductService : IProductService {
        private readonly IProductRepository _repo;
        private readonly DTOMapperService _mapper;

        public ProductService(IProductRepository repo, DTOMapperService mapper) {
            _repo = repo;
            _mapper = mapper;

        }

        public async Task<ProductDTO> Create(CreateProductRequest req) {
            Product toCreate = new() {
                Description = req.Description,
                Name = req.Name,
                Price = req.Price
            };

            Product ret = await _repo.Create(toCreate);

            return _mapper.mapProduct(ret);
        }

        public async Task Delete(long id) {
            Product? toDelete = await _repo.GetById(id);
            int status;

            if(toDelete == null)
                throw new ArgumentException();

            status = await _repo.Delete(toDelete);

            if(status != 1)
                throw new Exception("Entity not deleted");
        }

        public async Task<List<ProductDTO>> Get(string? filterName) {
            List<Product> res = await _repo.Get(filterName);

            return await Task.Run(() =>
                res
                .Select(p => _mapper.mapProduct(p))
                .ToList()
            );
        }

        public async Task<ProductDTO?> GetById(long id) {
            Product? product = await _repo.GetById(id);

            return product == null 
                ? null 
                : _mapper.mapProduct(product);
        }

        public async Task Update(long id, ProductDTO dto) {
            Product? toUpdate = await _repo.GetById(id);
            int status;

            if(toUpdate == null)
                throw new ArgumentException();

            toUpdate.Name = dto.Name;
            toUpdate.Price = dto.Price;
            toUpdate.Description = dto.Description;

            status = await _repo.Update(toUpdate);
            
            if(status != 1)
                throw new Exception("Entity not deleted");
        }
    }
}
