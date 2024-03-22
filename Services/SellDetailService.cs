using backend.Models;
using backend.Models.DTOs;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace backend.Services {
    public class SellDetailService : ISellDetailService {
        private readonly ISellDetailRepository _repo;
        private readonly DTOMapperService _mapper;
        private readonly ISellService _sellService;

        public SellDetailService(
            ISellDetailRepository repo,
            DTOMapperService mapper,
            ISellService sellService) {
            _repo = repo;
            _mapper = mapper;
            _sellService = sellService;

        }

        public async Task Delete(long id, long sellId) {
            int status;
            double total;
            SellDetail toDelete = await _repo.GetById(id, sellId)
                ?? throw new ArgumentException();

            total = -toDelete.Total;
            status = await _repo.Delete(toDelete);

            if(status < 1)
                throw new Exception("Sell detail not deleted");

            await _sellService.UpdateTotalPrice(sellId,total);
        }

        public async Task<List<SellDetailDTO>?> Get(long sellId) {
            List<SellDetail>? res = await _repo.Get(sellId);

            if(res == null)
                return null;

            return await Task.Run(() => res
                .Select(sd => _mapper.mapSellDetail(sd))
                .ToList()
            );
        }

        public async Task<SellDetailDTO?> GetById(long id, long sellId) {
            SellDetail? res = await _repo.GetById(id, sellId);

            return res == null
                ? null
                : _mapper.mapSellDetail(res);
        }

        public async Task<SellDetailDTO?> Update(
            long id,
            long sellId,
            SellDetailDTO dto
        ) {
            SellDetail? toUpdate = await _repo.GetById(id, sellId);

            if(toUpdate == null) return null;

            toUpdate.ProductId = dto.ProductId;
            toUpdate.Quantity = dto.Quantity;
            toUpdate.UnitaryPrice = dto.Price;
            toUpdate.Total = dto.TotalPrice != 0
                ? dto.TotalPrice
                : dto.Price * dto.Quantity;

            (SellDetail sellDetail, int status) res = await _repo
                .Update(id,sellId,toUpdate);

            if(res.status < 1)
                throw new Exception("Sell detail not updated");

            return _mapper.mapSellDetail(res.sellDetail);
        }
    }
}
