﻿using Microsoft.EntityFrameworkCore;
using Tudu.Application.Interfaces;
using Tudu.Domain.Entities;
using Tudu.Infrastructure.Context;

namespace Tudu.Infrastructure.Repositories;

public class UserRepository : IUserRepository
    {
    private readonly TuduDbContext _context;

    public UserRepository(TuduDbContext context)
        {
        _context = context;
        }

    public async Task AddAsync(User user)
        {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        }

    public async Task DeleteAsync(int id)
        {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
            {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            }
        }

    public async Task<List<User>> GetAllAsync()
        {
        return await _context.Users.ToListAsync();
        }

    public async Task<User?> GetByIdAsync(int id)
        {
        return await _context.Users.FindAsync(id);
        }

    public async Task<User?> GetByUsernameAsync(string username)
        {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == username);
        }

    public async Task<User?> GetByEmailAsync(string email)
        {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
        }

    public async Task UpdateAsync(User user)
        {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        }
    }

