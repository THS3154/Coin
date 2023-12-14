using FileIO;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

namespace Network
{
    public static class Network
    {
        public static bool Connected { get; private set; }

        /// <summary>
        /// 현재 인터넷 연결상태 확인
        /// </summary>
        public delegate void EventHandler(bool connect);
        public static event EventHandler Connect;

        static Network()
        {
            Connected = IsInternetConnected();
            NetworkChange.NetworkAvailabilityChanged += (s, ne) =>
            {
                if (ne.IsAvailable)
                {
                    Connected = true;
                    Connect(true);
                }
                else
                {
                    Connected = false;
                    Connect(false);
                }
            };
        }

        public static string GetPublicIP()
        {
            string publicIp = new WebClient().DownloadString("http://ipinfo.io/ip").Trim();

            //null경우 Get Internal IP를 가져오게 한다.
            if (String.IsNullOrWhiteSpace(publicIp))
            {
                Debug.WriteLine("외부IP 못찾음");
            }

            return publicIp;
        }

        public static bool IsInternetConnected()
        {
            const string NCSI_TEST_URL = "http://www.msftncsi.com/ncsi.txt";
            const string NCSI_TEST_RESULT = "Microsoft NCSI";
            const string NCSI_DNS = "dns.msftncsi.com";
            const string NCSI_DNS_IP_ADDRESS = "131.107.255.255";

            try
            {
                // Check NCSI test link
                var webClient = new WebClient();
                string result = webClient.DownloadString(NCSI_TEST_URL);
                if (result != NCSI_TEST_RESULT)
                {
                    return false;
                }

                // Check NCSI DNS IP
                var dnsHost = Dns.GetHostEntry(NCSI_DNS);
                if (dnsHost.AddressList.Count() < 0 || dnsHost.AddressList[0].ToString() != NCSI_DNS_IP_ADDRESS)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

            return true;
        }
    }
}