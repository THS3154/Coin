using Coins.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Media;
using Upbit.UpbitFunctions;
using Upbit.Views;

namespace Coins.ViewModels
{
    public class UpbitViewerViewModel : BindableBase
    {

        public void D_ConnectEvent(bool connect)
        {
            //MessageBox.Show(connect.ToString());
        }

        public void D_AccessEvent(bool b)
        {
            //MessageBox.Show(b.ToString());
        }

        public UpbitViewerViewModel(IRegionManager rm)
        {
            Network.Network.Connect += (D_ConnectEvent);

            Access.CheckedAccess += (D_AccessEvent);


            //PublicColor.Colors.Colorinstance.Test();

            Access.UpbitInstance.LoadKey();

            MyTradeClientWebSocket.Instance.SocketStart();

            rm.RegisterViewWithRegion("CRCoinOrder", typeof(CoinOrder));
            rm.RegisterViewWithRegion("CRBidAsk", typeof(BidAskList));
            rm.RegisterViewWithRegion("CRChart", typeof(Chart));
            rm.RegisterViewWithRegion("CRCoinView", typeof(CoinView));
            rm.RegisterViewWithRegion("CRBalance", typeof(Balance));
            rm.RegisterViewWithRegion("CRCoinList", typeof(CoinList));
            //rm.RegisterViewWithRegion("CRMenu", typeof(Menus)); 설정칸을 따로 만들어서 제거
        }
    }
}
