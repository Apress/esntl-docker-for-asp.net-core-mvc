using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ExampleApp.Models {

    public static class SeedData {

        public static void EnsurePopulated(IApplicationBuilder app) {
            EnsurePopulated(
                app.ApplicationServices.GetRequiredService<ProductDbContext>());
        }

        public static void EnsurePopulated(ProductDbContext context) {
            System.Console.WriteLine("Applying Migrations...");
            context.Database.Migrate();

            if (!context.Products.Any()) {

                System.Console.WriteLine("Creating Seed Data...");
                context.Products.AddRange(
                    new Product("Kayak", "Watersports", 275),
                    new Product("Lifejacket", "Watersports", 48.95m),
                    new Product("Soccer Ball", "Soccer", 19.50m),
                    new Product("Corner Flags", "Soccer", 34.95m),
                    new Product("Stadium", "Soccer", 79500),
                    new Product("Thinking Cap", "Chess", 16),
                    new Product("Unsteady Chair", "Chess", 29.95m),
                    new Product("Human Chess Board", "Chess", 75),
                    new Product("Bling-Bling King", "Chess", 1200)
                );
                context.SaveChanges();
            } else {
                System.Console.WriteLine("Seed Data Not Required...");
            }
        }
    }
}
