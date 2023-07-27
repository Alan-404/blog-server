using Microsoft.EntityFrameworkCore;
using server.SRC.DB;
using server.SRC.Services;
using server.SRC.Services.Providers;
using server.SRC.Middlewares;
using server.SRC.Hubs;
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));

builder.Services.AddScoped<IUserService, UserProvider>();
builder.Services.AddScoped<IAccountService, AccountProvider>();
builder.Services.AddScoped<IBlogService, BlogProvider>();
builder.Services.AddScoped<ICommentService, CommentProvider>();
builder.Services.AddScoped<IBlogViewSerivce, BlogViewProvider>();
builder.Services.AddScoped<ICommentLikeService, CommentLikeProvider>();
builder.Services.AddScoped<ICategoryService, CategoryProvider>();
builder.Services.AddScoped<IBlogCategoryService, BlogCategoryProvider>();
builder.Services.AddScoped<ISocialNetworkService, SocialNetworkProvider>();
builder.Services.AddScoped<IUserSocialNetworkService, UserSocialNetworkProvider>();
builder.Services.AddScoped<IRoomService, RoomProvider>();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseMiddleware<AuthMiddleware>();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.UseWebSockets();
app.MapHub<ChatHub>("/ws");

app.Run();
