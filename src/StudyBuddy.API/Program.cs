using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application;
using StudyBuddy.Application.Features.Commands.Auth;
using StudyBuddy.Application.Helpers;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Interfaces.Repositories;
using StudyBuddy.Application.Mapping;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Persistence;
using StudyBuddy.Persistence.Context;
using StudyBuddy.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDB"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly("StudyBuddy.Persistence");
    });
});
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();
//builder.Services.AddHttpClient();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;

}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddApplicationServices();
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));
var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
builder.Services.AddCustomTokenAuth(tokenOptions);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();