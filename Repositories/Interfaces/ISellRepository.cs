using backend.Models;
using backend.Models.DTOs;
using backend.Models.Responses;

namespace backend.Repositories.Interfaces {
    public interface ISellRepository {
        public Task<List<Sell>> Get(string? search);
        public Task<Sell?> GetById(long id);
        public Task<List<SellDetail>> Create(List<SellDetail> sells);
        public Task<int> Update(Sell sell);
        public Task<int> Delete(Sell sell);
    }
}
