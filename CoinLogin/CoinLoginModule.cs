using CoinLogin.ViewModels;
using CoinLogin.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CoinLogin
{
    public class CoinLoginModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Login, LoginViewModel>();
            containerRegistry.RegisterForNavigation<LoginMain, LoginMainViewModel>();
            containerRegistry.RegisterForNavigation<SingUp, SingUpViewModel>();
        }
    }
}