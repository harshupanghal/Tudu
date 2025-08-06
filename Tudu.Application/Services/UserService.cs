using Tudu.Application.DTOs;
using Tudu.Application.Interfaces;
using Tudu.Application.Mappers;
using Tudu.Domain.Entities;

namespace Tudu.Application.Services;

public class UserService : IUserService
    {
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
        {
        _userRepository = userRepository;
        }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
        var existingUser = await _userRepository.GetByUsernameAsync(request.UserName);
        if (existingUser != null)
            return new AuthResponse { Success = false, message = "Username already exists." };

        var user = new User
            {
            UserName = request.UserName,
            Password = request.Password,
            CreatedAt = DateTime.UtcNow
            };

        await _userRepository.AddAsync(user);
        return user.ToAuthResponse("User registered successfully.");
        }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
        var user = await _userRepository.GetByUsernameAsync(request.UserName);
        if (user == null || user.Password != request.Password)
            return new AuthResponse { Success = false, message = "Invalid credentials." };

        return user.ToAuthResponse("Login successful.");
        }

    public async Task<AuthResponse?> GetUserByIdAsync(int id)
        {
        var user = await _userRepository.GetByIdAsync(id);
        return user?.ToAuthResponse();
        }
    }
