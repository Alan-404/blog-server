using Microsoft.EntityFrameworkCore;
using server.SRC.Models;

namespace server.SRC.DB
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext() {}

        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options){}

        public DbSet<User> Users {get; set;}
        public DbSet<Account> Accounts {get; set;}
        public DbSet<Blog> Blogs {get; set;}
        public DbSet<Comment> Comments {get; set;}
        public DbSet<Category> Categories {get; set;}
        public DbSet<BlogView> BlogViews {get; set;}
        public DbSet<BlogCategory> BlogCategories {get; set;}
        public DbSet<CommentLike> CommentLikes {get; set;}
        public DbSet<SocialNetwork> SocialNetworks {get; set;}
        public DbSet<UserSocialNetwork> UserSocialNetworks {get; set;}
        public DbSet<Room> Rooms {get ;set;}
        
    }
}