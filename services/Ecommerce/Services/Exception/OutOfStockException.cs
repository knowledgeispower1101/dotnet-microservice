namespace Ecommerce.Services.Exception;

public class OutOfStockException(string message) : IOException(message)
{
}