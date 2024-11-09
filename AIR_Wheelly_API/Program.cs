using System.Text;
using AIR_Wheelly_BLL.Services;
using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_DAL;
using AIR_Wheelly_DAL.Data;
using AIR_Wheelly_DAL.Models;
using AIR_Wheelly_DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>();
//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//Services
builder.Services.AddScoped<AuthService>();
//JWT
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
            ValidIssuer = "wheelly",
            ValidAudience = "user",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("f0085aed6406bfd10a953bdbd2e8b2c706262beae0dd7ba939e7d557e43f70ad88fd664130779f209491440a95ee75420b44ae566fb320a25bda283eb9268b2eeac32d34095d3417b108a8f7539c5e458648daa24e38061060972c8456127b27deaef1db5b8bdbb6ef4d59dd076daccf66c1a42edd0ed0037ddc3442efec9df822b72ae8f72cb225a81365d2795c7e4a8cb0dd5d306e6fb93b573b8a9264d5a191c9135c333720104447b0481be28a30443fbb51f6a9d6f2f20178b505ea3ce6df5b916730ab3673a9929d56c67ff6c76c358032d7adfcace8f5adab0117643a5d4cbcc8d6574d4f15a51c1d56223bd4ac41ecd66a51aff52cfe63741ebcd88f"))
        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
