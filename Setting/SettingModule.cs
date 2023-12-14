using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Setting.ViewModels;
using Setting.Views;
using PublicColor.View;
using PublicColor.Model;
using System.Windows.Navigation;

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
            containerRegistry.RegisterDialog<DialogSettings, DialogSettingsViewModel>();
            containerRegistry.RegisterForNavigation<ColorView,ColorModel>();

            
        }
    }
}