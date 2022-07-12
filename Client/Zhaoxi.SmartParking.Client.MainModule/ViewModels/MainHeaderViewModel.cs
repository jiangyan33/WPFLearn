using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.SmartParking.Client.Entity;

namespace Zhaoxi.SmartParking.Client.MainModule.ViewModels
{
    public class MainHeaderViewModel
    {
        public string CurrentUserName { get; set; }

        public MainHeaderViewModel()
        {
            CurrentUserName = GlobalEntity.CurrentUserInfo.UserName;
        }
    }
}
