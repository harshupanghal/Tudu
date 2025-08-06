using Tudu.Application.DTOs;
using Tudu.Domain.Entities;

namespace Tudu.Application.Mappers;

public static class UserTaskMapper
    {
    public static UserTask ToEntity(this CreateUserTaskRequest dto)
        {
        return new UserTask
            {
            Title = dto.Title,
            Description = dto.Description,
            UserId = dto.UserId,
            CreatedAt = DateTime.UtcNow
            };
        }

    public static UserTaskResponse ToResponse(this UserTask task)
        {
        return new UserTaskResponse
            {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            UserId = task.UserId,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt
            };
        }
    }
