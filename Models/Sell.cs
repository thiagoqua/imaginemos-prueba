namespace backend.Models {
    public class Sell {
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime date { get; set; }
        public double Total { get; set; }

        public User User { get; set; }
    }
}