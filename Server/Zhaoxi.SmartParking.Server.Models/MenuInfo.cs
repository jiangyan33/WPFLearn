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
    [Table("menu_info")]
    public class MenuInfo
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("menu_header")]
        public string MenuHeader { get; set; }

        [Column("target_view")]
        public string TargetView { get; set; }

        [Column("parent_id")]
        public int ParentId { get; set; }

        [Column("menu_icon")]
        public string MenuIcon { get; set; }

        [Column("index")]
        public int Index { get; set; }

        [Column("menu_type")]
        public int MenuType { get; set; }

        [Column("state")]
        [DefaultValue(1)]
        public int State { get; set; }

    }
}
