using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.SmartParking.Server.Models
{
    [Table("role_info")]
    public class RoleInfo
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("role_name")]
        public string RoleName { get; set; }

        [Column("state")]
        [DefaultValue(1)]
        public int State { get; set; }
    }
}
