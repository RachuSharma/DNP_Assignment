﻿using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int id);
    Task<User> GetSingleAsync(int id);
    IQueryable<User> GetManyUser();

    Task<User> GetSingleUserByUserNameAsync(string userName);
}