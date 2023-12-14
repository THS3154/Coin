using System;

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
        //이름
        public string Name { get; private set; }

        //로그인시 아이디 저장
        public string ID { get; private set; }

        /// <summary>
        /// 권한 등급 값 Base = 0
        /// </summary>
        public int Auth { get; private set; }

        //권한 목록
        public List<SAuth> AuthList { get; private set; }
        
        //내가 가진 권한들
        public List<SAuth> MyAuth { get; private set; }

        #endregion


        /// <summary>
        /// 생성자
        /// </summary>
        public PM()
        {
            MyAuth = new List<SAuth>();
            AuthList = new List<SAuth>();
            Auth = 0;
            ID = "";
            Name = "";
        }


        #region AuthList Functions
        /// <summary>
        /// 권한목록 추가
        /// </summary>
        /// <param name="auth">추가될 권한</param>
        public void AddAuthList(SAuth auth)
        {
            AuthList.Add(auth);
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

        #region MyAuth Functions
        /// <summary>
        /// 개인권한 추가
        /// </summary>
        /// <param name="auth"></param>
        public void AddMyAuth(SAuth auth)
        {
            MyAuth.Add(auth);
            MyAuthChange(auth);
        }

        public void RemoveMyAuth(SAuth auth)
        {
            MyAuth.Remove(auth);
            MyAuthChange(auth);
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
        #endregion

        #region Active Functions

        /// <summary>
        /// 해당 권한 기능이 열려있으면 활성화
        /// </summary>
        /// <param name="auth">활성화 체크할 변수</param>
        /// <returns></returns>
        public bool ActiveAuth(SAuth auth)
        {
            return MyAuth.Contains(auth);
        }


        //서버에서 특정 기능을 닫을 함수를 추가 할 예정

        #endregion
    }
}