using Prism.Regions;
using Zhaoxi.SmartParking.Client.Common;

namespace Zhaoxi.SmartParking.Client.BaseModule.ViewModels
{
    public class UserManagementViewModel : ViewModelNavigationBase
    {
        public UserManagementViewModel(IRegionManager regionManager) : base(regionManager)
        {
            PageTitle = "用户信息管理";
        }
    }
}
