namespace Tudu.Application.DTOs;

public class CreateUserTaskRequest
    {
    public string Title { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; } // Needed to associate with a user
    }

public class UpdateUserTaskRequest
    {
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public int UserId { get; set; } // Include if updatable
    }

public class UserTaskResponse
    {
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    }
