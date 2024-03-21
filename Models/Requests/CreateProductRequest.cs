namespace backend.Models.Requests {
    public record CreateProductRequest {
        public string Name { get; set; }
        public double Price {  get; set; }
        public string Description { get; set; }
    }
}
