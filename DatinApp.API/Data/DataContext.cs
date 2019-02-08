using DatinApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatinApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Value> ValuesSet { get; set; }

        public DbSet<User> UsersSet { get; set; }

        public DbSet<Photo> Photos { get; set; }
    }
}