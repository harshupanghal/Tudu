using System.ComponentModel.DataAnnotations;

namespace Tudu.Application.DTOs;
public class RegisterRequest
    {
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
    }

public class LoginRequest
    {
    [Required(ErrorMessage = "Username is required")]
    public string UserName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
    }

public class AuthResponse
    {
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? message { get; set; }
    public bool Success { get; set; }
    }
