namespace ExampleApp.Models {

    public class Product {

        public Product() { }

        public Product(string name = null,
                        string category = null,
                        decimal price = 0) {
            Name = name;
            Category = category;
            Price = price;
        }
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
