using System.Text.Json;
using Models;
namespace Services;
public interface IProductService
{
    public List<Product> GetProducts(string? type);
    public Product CreateProduct(Dictionary<string, JsonElement> dto);
}