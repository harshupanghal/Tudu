using Microsoft.EntityFrameworkCore;
using Tudu.Application.Interfaces;
using Tudu.Domain.Entities;
using Tudu.Infrastructure.Context;

namespace Tudu.Infrastructure.Repositories
    {
    public class TaskRepository : ITaskRepository
        {
        private readonly TuduDbContext _context;

        public TaskRepository(TuduDbContext context)
            {
            _context = context;
            }

        public async Task AddAsync(UserTask task)
            {
            await _context.UserTasks.AddAsync(task);
            await _context.SaveChangesAsync();
            }

        public async Task DeleteAsync(int taskId)
            {
            var task = await _context.UserTasks.FindAsync(taskId);
            if (task != null)
                {
                _context.UserTasks.Remove(task);
                await _context.SaveChangesAsync();
                }
            }

        public async Task<List<UserTask>> GetAllAsync()
            {
            return await _context.UserTasks.ToListAsync();
            }

        public async Task<List<UserTask>> GetAllByUserIdAsync(int userId)
            {
            return await _context.UserTasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
            }

        public async Task<UserTask?> GetByIdAsync(int taskId)
            {
            return await _context.UserTasks.FindAsync(taskId);
            }

        public async Task UpdateAsync(UserTask task)
            {
            _context.UserTasks.Update(task);
            await _context.SaveChangesAsync();
            }
        }
    }
