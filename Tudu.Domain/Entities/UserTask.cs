using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tudu.Domain.Entities;

public class UserTask
    {
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    // Foreign key
    [ForeignKey("User")]
    public int UserId { get; set; }

    // Navigation to User
    public User User { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public Boolean Reminder { get; set; }
    public DateTime? DueDate { get; set; }
    public string? Category { get; set; }
    }

