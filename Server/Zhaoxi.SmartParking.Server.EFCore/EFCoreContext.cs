using Microsoft.EntityFrameworkCore;
using Zhaoxi.SmartParking.Server.Models;

namespace Zhaoxi.SmartParking.Server.EFCore
{
    public class EFCoreContext : DbContext
    {
        private readonly string _connectionString = "Data Source=120.79.185.158;Database=zx_sp_record;User ID=root;Password=helloworld;";

        public EFCoreContext()
        {
        }

        public EFCoreContext(string strConn="")
        {
            //_connectionString = strConn;
        }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }

        public DbSet<SysUserInfo> SysUserInfo { get; set; }

        public DbSet<MenuInfo> MenuInfo { get; set; }

        public DbSet<RoleInfo> RoleInfo { get; set; }

        public DbSet<RoleMenu> RoleMenu { get; set; }

        public DbSet<UserRole> UserRole { get; set; }
    }
}
