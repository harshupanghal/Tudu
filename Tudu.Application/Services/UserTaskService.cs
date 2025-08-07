using Tudu.Application.DTOs;
using Tudu.Application.Interfaces;
using Tudu.Application.Mappers;

namespace Tudu.Application.Services;

public class UserTaskService : IUserTaskService
    {
    private readonly ITaskRepository _taskRepository;

    public UserTaskService(ITaskRepository taskRepository)
        {
        _taskRepository = taskRepository;
        }

    public async Task<UserTaskResponse> CreateAsync(CreateUserTaskRequest request)
        {
        var task = request.ToEntity();
        await _taskRepository.AddAsync(task);
        return task.ToResponse();
        }

    public async Task<UserTaskResponse> UpdateAsync(UpdateUserTaskRequest request)
        {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task == null) throw new Exception("Task not found");

        task.Title = request.Title;
        task.Description = request.Description;
        task.IsCompleted = request.IsCompleted;
        task.UpdatedAt = DateTime.UtcNow;

        await _taskRepository.UpdateAsync(task);
        return task.ToResponse();
        }

    public async Task<List<UserTaskResponse>> GetAllByUserIdAsync(int userId)
        {
        var tasks = await _taskRepository.GetAllByUserIdAsync(userId);
        return tasks.Select(t => t.ToResponse()).ToList();
        }

    public async Task DeleteAsync(int id)
        {
        await _taskRepository.DeleteAsync(id);
        }
    }
