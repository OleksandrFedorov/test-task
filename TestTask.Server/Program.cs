using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Managers;
using TestTask.Managers.File;
using TestTask.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TestTaskApplcationContext>(x => x.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));
builder.Services.Configure<FileManagerConfiguration>(builder.Configuration.GetSection("AppIdentitySettings"));
builder.Services.AddManagers(builder.Configuration);
builder.Services.AddServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(builder => builder
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader());

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
