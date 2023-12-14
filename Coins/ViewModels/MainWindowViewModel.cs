using Prism.Mvvm;
using Upbit.Functions;
using FileIO;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows;
using Network;
using System.Windows.Controls;
using Prism.Regions;
using Upbit.Views;
using Upbit.UpbitFunctions;
using Upbit.UpbitFunctions.Interface;
using Coins.Views;
using System.Windows.Media;
using PublicColor;
using System.Threading;
using Prism.Commands;
using System.Linq;
using System.Diagnostics;
using Prism.Ioc;
using Permission;
using Prism.Events;
using Permission.Event;

namespace Coins.ViewModels
{
    public class MainWindowViewModel : BindableBase, INetwork, IAccess, IColors
    {
        #region Prism
        private readonly IRegionManager _rm;
        private readonly IEventAggregator _ea;
        #endregion


        #region Models
        private string _title = "Coin";

        //FileTypes
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #endregion Models

        #region Colors
        /// <summary>
        /// 색상 업데이트 이벤트
        /// </summary>
        public void EventUpdateColor()
        {
            ColorText = PublicColor.Colors.Colorinstance.ColorText;
        }
        private Brush colortext;
        public Brush ColorText
        {
            get { return colortext; }
            set { SetProperty(ref colortext, value); }
        }
        #endregion Colors

        private DelegateCommand<string> _fieldName;
        public DelegateCommand<string> CommandName =>
            _fieldName ?? (_fieldName = new DelegateCommand<string>(ExecuteCommandName));

        void ExecuteCommandName(string parameter)
        {

            _rm.RequestNavigate("Main", parameter);
        }

        private void SetTitle(string title)
        {
            Title = title;
        }

        public MainWindowViewModel(IRegionManager rm, IEventAggregator ea, PM pm)
        {
            _rm = rm;
            _ea = ea;
            Debug.WriteLine(pm.ToString());

            //네트워크 연결 이벤트 추가
            Network.Network.Connect += (D_ConnectEvent);
            //색변경 이벤트 추가
            PublicColor.Colors.ColorUpdate += (EventUpdateColor);


            _ea.GetEvent<MessageTitleEvent>().Subscribe(SetTitle);
            //로그인창
            _rm.RegisterViewWithRegion("Main", typeof(CoinLogin.Views.LoginMain));
            //MessageBox.Show(Network.Network.Connected.ToString());


        }

        public void D_ConnectEvent(bool connect)
        {
            //MessageBox.Show(connect.ToString());
        }

        public void D_AccessEvent(bool b)
        {
            //MessageBox.Show(b.ToString());
        }

    }
}
