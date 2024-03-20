namespace backend.Models {
    public class SellDetail {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long SellId { get; set; }
        public int Quantity { get; set; }
        public double UnitaryPrice { get; set; }
        public double Total {  get; set; }

        public Product Product { get; set; }
        public Sell Sell { get; set; }
    }
}