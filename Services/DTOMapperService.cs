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

        public SellDTO mapSell(Sell obj) {
            return new SellDTO {
                Id = obj.Id,
                Date = obj.date,
                Total = obj.Total,
                UserId = obj.UserId,
                UserName = obj.User.Name
            };
        }

        public UserDTO mapUser(User obj) {
            return new UserDTO {
                Id = obj.Id,
                DNI = obj.DNI,
                Name = obj.Name
            };
        }

        public SellDetailDTO mapSellDetail(SellDetail obj) {
            return new SellDetailDTO {
                Id = obj.Id,
                Price = obj.UnitaryPrice,
                ProductId = obj.ProductId,
                Quantity = obj.Quantity,
                TotalPrice = obj.Total
            };
        }
    }
}
