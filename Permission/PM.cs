using DB;
using System;
using System.Data.SqlClient;

namespace Permission
{
    public class PM
    {
        #region Events
        public delegate void AllAuthListChangeHandler(SAuth auth);
        public event AllAuthListChangeHandler? AllAuthListChange;

        public delegate void MyAuthChangeHandler(SAuth auth);
        public event MyAuthChangeHandler? MyAuthChange;
        #endregion

        #region Values
        private int Admin = 9999;

        //이름
        public string Name { get; private set; }

        //로그인시 아이디 저장
        public string ID { get; private set; }

        /// <summary>
        /// 권한 등급 값 Base = 0 / 회원 1 / 어드민 9999
        /// </summary>
        public int Auth { get; private set; }

        //권한 목록
        public List<SAuth> AuthList { get; private set; }
        
        //내가 가진 권한들

        #endregion


        /// <summary>
        /// 생성자
        /// </summary>
        public PM()
        {
            AuthList = GetAuthList();
            Auth = 0;
            ID = "";
            Name = "";
        }


        #region AuthList Functions

        private List<SAuth> GetAuthList()
        {
            List<SAuth> list = new List<SAuth>();
            string query = $"SELECT * FROM AUTHLIST";
            
            SqlDataReader read = Mssql.Instance.ExecuteReaderQuery(query);
            if(read is not null)
            {
                while (read.Read())
                {
                    SAuth at = new SAuth();
                    at.AuthName = read["NAME"].ToString();
                    at.AuthLevel = Convert.ToInt32(read["LEVEL"]);
                    at.IsEnable = Convert.ToBoolean(read["ENABLE"]);
                    at.IsShow = (Convert.ToBoolean(read["SHOW"]) ? 0 : 2);

                    list.Add(at);
                }
                read.Close();//읽어온 쿼리 닫음.
            }
            else
            {
                //DB연결 없을때 오픈된 설정만 등록
                //최소한 기능만 추가
                SAuth at = new SAuth();
                at.AuthName = "업비트API KEY등록";
                at.AuthLevel = 0;
                at.IsEnable = true;
                at.IsShow = 1;

                list.Add(at);

                at.AuthName = "색 변경";
                list.Add(at);

                at.AuthName = "언어 변경";
                list.Add(at);
                
            }
            


            return list;
        }

        /// <summary>
        /// 권한목록 추가
        /// </summary>
        /// <param name="auth">추가될 권한</param>
        public void AddAuthList(SAuth auth)
        {
            AuthList = GetAuthList();
            AllAuthListChange(auth);
        }

        /// <summary>
        /// 권한목록 제거
        /// </summary>
        /// <param name="auth">제거할 권한</param>
        public void RemoveAuthList(SAuth auth)
        {
            AuthList.Remove(auth);
            AllAuthListChange(auth);
        }
        #endregion


        #region Info Functions
        /// <summary>
        /// 이름 셋팅
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 아이디 셋팅
        /// </summary>
        /// <param name="id"></param>
        public void SetID(string id)
        {
            this.ID = id;
        }
        public void SetAuth(int auth)
        {
            this.Auth = auth;
        }
        #endregion

        #region Active Functions

        /// <summary>
        /// 해당 권한 기능이 열려있으면 활성화
        /// </summary>
        /// <param name="auth">활성화 체크할 변수</param>
        /// <returns></returns>
        public bool AuthCheck(string authname)
        {
            SAuth auth = new SAuth();
            bool _b = false;
            foreach(var i in AuthList)
            {
                if(i.AuthName == authname)
                {
                    auth = i;
                    break;
                }
            }
            if(auth.AuthName != "" && auth.AuthName is not null)
            {
                if (this.Auth >= auth.AuthLevel)
                {
                    _b = true;
                }
            }
            
            return _b;
            
        }

        public bool AdminCheck()
        {
            bool _b = false;
            if (Auth >= Admin)
                _b = true;
            return _b;
        }

        //서버에서 특정 기능을 닫을 함수를 추가 할 예정

        #endregion
    }
}