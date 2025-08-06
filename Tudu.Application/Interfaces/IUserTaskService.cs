using Tudu.Application.DTOs;

namespace Tudu.Application.Interfaces;

public interface IUserTaskService
    {
    Task<UserTaskResponse> CreateAsync(CreateUserTaskRequest request);
    Task<UserTaskResponse> UpdateAsync(UpdateUserTaskRequest request);
    Task<List<UserTaskResponse>> GetAllByUserIdAsync(int userId);
    Task DeleteAsync(int id);
    }
