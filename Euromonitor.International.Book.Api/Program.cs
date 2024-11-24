using System.Text;
using Euromonitor.International.Book.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBookServices();
builder.Services.AddSingleton<AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // TokenValidationParameters is a class that specifies the parameters
        // that will be used by JwtBearerMiddleware to validate the token in each request.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // ValidateIssuer set to true means the issuer will be validated during token validation.
            ValidateIssuer = true,
            // ValidateAudience set to true means the audience will be validated during token validation.
            ValidateAudience = true,
            // ValidateLifetime set to true means the token expiry will be checked to ensure it's still valid.
            ValidateLifetime = true,
            // ValidateIssuerSigningKey set to true means the signing key will be validated to ensure the token's integrity.
            ValidateIssuerSigningKey = true,
            // ValidIssuer specifies the issuer to validate. It's taken from the app settings (configuration).
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            // ValidAudience specifies the audience to validate. It's taken from the app settings (configuration).
            ValidAudience = builder.Configuration["Jwt:Audience"],
            // IssuerSigningKey specifies the key used to sign the token. It needs to match the one used to generate the token.
            // It is taken from the app settings and must be a key that both the issuer and the API know.
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAngularApp", builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Angular development server
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
    });

// Add DbContext using InMemoryDatabase 
builder.Services.AddDbContext<Db>(options =>
    options.UseInMemoryDatabase("InMemoryBooksDb"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");
app.MapEndpoints();

app.Run();

