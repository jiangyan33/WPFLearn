using Microsoft.EntityFrameworkCore;
using System;
using Zhaoxi.SmartParking.Server.Models;

namespace Zhaoxi.SmartParking.Server.EFCore
{
    public class EFCoreContext : DbContext
    {
        private string _connectionString = "Server=localhost;Database=zx_sp_record;Trusted_Connection=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<SysUserInfo> SysUserInfo { get; set; }

    }
}
