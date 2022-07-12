using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Globalization;
using Zhaoxi.SmartParking.Server.Models;

namespace Zhaoxi.SmartParking.Server.EFCore
{
    public class EFCoreContext : DbContext
    {
        private readonly string _connectionString = "Data Source=120.79.185.158;Database=zx_sp_record;User ID=root;Password=helloworld;";

        public EFCoreContext()
        {
        }

        public EFCoreContext(string strConn)
        {
            _connectionString = strConn;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var iconValueConverter = new ValueConverter<string, string>(
              v => string.IsNullOrEmpty(v) ? null : ((int)v.ToCharArray()[0]).ToString("x"),
              v => v == null ? "" : ((char)int.Parse(v, NumberStyles.HexNumber)).ToString());
            modelBuilder.Entity<MenuInfo>().Property(p => p.MenuIcon).HasConversion(iconValueConverter);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<SysUserInfo> SysUserInfo { get; set; }

        public DbSet<MenuInfo> MenuInfo { get; set; }

        public DbSet<RoleInfo> RoleInfo { get; set; }

        public DbSet<RoleMenu> RoleMenu { get; set; }

        public DbSet<UserRole> UserRole { get; set; }
    }
}
