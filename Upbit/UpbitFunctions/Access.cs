using FileIO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Upbit.Functions;

namespace Upbit.UpbitFunctions
{
    public class Access
    {

        public delegate void AccessEventHandler(bool connect);
        public static event AccessEventHandler CheckedAccess;

        private FncFileIO fileIO = new FncFileIO();

        private string KeyPath = "Access";
        private string KeyFile = "Key.log";

        private bool IsServerActive = true;             //서버 열림 체크
        private bool Maintainance = false;              //서버 점검이 있을경우 true로 변경
        
        private DateTime ServerDownTime = DateTime.Now; //서버 닫히는 시간
        private DateTime ServerUpTime = DateTime.Now;   //서버 열리는 시간
        private Timer timer;


        //키값
        private string Access_Key = "";
        private string Secret_Key = "";
        private bool _Access = false;
        private static Access instance;

        public static Access UpbitInstance
        {
            get
            {
                if (instance == null) instance = new Access();

                return instance;
            }
        }


        private void StartTimer(DateTime time)
        {
            timer = new Timer(ServerTimer);
            int sec = (time - DateTime.Now).Seconds;
            timer.Change(sec * 1000, 0);
        }
        private void StopTimer()
        {
            timer.Dispose();
        }
        private void ServerTimer(object sender)
        {
            IsServerActive = !IsServerActive;
            if(IsServerActive == false)
            {
                _Access = false;
                Maintainance = true;
                CheckedAccess(_Access);
                StopTimer();
                StartTimer(ServerUpTime);
            }
            else
            {
                LoadKey();
                Maintainance = false;
                StopTimer();
            }
            
        }
        
        public void GetMaintainace(DateTime DownTime, DateTime UpTime)
        {
            Maintainance = false;
            ServerDownTime = DownTime;
            ServerUpTime = UpTime;
            StartTimer(DownTime);
        }
        public void GetUpdateMaintainance(DateTime UpTime)
        {
            ServerUpTime = UpTime;
            if (Maintainance)
            {
                StopTimer();
                StartTimer(ServerUpTime);
            }
        }
        //해당 키 등록
        public void SetKeys(string Access, string Secret)
        {
            Access_Key = Access;
            Secret_Key = Secret;

            SaveKey(Access, Secret);

            //해당값이 True 경우에만 주문 계좌조회 등등 키가 필요한 작업들 실행가능
            if (IsServerActive)
            {
                _Access = CheckedKey();
            }
            
            if(CheckedAccess is not null)
            {
                CheckedAccess(_Access);
            }
        }

        private bool CheckedKey()
        {
            Task<bool> task = Task.Run(() => KeyCheck());
            bool accounts = task.Result;

            return accounts; 
        }
        public string GetAccessKey()
        {
            return Access_Key;
        }

        public string GetSecretKey()
        {
            return Secret_Key;
        }

        public void ExceptionKey()
        {
            _Access = false;
            CheckedAccess(_Access);
        }

        /// <summary>
        /// 키가 유무 체크 후 사용가능함 함수들 사용하기위해
        /// </summary>
        /// <returns></returns>
        public bool GetAccess()
        {
            if (!IsServerActive)
            {
                _Access = false;
                CheckedAccess(_Access);
            }

            return _Access;
        }

        /// <summary>
        /// 기존에 저장된 Key값들이 있을경우에 불러오는 함수
        /// </summary>
        /// <returns></returns>
        public void LoadKey()
        {
            FileIOAccess Keys  = fileIO.FileRead<FileIOAccess>(KeyPath, KeyFile);
            Access_Key = Keys.AccessKey is null ? "" : Keys.AccessKey;
            Secret_Key = Keys.SecretKey is null ? "" : Keys.SecretKey;

            if (IsServerActive)
            {
                _Access = CheckedKey();
            }
            if (CheckedAccess is not null)
            {
                CheckedAccess(_Access);
            }

        }

        /// <summary>
        /// 다이얼로그에서 키값을 등록하기위한 함수
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="sc"></param>
        private void SaveKey(string ac, string sc)
        {
            FileIOAccess Keys = new FileIOAccess();
            Keys.SecretKey = sc;
            Keys.AccessKey = ac;
            fileIO.FileWrite(KeyPath, KeyFile, Keys);
        }


        private async Task<bool> KeyCheck()
        {
            URLs url = new URLs();
            bool ListResult;
            string body = "";
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url.Account),
                    Headers =
                    {
                        { "accept", "application/json" },
                        { "Authorization", JWT.GetJWT() },
                    },
                };


                using (var response = await client.SendAsync(request))
                {
                    //response.EnsureSuccessStatusCode();
                    body = await response.Content.ReadAsStringAsync();

                    JObject jmain = JObject.Parse(body);
                    JToken error = jmain.Value<JToken>("error");
                    string errorname = error.Value<string>("name");
                    Language.Lang.Upbit Lang = new Language.Lang.Upbit();
                    string KeyError = Lang.LKeyError;
                    string KeyComment = Lang.LKeyComment;
                    string PrintMessage = "";
                    if (errorname == "invalid_query_payload")        PrintMessage = "JWT 헤더의 페이로드가 올바르지 않습니다.";
                    else if (errorname == "jwt_verification") PrintMessage = "JWT 헤더 검증에 실패했습니다.";
                    else if (errorname == "expired_access_key") PrintMessage = "API 키가 만료되었습니다.";
                    else if (errorname == "nonce_used") PrintMessage = "이미 요청한 nonce값이 다시 사용되었습니다.";
                    else if (errorname == "no_authorization_i_p") PrintMessage = "허용되지 않은 IP 주소입니다.";
                    else if (errorname == "out_of_scope") PrintMessage = "허용되지 않은 기능입니다.";
                    MessageBox.Show(KeyError + "\n" + PrintMessage);
                    Debug.WriteLine(KeyError + "\n" + PrintMessage);
                    Logs.Loginstance.UpbitLog(PrintMessage);
                    return false;
                }
            }
            catch (Exception e)
            {
                return true;
                
            }


        }


        public Access()
        {
            
        }
    }
}
