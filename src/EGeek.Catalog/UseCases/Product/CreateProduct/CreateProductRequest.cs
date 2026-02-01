namespace EGeek.Catalog.UseCases.Product.CreateProduct
{
    internal class CreateProductRequest
    {
        public CreateProductRequest(
            string name,
            string description,
            decimal price,
            int quantityInStock)
        {
            Name = name;
            Description = description;
            Price = price;
            QuantityInStock = quantityInStock;

        }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }

    }
}
