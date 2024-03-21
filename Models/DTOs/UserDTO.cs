namespace backend.Models.DTOs {
    public record UserDTO {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DNI { get; set; }
    }
}
