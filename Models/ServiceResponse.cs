namespace McuApi.NET_7.Models;

//Convenience: Services allow customers
//to access what they need without having to purchase or store a physical product.
public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public bool Success { get; set; } = true;
    public string? Message { get; set; } = string.Empty;
}