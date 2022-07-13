using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhaoxi.SmartParking.Server.Models
{
    [Table("sys_user_info")]
    public class SysUserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("user_icon")]
        public string UserIcon { get; set; }

        [Column("age")]
        public int Age { get; set; }

        [Column("real_name")]
        public string RealName { get; set; }

        [Column("state")]
        public int State { get; set; }

        [NotMapped]
        public List<MenuInfo> Menus { get; set; }

        [NotMapped]
        public List<RoleInfo> Roles { get; set; }
    }
}
