using Microsoft.EntityFrameworkCore;

namespace backend.Models {
    public class ImaginemosDBContext : DbContext {
        protected readonly IConfiguration Configuration;

        public ImaginemosDBContext(IConfiguration configuration) {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseNpgsql(Configuration.GetConnectionString("database"));
        }

        DbSet<Product> Products { get; set; }
        DbSet<Sell> Sells { get; set; }
        DbSet<SellDetail> SellDetails {  get; set; }
        DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SellDetail>()
                .HasOne(sd => sd.Product)
                .WithMany()
                .HasForeignKey(sd => sd.ProductId);

            modelBuilder.Entity<SellDetail>()
                .HasOne(sd => sd.Sell)
                .WithMany()
                .HasForeignKey(sd => sd.SellId);

            modelBuilder.Entity<Sell>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(sd => sd.UserId);
        }
    }
}
