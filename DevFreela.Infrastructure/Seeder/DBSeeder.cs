using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Exceptions;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Infrastructure.Seeder;

public static class DBSeeder
{
    
    public static async void Seed(IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.CreateScope();
        var unitOfWorrk = context.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var authService = context.ServiceProvider.GetService<IAuthService>();
        ArgumentNullException.ThrowIfNull(unitOfWorrk);
        ArgumentNullException.ThrowIfNull(authService);

        var adminRole = await GetRole(unitOfWorrk, RoleNameEnum.ADMIN);
        var clientRole = await GetRole(unitOfWorrk, RoleNameEnum.CLIENT);
        var freelancerRole = await GetRole(unitOfWorrk, RoleNameEnum.FREELANCER);
        var adminUser = new User
        (
            "Admin",
            "admin@admin.com",
            DateTime.Now.AddYears(-20),
            authService.ComputeSha256Hash("admin"),
            new List<UserRole> { new(adminRole.Id) },
            new List<UserSkill>()
        );
        try
        {
            await unitOfWorrk.UserQueryRepository.GetByEmailAndPasswordAsync(adminUser.Email, adminUser.Password);
        }
        catch(NotFoundException)
        {
            await unitOfWorrk.UserRepository.AddAsync(adminUser);
            await unitOfWorrk.CompleteAsync();
        }
    }

    private static async Task<Role> GetRole(IUnitOfWork unitOfWork, RoleNameEnum roleEnum)
    {
        try
        {
            return await unitOfWork.RoleQueryRepository.GetByNameAsync(roleEnum);
        }
        catch (NotFoundException)
        {
            var role = await unitOfWork.RoleRepository.AddAsync(new(roleEnum));
            await unitOfWork.CompleteAsync();
            return role;
        }
    }

}