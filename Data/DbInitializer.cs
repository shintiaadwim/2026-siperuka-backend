using Backend.Models;

namespace Backend.Data;

public static class DbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        SeedAdmin(context);
    }

    private static void SeedAdmin(AppDbContext context)
    {
        if (!context.Users.Any(u => u.Email == "admin@kampus.ac.id"))
        {
            context.Users.Add(new User
            {
                Name = "Admin Kampus",
                Email = "admin@kampus.ac.id",
                Role = "ADMIN",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });

            context.SaveChanges();
        }
    }
}
