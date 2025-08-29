namespace Tudu.Application.DTOs;

public class CreateUserTaskRequest
    {
    public string Title { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; } 
    public DateTime? DueDate { get; set; }
    public string? Category { get; set; } = null;
    public Boolean Reminder { get; set; } = false; 
    }

public class UpdateUserTaskRequest
    {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public int UserId { get; set; }
    public Boolean Reminder { get; set; } = false;
    public DateTime? DueDate { get; set; }
    public string? Category { get; set; } = null;
    }

public class UserTaskResponse
    {
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Category { get; set; } = null;
    }
