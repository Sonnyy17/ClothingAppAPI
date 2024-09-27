using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Security;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connect database.
builder.Services.AddDbContext<AppDbContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Đăng kí CloudinaryService
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
// Thêm các dịch vụ ứng dụng vào DI container
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddScoped<IClothesCategoryService, ClothesCategoryService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<IClothesService, ClothesService>();
builder.Services.AddScoped<IProfileService, ProfileService>();


// Thêm các repository vào DI container
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
builder.Services.AddScoped<IClothesCategoryRepository, ClothesCategoryRepository>();
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<IClothesRepository, ClothesRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();


// Cấu hình cho CloudinarySettings
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
// Add controllers
builder.Services.AddControllers();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Cấu hình JWT Token Generator
builder.Services.AddSingleton<IJwtTokenGenerator>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var secretKey = configuration["JwtSettings:SecretKey"];
    var issuer = configuration["JwtSettings:Issuer"];
    var audience = configuration["JwtSettings:Audience"];
    var expirationMinutesString = configuration["JwtSettings:ExpirationMinutes"];
    if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(expirationMinutesString))
    {
        throw new ArgumentNullException("Một trong các giá trị JwtSettings không được để trống.");
    }

    if (!int.TryParse(expirationMinutesString, out int expirationMinutes))
    {
        throw new ArgumentException("ExpirationMinutes phải là một số hợp lệ.");
    }

    return new JwtTokenGenerator(secretKey, expirationMinutes);
});

// Cấu hình JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // Nên lưu Issuer trong appsettings.json
        ValidAudience = builder.Configuration["Jwt:Audience"], // Nên lưu Audience trong appsettings.json
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization(); // Thêm dịch vụ Authorization




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Sử dụng Authentication Middleware
app.UseAuthorization();  // Sử dụng Authorization Middleware

app.MapControllers();

app.Run();
