﻿using Whisper.Data.Dtos.User;
using Whisper.Data.Utils;

namespace Whisper.Services.AuthService;

public interface IAuthService
{
    Task Register(UserRegisterDto request);

    Task LogIn(UserLogInDto request);

    Task ForgotPassword(UserForgotPasswordDto request);

    Task ResetPassword(UserResetPasswordDto request);

    Task Verify(UserVerifyDto request);
}