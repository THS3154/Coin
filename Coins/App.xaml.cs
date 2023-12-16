using CoinLogin;
using Coins.ViewModels;
using Coins.Views;
using ImTools;
using Prism.Ioc;
using Prism.Modularity;
using Setting;
using System.Windows;
using Upbit;
using Permission;
using Coins.Views;
using PublicColor.Model;
using PublicColor.View;

namespace Coins
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //메뉴 등록
            containerRegistry.RegisterForNavigation<Menus, MenusViewModel>();
            containerRegistry.RegisterForNavigation<SideMenu, SideMenuViewModel>();
            containerRegistry.RegisterForNavigation<Main, MainViewModel>();

            containerRegistry.RegisterForNavigation<DashboardViewer, DashboardViewerViewModel>();
            containerRegistry.RegisterForNavigation<SettingViewer, SettingViewerViewModel>();
            containerRegistry.RegisterForNavigation<UpbitViewer, UpbitViewerViewModel>();

            

            containerRegistry.RegisterSingleton<PM>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<UpbitModule>();
            moduleCatalog.AddModule<SettingModule>();
            moduleCatalog.AddModule<CoinLoginModule>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            //MessageBox.Show("종료");
            base.OnExit(e);
        }
    }
}
