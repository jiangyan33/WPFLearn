using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Windows.Input;

namespace Zhaoxi.SmartParking.Client.MainModule.Models
{
    public class MenuItemModel : BindableBase
    {
        public string MenuIcon { get; set; }

        public string MenuHeader { get; set; }

        public string TargetView { get; set; }

        private bool _isExpanded;

        public bool IsExpanded
        {
            get { return _isExpanded; }

            set { SetProperty(ref _isExpanded, value); }
        }

        public List<MenuItemModel> Children { get; set; } = new List<MenuItemModel>();

        public ICommand OpenViewCommand
        {
            get => new DelegateCommand(() =>
            {
                if (Children != null && Children.Count == 0 && !string.IsNullOrEmpty(TargetView))
                {
                    _regionManager.RegisterViewWithRegion("MainContentRegion", TargetView);
                }
                else IsExpanded = !IsExpanded;
            });
        }


        private readonly IRegionManager _regionManager;

        public MenuItemModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
    }

}
