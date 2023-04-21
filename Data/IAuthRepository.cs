using McU.Dtos.User;
using McU.Models;

namespace McU.Data;

public interface IAuthRepository{
    Task<(UserRegisterDto user, string? error)> Register(UserRegisterDto user);

    Task<(string? data, bool success, string? message)> Login(string username, string password);

}