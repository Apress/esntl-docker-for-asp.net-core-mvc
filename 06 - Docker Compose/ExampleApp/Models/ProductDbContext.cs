using Microsoft.EntityFrameworkCore;
using System;

namespace ExampleApp.Models {

    public class ProductDbContext : DbContext {

        public ProductDbContext() { }

        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options) {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {

            var envs = Environment.GetEnvironmentVariables();

            var host = envs["DBHOST"] ?? "localhost";
            var port = envs["DBPORT"] ?? "3306";
            var password = envs["DBPASSWORD"] ?? "mysecret";

            options.UseMySql($"server={host};userid=root;pwd={password};"
                    + $"port={port};database=products");
        }

        public DbSet<Product> Products { get; set; }
    }
}
