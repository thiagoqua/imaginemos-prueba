namespace backend.Models.DTOs {
    public record SellDTO {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
    }
}
