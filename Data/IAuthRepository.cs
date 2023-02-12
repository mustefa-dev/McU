using McU.Models;

namespace McU.Data;

public interface IAuthRepository{
    Task<(string? data, bool success, string? message)> Register(User user, string password);
    Task<(string? data, bool success, string? message)> Login(string username, string password);
}