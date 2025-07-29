using ProductAPI.Utils;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register AutoMapper with all profiles in the assembly
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.AddAppAuthetication();
builder.Services.AddAuthorization();

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
