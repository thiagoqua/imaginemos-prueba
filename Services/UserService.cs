using backend.Models;
using backend.Models.DTOs;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services {
    public class UserService : IUserService {
        private readonly IUserRepository _repo;
        private readonly DTOMapperService _mapper;

        public UserService(IUserRepository repo, DTOMapperService mapper) {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task Delete(long id) {
            int status;
            User toDelete = await _repo.GetById(id)
                ?? throw new ArgumentException();

            status = await _repo.Delete(toDelete);

            if(status < 1)
                throw new Exception("User not deleted");
        }

        public async Task<List<UserDTO>> Get(string? search) {
            List<User> res = await _repo.Get(search);

            return await Task.Run(() => {
                return res
                    .Select(user => _mapper.mapUser(user))
                    .ToList();
            });
        }

        public async Task<UserDTO?> GetByDNI(string DNI) {
            User? ret = await _repo.GetByDNI(DNI);

            return ret == null
                ? null
                : _mapper.mapUser(ret);
        }

        public async Task<UserDTO?> GetById(long id) {
            User? res = await _repo.GetById(id);

            return res == null
                ? null
                : _mapper.mapUser(res);
        }

        public async Task Update(long id,UserDTO userDTO) {
            int status;
            User toDelete = await _repo.GetById(id)
                ?? throw new ArgumentException();

            toDelete.Name = userDTO.Name;
            toDelete.DNI = userDTO.DNI;

            status = await _repo.Update(toDelete);

            if(status != 1)
                throw new Exception("User not updated");
        }
    }
}
