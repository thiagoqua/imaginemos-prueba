using backend.Models;
using backend.Models.DTOs;

namespace backend.Services {
    public class DTOMapperService {
        public ProductDTO mapProduct(Product obj) {
            return new ProductDTO {
                Id = obj.Id,
                Description = obj.Description,
                Name = obj.Name,
                Price = obj.Price
            };
        }
    }
}
