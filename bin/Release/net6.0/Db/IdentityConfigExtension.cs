namespace store.Db;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using store.Models;

public static class IdentityConfigExtension
{
    public static void AddIdentityConfig(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<IdentityRole<int>>().ToTable("roles");
        modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("roles_claims");
        modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("user_claims");
        modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("user_logins");
        modelBuilder.Entity<IdentityUserToken<int>>().ToTable("user_tokens");
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("user_roles");
    }
}
