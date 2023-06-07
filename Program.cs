using store.Models;
using store.DTOs;
using store.Db;
using store.Services;

using System.Text;
using System.Net;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add databases context
builder.Services.AddDbContext<AppDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters += null;
    options.Password.RequireNonAlphanumeric = false;
});
builder.Services
    .AddIdentity<User, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services
    .AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])
            ),
            RoleClaimType = "role"
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "UserMatching",
        policy =>
            policy.RequireAssertion(context =>
            {
                var roleClaim = context.User.FindFirstValue(ClaimTypes.Role);
                string idClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var httpContext = context.Resource as HttpContext;
                var idParams = httpContext?.Request.RouteValues["id"]?.ToString();

                // Check if user is an admin or owns the cart
                if (roleClaim == "Admin" || String.Equals(idParams, idClaim))
                {
                    return true;
                }
                else
                    return false;
            })
    );
    options.AddPolicy(
        "Admin",
        policy =>
        {
            policy.RequireClaim(ClaimTypes.Role, "Admin");
        }
    );
});

// Add scoped, transient or singleton services
builder.Services.AddScoped<IProductService, DbProductService>();
builder.Services.AddScoped<ICategoryService, DbCategoryService>();
builder.Services.AddScoped<IUserService, DbUserService>();
builder.Services.AddScoped<ICartService, DbCartService>();
builder.Services.AddScoped<ICartItemService, DbCartItemService>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();

//     // NOTE: Don't do this in production
//     using (var scope = app.Services.CreateScope())
//     {
//         var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
//         var config = scope.ServiceProvider.GetService<IConfiguration>();

//         if (dbContext is not null && config.GetValue<bool>("CreateDbAtStart", false))
//         {
//             dbContext.Database.EnsureDeleted();
//             dbContext.Database.EnsureCreated();
//         }
//     }
// }

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API");
    c.RoutePrefix = string.Empty; // Set the route prefix to an empty string
});

// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
//     var config = scope.ServiceProvider.GetService<IConfiguration>();

//     if (dbContext is not null && config.GetValue<bool>("CreateDbAtStart", false))
//     {
//         dbContext.Database.EnsureCreated();
//         dbContext.Database.Migrate();
//     }
// }

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();    
    context.Database.Migrate();
}

app.UseCors();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
