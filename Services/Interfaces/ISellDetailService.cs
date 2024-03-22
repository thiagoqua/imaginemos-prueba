using backend.Models.DTOs;

namespace backend.Services.Interfaces {
    public interface ISellDetailService {
        public Task<List<SellDetailDTO>?> Get(long sellId);
        public Task<SellDetailDTO?> GetById(long id,long sellId);
        public Task<SellDetailDTO?> Update(
            long id, 
            long sellId,
            SellDetailDTO dto
        );
        public Task Delete(long id, long sellId);
    }
}
