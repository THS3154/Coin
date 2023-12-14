using CoinLogin.Views;
using DB;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;

namespace CoinLogin.ViewModels
{
    public class LoginMainViewModel : BindableBase
    {
        private IRegionManager _rm;

        private DelegateCommand commandsingup;
        public DelegateCommand CommandSingUp =>
            commandsingup ?? (commandsingup = new DelegateCommand(ExecuteCommandSingUp));

        void ExecuteCommandSingUp()
        {
            if (Mssql.Instance.GetConnection() == false)
            {
                MessageBox.Show("서버가 닫혀있습니다. 비회원으로 이용해주세요.");
            }
            else
            {
                _rm.RequestNavigate("LoginView", "SingUp");
            }
        }


        public LoginMainViewModel(IRegionManager rm)
        {
            _rm = rm;
            _rm.RegisterViewWithRegion("LoginView", typeof(Login));
        }
    }
}
