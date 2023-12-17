using System.Resources;
using System.Reflection;
using FileIO;

namespace Language
{
    public class Language
    {
        private string DefaultLang = "kr";
        public string LangType { get; private set; } //한글

        //추가된 언어가있을경우 생성자에 추가해줘야함 없을경우 기본설정언어로 출력
        public List<string> Langs;

        private FncFileIO fileIO = new FncFileIO();
        private string FilePath = "Setting";
        private string FileName = "Language.log";

        #region Event
        public delegate void LangEventHandler();
        public static event LangEventHandler LangChangeEvent;
        #endregion

        private static Language lang;

        public static Language Lang
        {
            get
            {
                if (lang == null) lang = new Language();

                return lang;
            }
        }
        public Language() 
        {

            Langs = new List<string>();
            Langs.Add("kr");
            Langs.Add("en");

            //LangType = DefaultLang;
            if (LoadLang() == false) LangType = DefaultLang;

        }

        #region 리소스 데이터 불러오기
        /// <summary>
        /// 기본적으로 등록된 언어 가져오기
        /// </summary>
        /// <param name="rsname"></param>
        /// <returns></returns>
        private ResourceManager GetResource(string rsname = "")
        {
            string str = "";
            ResourceManager rm = new ResourceManager($"Language.{LangType}.{rsname}", Assembly.GetExecutingAssembly());

            return rm;
        }

        /// <summary>
        /// 공용 언어 가져오기
        /// </summary>
        /// <param name="rsname"></param>
        /// <returns></returns>
        public ResourceManager GetPublicResource(string rsname = "")
        {
            ResourceManager rm = new ResourceManager($"Language.Public.{rsname}", Assembly.GetExecutingAssembly());
            return rm;
        }

        /// <summary>
        /// 언어 변경시 등록된 언어가 없을경우 기본 언어로 로드.
        /// </summary>
        /// <param name="rsname"></param>
        /// <returns></returns>
        private ResourceManager ExceptResource(string rsname = "")
        {
            ResourceManager rm = new ResourceManager($"Language.{DefaultLang}.{rsname}", Assembly.GetExecutingAssembly());
            return rm;
        }

        /// <summary>
        /// 등록된 리소스에서 단어를 가져옴 / 못찾았을경우 해당 리소스 명과 해당 단어 이름을 표시해줌
        /// </summary>
        /// <param name="rsname">리소스명</param>
        /// <param name="word">단어명</param>
        /// <returns></returns>
        public string GetWord(string rsname = "",string word = "")
        {
            string Word = "";
            ResourceManager rm;
            try
            {
                rm = GetResource(rsname);
                Word = rm.GetString(word);
                if(Word is null)
                {
                    rm = ExceptResource(rsname);
                    Word = rm.GetString(word);
                    if (Word is null)
                        Word = $"Error, plz add word, Resource -> {rsname} / Word -> {word}";
                }
            }
            catch (Exception e)
            {
                Word = $"Error, plz add word, Resource -> {rsname} / Word -> {word}";
            }
            
            
            return Word;
        }


        #endregion

        #region Functions
        public void SetLang(string langtype)
        {

            bool Check = Langs.Contains(langtype);

            //선택한 언어가 있을경우
            if (Check)
            {
                this.LangType = langtype;
                SaveLang();

                //언어 변경 이벤트처리
                if(LangChangeEvent is not null)
                {
                    LangChangeEvent();
                }
            }

            
        }


        /// <summary>
        /// 저장된 언어타입 있을경우 로드 없을경우 기본값으로 설정
        /// </summary>
        /// <returns></returns>
        private bool LoadLang()
        {
            bool b = true;
            try
            {
                string type = fileIO.FileRead<string>(FilePath, FileName);
                if (type is null || type == "")
                {
                    LangType = DefaultLang;
                    SaveLang();
                }
                else
                    LangType = type;

            }
            catch (Exception e) 
            {
                b = false;
            }
            return b;
        }

        /// <summary>
        /// 파일로 언어셋팅저장.
        /// </summary>
        private void SaveLang()
        {
            fileIO.FileWrite<string>(FilePath, FileName, LangType);
        }

        #endregion Functions    
    }
}
