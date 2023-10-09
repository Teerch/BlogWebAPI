using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BlogContext>(options => options.UseMySql(conn, ServerVersion.AutoDetect(conn)));


builder.Services.AddScoped<BlogService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGet("/", () => @"Test API. Navigate to /swagger to open the Swagger test UI.");
app.MapControllers();

app.Run();
