using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<PhoneModel> Phones { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=online_store.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin";
            string adminPassword = "1234";

            RoleModel adminRole = new RoleModel { Id = 1, Name = adminRoleName };
            RoleModel userRole = new RoleModel { Id = 2, Name = userRoleName };
            UserModel adminUser = new UserModel { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<RoleModel>().HasData(new RoleModel[] { adminRole, userRole });
            modelBuilder.Entity<UserModel>().HasData(new UserModel[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
