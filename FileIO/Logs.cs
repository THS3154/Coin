using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIO
{

    

    public class Logs
    {
        private static Logs loginstance;

        public static Logs Loginstance
        {
            get
            {
                if (loginstance == null) loginstance = new Logs();

                return loginstance;
            }
        }


        #region Path
        private string PublicPath { get; set; }
        private string SVPath { get; set; }
        private string SVFileName { get; set; }

        private string PMPath { get; set; }
        private string PMFileName { get; set; }

        private string NetPath { get; set; }
        private string NetFileName { get; set; }
        private string DBPath { get; set; }
        private string DBFileName { get; set; }

        private string UpbitSCPath { get; set; }
        private string UpbitSCFileName { get; set; }

        private string UpbitLogPath { get; set; }
        private string UpbitLogFileName { get; set; }

        private string SettingLogPath { get; set; }
        private string SettingLogFileName { get; set; }

        #endregion

        private FncFileIO fio { get; set; }

        public Logs()
        {
            //로그 경로 지정
            PublicPath = Suffix("Logs");
            SVPath = Suffix("Server");
            PMPath = Suffix("Permission");
            NetPath = Suffix("Network");
            DBPath = Suffix("DataBase");
            UpbitSCPath = Suffix("UpbitSCPath");
            UpbitLogPath = Suffix("UpbitLogPath");
            SettingLogPath = Suffix("SettingLogPath");

            string Times = DateTime.Now.ToString("yyyy.MM.dd");
            //로그 파일명 지정 날짜별로 변경되서 저장 할 예정
            SVFileName = Suffix(Times + "SV", ".log");
            PMFileName = Suffix(Times + "PM", ".log");
            NetFileName = Suffix(Times + "Net", ".log");
            DBFileName = Suffix(Times + "DB", ".log");
            UpbitSCFileName = Suffix(Times + "UpbitSC", ".log");
            UpbitLogFileName = Suffix(Times + "UpbitLogs", ".log");
            SettingLogFileName = Suffix(Times + "Setting", ".log");
            fio = new FncFileIO();

        }

        #region Suffix Functions
        /// <summary>
        /// 경로 끝 역슬래쉬 붙여줌.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string Suffix(string str)
        {
            return str + "\\";
        }

        private string Suffix(string str,string suf)
        {
            return str + suf;
        }
        #endregion


        public void DBLog(string Data)
        {
            fio.LogFileWrite(DBPath, DBFileName, true, Data, "DBLOG");
        }

        public void NetLog(string Data)
        {
            fio.LogFileWrite(NetPath, NetFileName, true, Data, "NETWORKLOG");
        }

        public void PermissionLog(string Data)
        {
            fio.LogFileWrite(PMPath, PMFileName, true, Data, "PERMISSIONLOG");
        }

        public void WebSocketLog(string Data)
        {
            fio.LogFileWrite(UpbitSCPath, UpbitSCFileName, true, Data, "WEBSOCKETLOG");
        }

        public void UpbitLog(string Data)
        {
            fio.LogFileWrite(UpbitLogPath, UpbitLogFileName, true, Data, "UPBITLOG");
        }

        public void SettingLog(string Data)
        {
            fio.LogFileWrite(SettingLogPath, SettingLogFileName, true, Data, "SETTING");
        }


    }
}
