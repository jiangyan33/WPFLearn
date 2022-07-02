using Microsoft.EntityFrameworkCore;
using System;
using Zhaoxi.SmartParking.Server.Models;

namespace Zhaoxi.SmartParking.Server.EFCore
{
    public class EFCoreContext : DbContext
    {
        private readonly string _connectionString;

        public EFCoreContext(string strConn)
        {
            _connectionString = strConn;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<SysUserInfo> SysUserInfo { get; set; }

    }
}
