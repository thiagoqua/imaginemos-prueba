using backend.Models;
using backend.Models.DTOs;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class SellDetailRepository : ISellDetailRepository {
        private readonly ImaginemosDBContext _dbContext;

        public SellDetailRepository(ImaginemosDBContext ctx){
            _dbContext = ctx;
        }

        public async Task<int> Delete(SellDetail toDelete) {
            return await Task.Run(() => {
                _dbContext.SellDetails.Remove(toDelete);
                return _dbContext.SaveChanges();
            });
        }

        public async Task<List<SellDetail>?> Get(long sellId) {
            bool exists = await SellExists(sellId);

            if(!exists) return null;

            return await _dbContext.SellDetails
                .Where(sd => sd.SellId == sellId)
                .ToListAsync();
        }

        public async Task<SellDetail?> GetById(long id, long sellId) {
            bool exists = await SellExists(sellId);

            if(!exists) return null;

            return await _dbContext.SellDetails
                .FirstOrDefaultAsync(sd => sd.Id == id);
        }

        public async Task<(SellDetail,int)> Update(
            long id,
            long sellId,
            SellDetail sell
        ) {
            return await Task.Run(() => {
                int status;
                
                _dbContext.SellDetails.Update(sell);
                status = _dbContext.SaveChanges();

                return (sell, status);
            });          
        }

        private async Task<bool> SellExists(long id) {
            return await _dbContext.Sells.AnyAsync(s => s.Id == id);
        }
    }
}
