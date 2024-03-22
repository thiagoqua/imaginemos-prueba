using backend.Models;
using backend.Models.DTOs;
using backend.Models.Requests;
using backend.Models.Responses;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services {
    public class SellService : ISellService {
        private readonly ISellRepository _repo;
        private readonly DTOMapperService _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IProductService _productService;

        public SellService(
            ISellRepository repo, 
            DTOMapperService mapper,
            IUserRepository userRepository,
            IProductService productService
        ) {
            _repo = repo;
            _mapper = mapper;
            _userRepository = userRepository;
            _productService = productService;
        }

        public async Task<CreateSellResponse> Create(CreateSellRequest req) {
            List<SellDetail> sellDetails = new();
            double totalPrice = await CalcTotal(req.Products);
            User? user = await _userRepository.GetByDNI(req.DNI.ToString());
            
            //completo los datos que se saben de la venta
            Sell sell = new Sell() {
                date = DateTime.Now,
                Total = totalPrice,
                User = user 
                        ?? new User() {
                            DNI = req.DNI.ToString(),
                            Name = req.UserName
                        }
            };

            //itero por cada producto comprado
            foreach(long productId in req.Products.Keys) {
                //obtengo los datos del producto en cuestión
                ProductDTO product = await _productService.GetById(productId)!;
                int quantity = req.Products[productId];
                double productPrice = product!.Price;

                //completo los datos correspondientes de cada detalle de venta
                SellDetail sellDetail = new SellDetail() {
                    ProductId = productId,
                    Quantity = quantity,
                    Sell = sell,
                    Total = productPrice * quantity,
                    UnitaryPrice = product!.Price
                };

                //los agrego a la lista de compras realizadas
                sellDetails.Add(sellDetail);
            }

            //inserto el total de compras realizadas
            var ret = await _repo.Create(sellDetails);

            //devuelvo una objeto con los datos de la compra. 
            //posee un atributo de tipo array que detalla todos los ids de los objetos
            //SellDetails creados
            return await Task.Run(() => 
                new CreateSellResponse {
                    Date = ret[0].Sell.date,
                    Total = totalPrice,
                    UserId = ret[0].Sell.UserId,
                    UserName = ret[0].Sell.User.Name,
                    SellDetailsIds = ret.Select(sell => sell.Id).ToList()
                }
            ); 
        }

        public async Task Delete(long id) {
            Sell toDelete = await _repo.GetById(id)
                                ?? throw new ArgumentException();
            int status = await _repo.Delete(toDelete);

            if(status != 1)
                throw new Exception("Sell not deleted");
        }

        public async Task<List<SellDTO>> Get(string? search, string? minDate, string? maxDate) {
            List<Sell> sells = await _repo.Get(search);
            IEnumerable<Sell> ret = sells.AsEnumerable();

            if(minDate != null) {
                DateTime date = DateTime.Parse(minDate);
                ret = ret
                    .Where(sell => sell.date.CompareTo(date) > 0);
            }

            if(maxDate != null) {
                DateTime date = DateTime.Parse(maxDate);
                ret = ret
                    .Where(sell => sell.date.CompareTo(date) < 0);
            }

            return await Task.Run(() => 
                ret.Select(sell => _mapper.mapSell(sell))
                    .ToList()
            );
        }

        public async Task<SellDTO?> GetById(long id) {
            Sell? ret = await _repo.GetById(id);

            return ret == null
                ? null
                : _mapper.mapSell(ret);
        }

        public async Task Update(long id, SellDTO dto) {
            Sell toUpdate = await _repo.GetById(id)
                                ?? throw new ArgumentException();
            int status;

            toUpdate.date = dto.Date;
            toUpdate.User.Name = dto.UserName;
            toUpdate.Total = dto.Total;

            status = await _repo.Update(toUpdate);

            if(status < 1)
                throw new Exception("Sell not updated");
        }

        public async Task UpdateTotalPrice(long id, double price) {
            int status;
            Sell toUpdate = await _repo.GetById(id)
                ?? throw new ArgumentException();

            //puede ser positivo o negativo el precio
            toUpdate.Total += price;

            status = await _repo.Update(toUpdate);

            if(status < 1)
                throw new Exception("Sell price not updated");
        }

        private async Task<double> CalcTotal(Dictionary<long,int> products) {
            double ret = 0;

            foreach(long productId in products.Keys) {
                ProductDTO product = await _productService.GetById(productId);
                int quantity = products[productId];
                double price = product!.Price;

                ret += price * quantity;
            }

            return ret;
        }
    }
}
