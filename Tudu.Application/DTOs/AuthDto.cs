namespace Tudu.Application.DTOs;
public class RegisterRequest
    {
    public string UserName { get; set; }
    public string Password { get; set; }
    }

public class LoginRequest
    {
    public string UserName { get; set; }
    public string Password { get; set; }
    }

public class AuthResponse
    {
    public int Id { get; set; }
    public string UserName { get; set; }
    public string message { get; set; }
    public bool Success { get; set; }
    }
