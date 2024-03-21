using backend.Models.DTOs;

namespace backend.Services.Interfaces {
    public interface IUserService {
        public Task<UserDTO?> GetById(long id);
        public Task<UserDTO?> GetByDNI(string DNI);
        public Task<List<UserDTO>> Get(string? search);
        public Task Update(long id,UserDTO userDTO);
        public Task Delete(long id);
    }
}
