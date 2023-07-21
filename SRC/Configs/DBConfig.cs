using Microsoft.EntityFrameworkCore;
using server.SRC.Models;

namespace server.SRC.Configs
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext() {}

        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options){}

        public DbSet<User> Users {get; set;}
        public DbSet<Account> Accounts {get; set;}
        
    }
}