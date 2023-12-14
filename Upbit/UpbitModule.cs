using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Upbit.ViewModels;
using Upbit.Views;

namespace Upbit
{
    public class UpbitModule : IModule
    {
        private readonly IRegionManager _rm;
        private IEventAggregator _ea;

        public UpbitModule(IRegionManager rm, IEventAggregator ea)
        {
            _rm = rm;
            _ea = ea;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //_rm.RegisterViewWithRegion("ContentRegion", typeof(Balance));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Balance, BalanceViewModel>();
            //containerRegistry.RegisterForNavigation<DialogAccess, DialogAccessViewModel>();
            containerRegistry.RegisterForNavigation<CoinList, CoinListViewModel>();
            containerRegistry.RegisterForNavigation<UpbitMain, UpbitMainViewModel>();

            containerRegistry.RegisterForNavigation<Chart, ChartViewModel>();

            containerRegistry.RegisterForNavigation<BidAskList, BidAskListViewModel>();
            containerRegistry.RegisterForNavigation<CoinOrder, CoinOrderViewModel>();

            containerRegistry.RegisterDialog<DialogAccess, DialogAccessViewModel>();
        }
    }
}