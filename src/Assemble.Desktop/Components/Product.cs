using Assemble.Desktop.Enums;

namespace Assemble.Desktop.Components
{
    public class Product
    {
        public ProductType Type { get; }

        public Product(ProductType type)
        {
            Type = type;
        }
    }
}