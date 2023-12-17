using AdminSetting.ViewModels;
using AdminSetting.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PublicColor.Model;
using PublicColor.View;

namespace AdminSetting
{
    public class AdminSettingModule : IModule
    {
        IRegionManager _rm;
        public AdminSettingModule(IRegionManager rm)
        {
            _rm = rm;

        }

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MarketList, MarketListViewModel>();
        }
    }
}