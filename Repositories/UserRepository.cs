using backend.Models;
using backend.Models.DTOs;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories {
    public class UserRepository : IUserRepository {
        private readonly ImaginemosDBContext _dbContext;

        public UserRepository(ImaginemosDBContext ctx){
            _dbContext = ctx;
        }

        public async Task<int> Delete(User user) {
            // por defecto hace la eliminación en cascada,
            // o sea borra también sellDetails y sell relacionadas
            return await Task.Run(() => {
                _dbContext.Users.Remove(user);
                return _dbContext.SaveChanges();
            });
        }

        public async Task<List<User>> Get(string? search) {
            if(search == null)
                return await _dbContext.Users.ToListAsync();

            return await _dbContext.Users
                            .Where(user => 
                                user.DNI.Contains(search) ||
                                user.Name.Contains(search)
                            )
                            .ToListAsync();
        }

        public async Task<User?> GetByDNI(string DNI) {
            return await _dbContext.Users
                            .FirstOrDefaultAsync(user => user.DNI == DNI);
        }

        public async Task<User?> GetById(long id) {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<int> Update(User user) {
            return await Task.Run(() => {
                _dbContext.Users.Update(user);

                return _dbContext.SaveChanges();
            });
        }
    }
}
