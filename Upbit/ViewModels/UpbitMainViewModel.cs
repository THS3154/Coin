using Network;
using Permission;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upbit.UpbitFunctions;
using Upbit.UpbitFunctions.Interface;
using Upbit.Views;

namespace Upbit.ViewModels
{
    public class UpbitMainViewModel : BindableBase,INetwork,IAccess
    {
        public void D_ConnectEvent(bool connect)
        {
            //MessageBox.Show(connect.ToString());
        }

        public void D_AccessEvent(bool b)
        {
            //MessageBox.Show(b.ToString());
        }

        public UpbitMainViewModel(IRegionManager rm,IDialogService dialog,PM pm)
        {
            

            Debug.WriteLine(pm.ToString());
            
        
        
        
            //rm.RegisterViewWithRegion("CRMenu", typeof(CoinList));
            rm.RegisterViewWithRegion("CRBalance", typeof(CoinList));
            rm.RegisterViewWithRegion("CRCoinList", typeof(CoinList));
            rm.RegisterViewWithRegion("CRCoinView", typeof(CoinList));
        }
    }
}
