using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class SellRepository : ISellRepository {
        private readonly ImaginemosDBContext _dbContext;

        public SellRepository(ImaginemosDBContext ctx){
            _dbContext = ctx;
        }

        public async Task<List<SellDetail>> Create(List<SellDetail> sells) {
            //inserta en cascada, o sea que inserta tanto las SellDetail
            //como la única Sell y el único Usuario relacionado con cada SellDetail
            await _dbContext.AddRangeAsync(sells);
            await _dbContext.SaveChangesAsync();

            return sells;
        }

        public async Task<int> Delete(Sell sell) {
            return await Task.Run(() => {
                _dbContext.Sells.Remove(sell);
                return _dbContext.SaveChanges();
            });
        }

        public async Task<List<Sell>> Get(string? search) {
            if(search == null)
                return await _dbContext.Sells
                    .Include(sell => sell.User)
                    .ToListAsync();

            return await _dbContext.Sells
                            .Include(sell => sell.User)
                            .Where(sell =>
                                sell.User.Name.Contains(search) ||
                                sell.User.DNI.Contains(search)
                             //sell.User.DNI == search
                             )
                            .ToListAsync();
        }

        public async Task<Sell?> GetById(long id) {
            return await _dbContext.Sells
                            .Include(sell => sell.User)
                            .FirstOrDefaultAsync(sell => sell.Id == id);
        }

        public async Task<int> Update(Sell sell) {
            return await Task.Run(() => {
                _dbContext.Sells.Update(sell);
                return _dbContext.SaveChanges();
            });
        }
    }
}
