﻿using Microsoft.EntityFrameworkCore;
using Whisper.Data.Entities;
using Whisper.Data.Repositories.Base;

namespace Whisper.Data.Repositories.UserRepository;

internal sealed class UserRepository(WhisperDbContext context) : Repository<UserEntity>(context), IUserRepository
{
    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        return await DbContext.Users
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync();
    }

    public async Task<UserEntity?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await DbContext.Users
            .Where(x => x.PhoneNumber == phoneNumber)
            .FirstOrDefaultAsync();
    }
}