using Microsoft.EntityFrameworkCore;

namespace ExampleApp.Models {

    public class ProductDbContext : DbContext {

        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options) {
        }

        public DbSet<Product> Products { get; set; }
    }
}
