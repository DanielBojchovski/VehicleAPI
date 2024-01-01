using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VehicleAPI.AppStartUp;
using VehicleAPI.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<VehicleDBContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!);
    });

builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);  //enables load related data

builder.Services.AddDependencyInjectionServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "DanielDevCors",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowCredentials().AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DanielDevCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
