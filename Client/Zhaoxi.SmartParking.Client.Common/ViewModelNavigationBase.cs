using Prism.Commands;
using Prism.Regions;
using System.Linq;
using System.Windows.Input;

namespace Zhaoxi.SmartParking.Client.Common
{
    /// <summary>
    /// ViewMode导航基类
    /// </summary>
    public abstract class ViewModelNavigationBase
    {

        public ViewModelNavigationBase(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            Refresh();
        }

        public string PageTitle { get; set; }

        public bool IsCanClose { get; set; } = true;

        private readonly IRegionManager _regionManager;

        public ICommand RefreshCommand { get => new DelegateCommand(Refresh); }

        public ICommand AddCommand { get => new DelegateCommand(Add); }

        public ICommand CloseCommand
        {
            get => new DelegateCommand<string>(Close);
        }

        private void Close(string uri)
        {
            var region = _regionManager.Regions["MainContentRegion"];

            var view = region.Views.FirstOrDefault(x => x.GetType().Name == this.GetType().Name.Replace("Model", ""));
            if (view == null) return;
            // 将这个对象从Region中移除掉
            region.Remove(view);
        }

        public virtual void Refresh()
        {

        }
        public virtual void Add()
        {

        }
    }
}
