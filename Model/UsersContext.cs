using Microsoft.EntityFrameworkCore;

namespace CRUD_BackEnd.Model
{
    public class UsersContext:DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
    }
}
