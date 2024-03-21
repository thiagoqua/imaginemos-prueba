namespace backend.Models.Responses {
    public record CreateSellResponse {
        public long Id { get; set; }
        public long UserId { get; set; }
        public List<long> SellDetailsIds { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
    }
}
