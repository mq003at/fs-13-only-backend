namespace store.Db;

using store.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Npgsql;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    // Static constructor which will be run ONCE
    static AppDbContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<Role>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    private readonly IConfiguration _config;

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config)
        : base(options) => _config = config;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connString = _config.GetConnectionString("DefaultConnection");
        optionsBuilder
            .UseNpgsql(connString)
            .AddInterceptors(new AppDbContextSaveChangesInterceptor())
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Enum declaration
        modelBuilder.HasPostgresEnum<Role>();

        modelBuilder.AddBuilderConfig();
        modelBuilder.AddIdentityConfig();
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Category { get; set; } = null!;
    public DbSet<User> User { get; set; } = null!;
    public DbSet<Cart>? Carts { get; set; }
    public DbSet<CartItem>? CartItems { get; set; }
}
