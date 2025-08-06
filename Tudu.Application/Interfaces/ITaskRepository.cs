using Tudu.Domain.Entities;
using System.Threading.Tasks;

namespace Tudu.Application.Interfaces;

public interface ITaskRepository
    {
    Task<UserTask?> GetByIdAsync(int taskId);
    Task<List<UserTask>> GetAllByUserIdAsync(int userId);
    Task<List<UserTask>> GetAllAsync(); // optional

    Task AddAsync(UserTask task);
    Task UpdateAsync(UserTask task);
    Task DeleteAsync(int taskId);
    }

