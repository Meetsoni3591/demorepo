using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using WebApplication6.Migrations;

namespace WebApplication6.Models
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<UserT> Users { get; set; }     //this is name of Database through dbset (users is database table name )
    }
}
