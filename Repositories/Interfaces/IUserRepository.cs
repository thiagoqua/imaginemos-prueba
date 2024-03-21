using backend.Models;
using backend.Models.DTOs;

namespace backend.Repositories.Interfaces {
    public interface IUserRepository {
        public Task<User?> GetById(long id);
        public Task<User?> GetByDNI(string DNI);
        public Task<List<User>> Get(string? search);
        public Task<int> Update(User user);
        public Task<int> Delete(User user);
    }
}
