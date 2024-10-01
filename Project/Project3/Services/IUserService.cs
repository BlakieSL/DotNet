using Project3.DTOs;

namespace Project3.Services;

public interface IUserService
{
    Task RegisterStudent(RegisterRequest model);
    Task<string[]> Login(LoginRequest loginRequest);
}