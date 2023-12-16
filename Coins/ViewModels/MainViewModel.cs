using Coins.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using Upbit.Views;

namespace Coins.ViewModels
{
	public class MainViewModel : BindableBase
	{
        #region Prism
        IRegionManager _rm;
        #endregion
        public MainViewModel(IRegionManager rm)
        {
            _rm = rm;

            rm.RegisterViewWithRegion("SideMenu", typeof(SideMenu));
            rm.RegisterViewWithRegion("Viewer", typeof(DashboardViewer));
            //_rm.RequestNavigate("Viewer", "UpbitViewer");
        }
	}
}
