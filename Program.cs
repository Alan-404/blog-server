using Microsoft.EntityFrameworkCore;
using server.SRC.Configs;
using server.SRC.Services;
using server.SRC.Services.Providers;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));

builder.Services.AddScoped<IUserService, UserProvider>();
builder.Services.AddScoped<IAccountService, AccountProvider>();
builder.Services.AddScoped<IBlogService, BlogProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
