using DB;
using Permission;
using Permission.Event;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Windows;
using Upbit.Functions;

namespace CoinLogin.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        #region Prism
        private IRegionManager _rm;
        private IEventAggregator _ea;
        private PM _pm;
        #endregion

        #region Model
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
        #endregion Model

        #region Event
        private DelegateCommand commandlogin;
        public DelegateCommand CommandLoign =>
            commandlogin ?? (commandlogin = new DelegateCommand(ExecuteCommandLoign));

        void ExecuteCommandLoign()
        {
            if (Mssql.Instance.GetConnection() == false)
            {
                MessageBox.Show("서버가 닫혀있습니다. 비회원으로 이용해주세요.");
                return;
            }

            if ((ID == "" || PW == "" ) || (ID is null || PW is null))
            {
                MessageBox.Show("ID 또는 PW를 입력해 주세요");
                return;
            }
            bool IsLoginCheck = false;
            string query = $"SELECT * FROM ACCOUNT WHERE ID = '{ID}' AND PW = '{PW}'";
            int UserNumber = 0;
            int auth = 0;
            SqlDataReader read = Mssql.Instance.ExecuteReaderQuery(query);
            if(read is not null)
            {
                string username = "";
                while (read.Read())
                {
                    username = read["USERNAME"].ToString();
                    UserNumber = Convert.ToInt32(read["NUMBER"]);
                    auth = Convert.ToInt32(read["LEVEL"]);
                    IsLoginCheck = true;

                }
                if (IsLoginCheck)
                {
                    //로그인 성공시 보냄
                    _pm.SetID(ID);
                    _pm.SetName(username);
                    _pm.SetAuth(auth);
                    _ea.GetEvent<MessageTitleEvent>().Publish($"{username}님 환영합니다.");
                    //사용 후 꼭 닫아줘야함
                    read.Close();

                    query = $"INSERT INTO ACCESSLOG(USERNUMBER, IP, KST) VALUES({UserNumber}, '{Network.Network.GetPublicIP()}', {FncDateTime.DateTimeToInt(DateTime.Now)})";
                    Mssql.Instance.ExecuteNonQuery(query);
                    _rm.RequestNavigate("Main", "Main");
                }
                else
                {
                    MessageBox.Show("등록된 계정이 없습니다.");
                }

            }
            else
            {
                MessageBox.Show("DB오류");
            }

        }

        private DelegateCommand commandnonmember;
        public DelegateCommand CommandNonMember =>
            commandnonmember ?? (commandnonmember = new DelegateCommand(ExecuteCommandNonMember));

        void ExecuteCommandNonMember()
        {
            //비회원 접속은 하루 1회로 제한, 단 서버가 닫혀있을땐 무제한으로 가능. 현재개인컴 서버라 OFF될일이 많음.



            if (Mssql.Instance.GetConnection())
            {
                string MyIP = Network.Network.GetPublicIP();
                DateTime time = DateTime.Now;
                time = time.AddHours(DateTime.Now.Hour * -1).AddMinutes(DateTime.Now.Minute * -1).AddSeconds(DateTime.Now.Second * -1);
                uint StartDay = FncDateTime.DateTimeToInt(time);
                uint EndDay = FncDateTime.DateTimeToInt(time.AddDays(1));

                string query = $"SELECT * FROM ACCESSLOG WHERE ({StartDay} <= KST AND KST < {EndDay}) AND IP ='{MyIP}' AND USERNUMBER = 0";

                bool IsNonMemberAccessCheck = Mssql.Instance.HasRows(query);    //접속기록 있을경우 true / 없을경우 false;
                if (IsNonMemberAccessCheck)
                {
                    //접속 불가
                    MessageBox.Show("비회원은 하루 최대 1회 접속가능합니다.");
                    return;
                }
                else
                {
                    //비회원 접속기록 보냄.
                    query = $"INSERT INTO ACCESSLOG(USERNUMBER, IP, KST) VALUES(0, '{MyIP}', {FncDateTime.DateTimeToInt(DateTime.Now)})";
                    
                    //테스트하기위해서 비회원 잠금기능 해제시켜둠 계속 DB날리기 너무 귀찮고 로그인하기도 귀찮음
                    //Mssql.Instance.ExecuteNonQuery(query);
                }
            }
            _rm.RequestNavigate("Main", "Main");
            _ea.GetEvent<MessageTitleEvent>().Publish($"Upbit");



        }
        #endregion Event

        

        public LoginViewModel(IRegionManager rm, IEventAggregator ea, PM pm)
        {
            _rm = rm;
            _pm = pm;
            _ea = ea;
            PW = "";
        }
    }
}
