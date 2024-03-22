namespace backend.Models.DTOs {
    public record SellDetailDTO {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
    }
}
