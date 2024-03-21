namespace backend.Models.Requests {
    public record CreateSellRequest {
        public string UserName { get; set; } 
        public long DNI { get; set; }
        public Dictionary<long, int> Products { get; set; } 
    }
}
