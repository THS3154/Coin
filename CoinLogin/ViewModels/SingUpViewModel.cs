using DB;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Windows;

namespace CoinLogin.ViewModels
{
    public class SingUpViewModel : BindableBase
    {
        #region Prism
        private IRegionManager _rm;
        #endregion

        #region Model
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }
        private string id;
        public string ID
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private string pw;
        public string PW
        {
            get { return pw; }
            set { SetProperty(ref pw, value); }
        }

        private string repw;
        public string RePW
        {
            get { return repw; }
            set { SetProperty(ref repw, value); }
        }

        private string eamil;
        public string Email
        {
            get { return eamil; }
            set { SetProperty(ref eamil, value); }
        }
        #endregion

        #region Event
        private DelegateCommand commandnavigationback;
        public DelegateCommand CommandNavigationBack =>
            commandnavigationback ?? (commandnavigationback = new DelegateCommand(ExecuteCommandNavigationBack));

        void ExecuteCommandNavigationBack()
        {
            _rm.RequestNavigate("LoginView", "Login");
        }

        private DelegateCommand commandsingup;
        public DelegateCommand CommandSinUp =>
            commandsingup ?? (commandsingup = new DelegateCommand(ExecuteCommandSinUp));

        void ExecuteCommandSinUp()
        {
            if (Mssql.Instance.GetConnection() == false)
            {
                MessageBox.Show("서버가 닫혀있습니다. 비회원으로 이용해주세요.");
                ExecuteCommandNavigationBack();
            }
            string message = "";
            if (Name == "")
                message = "이름을 입력해주세요.";
            else if (ID == "")
                message = "아이디를 입력해주세요.";
            else if (PW == "" || RePW == "")
                message = "비밀번호를 입력해주세요.";
            else if (PW != RePW)
                message = "입력하신 비밀번호가 서로 다릅니다.";

            if (message != "")
            {
                MessageBox.Show(message);
                return;
            }

            string Query = $"SELECT * FROM ACCOUNT WHERE ID = '{ID}'";
            try
            {
                bool IDDuplication = Mssql.Instance.HasRows(Query);
                if (IDDuplication)
                {
                    message = "아이디 중복입니다.";
                    MessageBox.Show(message);
                    return;
                }
                Query = $"INSERT INTO ACCOUNT(ID, PW, USERNAME, EMAIL,USEMARKET) VALUES ('{ID}', '{PW}','{Name}' ,'{Email}', 0)";
                Mssql.Instance.ExecuteNonQuery(Query);
                MessageBox.Show("회원가입 완료");
                ExecuteCommandNavigationBack();
            }
            catch(Exception ex) 
            {
                MessageBox.Show("ExecuteCommandSinUp DB오류");
                ExecuteCommandNavigationBack();
            }
            

            
            
        }
        #endregion 

        public SingUpViewModel(IRegionManager rm)
        {
            _rm = rm;
            PW = "";
            RePW = "";

        }
    }
}
