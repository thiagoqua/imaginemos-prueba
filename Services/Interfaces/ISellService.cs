using backend.Models.DTOs;
using backend.Models.Requests;
using backend.Models.Responses;

namespace backend.Services.Interfaces {
    public interface ISellService {
        public Task<List<SellDTO>> Get(
            string? search,
            string? minDate,
            string? maxDate
        );
        public Task<SellDTO?> GetById(long id);
        public Task<CreateSellResponse> Create(CreateSellRequest req);
        public Task Update(long id, SellDTO dto);
        public Task Delete(long id);
        public Task UpdateTotalPrice(long id,double price);
    }
}
