using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using PublicColor.View;
using PublicColor.Model;
using System.Windows.Navigation;
using Setting.Views;
using Setting.ViewModels;
using AdminSetting.Views;
using AdminSetting.ViewModels;

namespace Setting
{
    public class SettingModule : IModule
    {
        IRegionManager _rm;
        public SettingModule(IRegionManager rm)
        {
            _rm = rm;

        }
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

            containerRegistry.RegisterForNavigation<ColorView, ColorModel>();
            containerRegistry.RegisterForNavigation<MarketList, MarketListViewModel>();
            containerRegistry.RegisterForNavigation<LangSetting, LangSettingViewModel>();

        }
    }
}