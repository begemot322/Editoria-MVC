using Editoria.Application.Common.Interfaces.Identity;
using Editoria.Domain.Constants;
using Editoria.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<DbInitializer>();

        await initialiser.InitialiseAsync();

    }
}

public class DbInitializer
{
    private readonly ApplicationDbContext _db;
    private readonly IPasswordHasher _passwordHasher;

    public DbInitializer(
        ApplicationDbContext db,
        IPasswordHasher passwordHasher)
    {
        _db = db;
        _passwordHasher = passwordHasher;
    }
    public async Task InitialiseAsync()
    {
        try
        {
            var pendingMigrations = await _db.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                await _db.Database.MigrateAsync();
            }

            if (!_db.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        UserName = "admin",
                        Email = "admin@example.com",
                        PhoneNumber = "123-456-7890",
                        Role = Roles.Admin,
                        PasswordHash = _passwordHasher.Generate("admin123"),
                        ImageUrl = "/images/users/admin.png"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        UserName = "user",
                        Email = "user@example.com",
                        PhoneNumber = "987-654-3210",
                        Role = Roles.User,
                        PasswordHash = _passwordHasher.Generate("user123"), 
                        ImageUrl = "/images/users/default_user.png"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        UserName = "moderator",
                        Email = "moderator@example.com",
                        PhoneNumber = "880-555-3535",
                        Role = Roles.Moderator,
                        PasswordHash = _passwordHasher.Generate("moderator123"),
                        ImageUrl = "/images/users/moderator.png"
                    }
                };
                await _db.Users.AddRangeAsync(users);
                await _db.SaveChangesAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}