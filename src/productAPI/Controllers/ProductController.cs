using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Models;
using Services;

[ApiController]
[Route("Products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
            _productService= productService;
    }
    

    [HttpGet]
    public IActionResult GetProducts([FromQuery] string? type)
    {
        try
        {
            var result = _productService.GetProducts(type);
            return Ok(result);
        }
        catch (Exception ex)
        {
            var ErrorBody = new ErrorResponseBody
            {
                Timestamp = DateTime.Now,
                Status = 400,
                Error = ex.Message,
                Path = "/Products"
            };
            return new ObjectResult(ErrorBody) { StatusCode = StatusCodes.Status400BadRequest };
        }
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] Dictionary<string, JsonElement> dto)
    {
        try
        {
            var result = _productService.CreateProduct(dto);
            return CreatedAtAction(nameof(GetProducts), new ProductResponse { Id = result.Id });
        }
        catch (Exception ex)
        {
            var errorBody = new ErrorResponseBody
            {
                Timestamp = DateTime.Now,
                Status = 400,
                Error = ex.Message,
                Path = "/Products"
            };
            return new ObjectResult(errorBody) { StatusCode = StatusCodes.Status400BadRequest };
        }
    }
}

