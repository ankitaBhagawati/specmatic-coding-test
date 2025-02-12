using System.Text.Json;
using Models;
using Services;
public class ProductService : IProductService
{
    private static readonly List<Product> _products = new();
    private static int _idCounter = 1;

    public List<Product> GetProducts(string? type)
    {
        if (string.IsNullOrEmpty(type))
        {
            var products = _products.ToList();
            return products;
        }
        else
        {
            type = type?.ToLower();

            if (type != "book" && type != "food" && type != "gadget" && type != "other")
            {
                throw new Exception("Invalid Type!");
            }

            var filteredProducts = _products.Where(p => p.Type == type).ToList();
            return filteredProducts;
        }
    }
    public Product CreateProduct(Dictionary<string, JsonElement> dto)
    {
        var inventory = dto.GetValueOrDefault("inventory");
        int inventoryInt = 0;

        var type = dto.GetValueOrDefault("type");
        var name = dto.GetValueOrDefault("name");
        var cost = dto.GetValueOrDefault("cost");

        decimal costDecimal = (decimal)0.0;

        if (inventory.ValueKind == JsonValueKind.Number)
        {
            inventoryInt = inventory.GetInt32();
        }
        else if (inventory.ValueKind == JsonValueKind.String)
        {
            if (!int.TryParse(inventory.GetString(), out inventoryInt))
            {
                throw new Exception("Please enter valid inventory!");
            }
        }

        if (cost.ValueKind == JsonValueKind.Number)
        {
            costDecimal = cost.GetDecimal();
        }
        else if (cost.ValueKind == JsonValueKind.String)
        {
            if (!decimal.TryParse(cost.GetString(), out costDecimal))
            {
                throw new Exception("Please enter valid Cost!");
            }
        }



        if (type.ValueKind != JsonValueKind.String || name.ValueKind != JsonValueKind.String)
        {
            throw new Exception("Please enter valid Name or Type!");
        }


        var productDetails = new ProductDetails
        {
            Inventory = inventoryInt,
            Name = name.GetString(),
            Type = type.GetString(),
            Cost = costDecimal
        };

        if (string.IsNullOrEmpty(productDetails.Name) || productDetails.Inventory < 1 || productDetails.Inventory > 9999)
        {
            throw new Exception("Please enter the type!");
        }
        if (productDetails.Cost <= (decimal)0.0 || productDetails.Cost > (decimal)9999.99)
        {
            throw new Exception("Please enter a valid cost!");
        }
        productDetails.Type = productDetails.Type?.ToLower();

        if (productDetails.Type != "book" && productDetails.Type != "food" && productDetails.Type != "gadget" && productDetails.Type != "other")
        {
            throw new Exception("Please enter valid type!");

        }

        var product = new Product
        {
            Id = _idCounter++,
            Name = productDetails.Name,
            Type = productDetails.Type,
            Inventory = productDetails.Inventory,
            Cost = productDetails.Cost
        };

        _products.Add(product);
        return product;
    }
}