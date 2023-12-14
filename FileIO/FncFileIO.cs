using System.Runtime.Serialization.Formatters.Binary;


namespace FileIO
{

    public enum FileTypes
    {
        Create,
        Append,
        OpenOrCreate,
        CreateNew,
        Open,
        Truncate
    }
    public class FncFileIO
    {
        public string AppPath { get; private set; }


        public FncFileIO()
        {
            AppPath = Environment.CurrentDirectory;
        }

        /// <summary>
        /// 폴더가 없을경우 폴더 생성
        /// </summary>
        private void DirExists(string AddPath)
        {
            DirectoryInfo di = new DirectoryInfo($"{this.AppPath}\\{AddPath}");
            if (!di.Exists) { di.Create(); }
        }

        /// <summary>
        /// 읽기전 유무 확인
        /// </summary>
        /// <param name="AddPath">추가 경로</param>
        /// <param name="FileName">파일명 및 확장자</param>
        /// <returns></returns>
        private void FileExists(string AddPath, string FileName)
        {
            DirExists(AddPath);
            FileInfo fi = new FileInfo(GetFilePath(AddPath,FileName));
            if (!fi.Exists) 
            {
                //파일 생성 후 종료 / 안할시 다른 프로세서에서 참조중이라고 오류뜸
                var myFile =  fi.Create();
                myFile.Close();
            }
        }

        /// <summary>
        /// 파일읽기
        /// </summary>
        /// <typeparam name="T">데이터 타입</typeparam>
        /// <param name="AddPath">추가 경로</param>
        /// <param name="FileName">파일명 및 확장자</param>
        /// <returns></returns>
        public List<T> FileListRead<T>(string AddPath, string FileName)
        {
            List<T> values = new List<T>();

            FileExists(AddPath, FileName);


            return values;
        }

        public T FileRead<T>(string AddPath, string FileName)
        {
            T values = default(T);
                
            FileExists(AddPath, FileName);

            try
            {
                using (FileStream fs = new FileStream(GetFilePath(AddPath, FileName), FileMode.Open, FileAccess.Read))
                {
                    if (fs.Length != 0)
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        values = (T)bf.Deserialize(fs);
                    }

                }
            }
            catch(Exception ex)
            {

            }

            return values;
        }

        #region LogFileWrite
        /// <summary>
        /// 파일쓰기
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="AddPath">추가경로</param>
        /// <param name="FileName">파일명</param>
        /// <param name="Type"></param>
        /// <param name="InputData"></param>
        /// <param name="Logs"></param>
        public void LogFileWrite(string AddPath, string FileName, bool Append, string InputData, string Logs)
        {
            FileExists(AddPath, FileName);
            if (InputData is not null)
            {
                using (StreamWriter sw = new StreamWriter(GetFilePath(AddPath, FileName), Append))
                {
                    string LogTime = DateTime.Now.ToString("HH:mm:ss");
                    sw.WriteLine($"[  {Logs} : {LogTime} ] {InputData}");
                }
            }
        }

        public void LogFileWrite<T>(string AddPath, string FileName, bool Append, List<T> InputData, string Logs)
        {
            FileExists(AddPath, FileName);
            if (InputData is not null)
            {
                using (StreamWriter sw = new StreamWriter(GetFilePath(AddPath, FileName), Append))
                {
                    foreach (T t in InputData)
                    {
                        string LogTime = DateTime.Now.ToString("HH:mm:ss");
                        sw.WriteLine($"[  {Logs} : {LogTime} ] {t}");
                    }
                }
            }
        }

        #endregion LogFileWrite

        public void FileWrite<T>(string AddPath, string FileName, T InputData)
        {
            FileExists(AddPath, FileName);
            if (InputData is not null)
            {
                using (FileStream fs = new FileStream(GetFilePath(AddPath, FileName), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, InputData);
                }
            }
        }


        private string GetFilePath(string AddPath, string FileName)
        {
            string path = $"{AppPath}\\{(AddPath == "" ? FileName : AddPath + "\\" + FileName)}";

            return path;
        }
    }
}
