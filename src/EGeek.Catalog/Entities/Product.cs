using EGeek.Catalog.Exceptions;

namespace EGeek.Catalog.Entities
{
    public sealed class Product : AuditableEntity
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

        public int QuantityInStock { get; private set; }

        private Product() { }

        public Product(
            string name,
            string description,
            decimal price,
            int quantityInStock)
        {

            if (string.IsNullOrEmpty(name))
                throw new EGeekCatalogException("Nome do produto é obrigatório.");

            if (string.IsNullOrEmpty(description))
                throw new EGeekCatalogException("Descrição do produto é obrigatória.");

            if (price <= 0)
                throw new EGeekCatalogException("Preço do produto não pode ser menor ou igual à zero.");

            if (quantityInStock < 0)
                throw new EGeekCatalogException("Quantidade do produto em estoque não pode ser negativo.");

            Name = name;
            Description = description;
            Price = price;
            QuantityInStock = quantityInStock;
        }

    }
}
