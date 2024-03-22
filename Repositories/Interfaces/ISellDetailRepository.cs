using backend.Models;
using backend.Models.DTOs;

namespace backend.Repositories.Interfaces {
    public interface ISellDetailRepository {
        public Task<List<SellDetail>?> Get(long sellId);
        public Task<SellDetail?> GetById(long id,long sellId);
        public Task<(SellDetail,int)> Update(
            long id,
            long sellId,
            SellDetail sell
        );
        public Task<int> Delete(SellDetail toDelete);
    }
}
