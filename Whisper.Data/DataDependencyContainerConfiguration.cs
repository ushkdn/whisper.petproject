﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Whisper.Data.Repositories.Base;
using Whisper.Data.Repositories.CacheRepository;
using Whisper.Data.Repositories.GroupRepository;
using Whisper.Data.Repositories.LocationRepository;
using Whisper.Data.Repositories.RefreshTokenRepository;
using Whisper.Data.Repositories.UserRepository;
using Whisper.Data.Transactions;
using Whisper.Data.Validations.DtosValidations.LocationDtoValidations;
using Whisper.Data.Validations.DtosValidations.UserDtosValidations;

namespace Whisper.Data;

public static class DataDependencyContainerConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ITransactionManager, TransactionManager>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }

    public static void RegisterDatabase(IServiceCollection services, IConfiguration configuration, string dbConnectionStringKey)
    {
        var dbConnectionString = configuration.GetConnectionString(dbConnectionStringKey)
                               ?? throw new ArgumentNullException($"{dbConnectionStringKey} is not configured.");

        services.AddDbContext<WhisperDbContext>(options =>
            options
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseNpgsql(dbConnectionString,
                    x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
    }

    public static void RegisterCacheStorage(IServiceCollection services, IConfiguration configuration, string cacheStorageConnectionStringKey)
    {
        var cacheStorageconnectionString = configuration.GetConnectionString(cacheStorageConnectionStringKey)
            ?? throw new ArgumentNullException($"{cacheStorageConnectionStringKey} is not configured.");

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(cacheStorageconnectionString));
        services.AddSingleton<ICacheRepository, CacheRepository>();
    }

    public static void SetNpgsqlContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public static void RegisterValidators(IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<UserRegisterDtoValidation>();
        services.AddValidatorsFromAssemblyContaining<AddLocationDtoValidation>();
        services.AddValidatorsFromAssemblyContaining<UserResetPasswordDtoValidation>();
        services.AddValidatorsFromAssemblyContaining<UserLogInDtoValidation>();
    }
}