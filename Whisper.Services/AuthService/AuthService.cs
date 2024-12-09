﻿using System.Text.RegularExpressions;
using Whisper.Data;
using Whisper.Data.CacheModels;
using Whisper.Data.Dtos.User;
using Whisper.Data.Entities;
using Whisper.Data.Mapping;
using Whisper.Data.Models;
using Whisper.Data.Repositories.CacheRepository;
using Whisper.Data.Repositories.UserRepository;
using Whisper.Data.Transactions;
using Whisper.Data.Utils;
using Whisper.Services.MessageService;

namespace Whisper.Services.AuthService;

public class AuthService(IUserRepository _userRepository,
    ITransactionManager _transactionManager,
    ICacheRepository _cacheRepository,
    IMessageService _messageService) : IAuthService
{
    private const string EMAIL_REGEX = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    public async Task ForgotPassword(UserForgotPasswordDto request)
    {
        if (Regex.IsMatch(request.EmailOrPhoneNumber, EMAIL_REGEX))
        {
            //
        }
        else
        {
            //
        }
    }

    public async Task LogIn(UserLogInDto request)
    {
        UserEntity storedUser = null;
        if (Regex.IsMatch(request.EmailOrPhoneNumber, EMAIL_REGEX))
        {
            storedUser = await _userRepository.GetByEmailAsync(request.EmailOrPhoneNumber);
        }
        else
        {
            storedUser = await _userRepository.GetByPhoneNumberAsync(request.EmailOrPhoneNumber);
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, storedUser.Password))
        {
            throw new ArgumentException("Wrong email/phonenumber or password.");
        }

        if (!storedUser.IsVerified)
        {
            throw new InvalidOperationException("Your account is not verified.");
        }

        //token zone should be here
    }

    public async Task Register(UserRegisterDto request)
    {
        //remove all not verified accs via quartz every 1h??
        var storedUser = await _userRepository.GetByEmailAsync(request.Email);
        if (storedUser is not null)
        {
            throw new InvalidOperationException("User already exists.");
        }

        request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

        await _userRepository.CreateAsync(WhisperMapper.Mapper.Map<UserEntity>(request));
        await _transactionManager.SaveChangesAsync();

        await _messageService.SendMessage(new MessagePayload { UserEmail = request.Email, });
        //mail send here
    }

    public async Task ResetPassword(UserResetPasswordDto user)
    {
    }

    public async Task Verify(UserVerifyDto request)
    {
        var cachedSecretCode = await _cacheRepository.GetSingleAsync<CacheSecretCode>(
            CacheTables.SECRET_CODE + $":{request.Email}"
        );

        if (cachedSecretCode is null)
        {
            throw new ArgumentNullException("Your secret code expires. Please try again.");
        }

        if (request.SecretCode != cachedSecretCode.SecretCode)
        {
            throw new ArgumentException("Wrong email or secret code.");
        }

        var storedUser = WhisperMapper.Mapper.Map<UserModel>(await userRepository.GetByEmailAsync(request.Email));

        if (storedUser.IsVerified)
        {
            throw new InvalidOperationException("Your account already verified.");
        }

        storedUser.IsVerified = true;
        _userRepository.Update(WhisperMapper.Mapper.Map<UserEntity>(storedUser));
        await _transactionManager.SaveChangesAsync();
    }
}