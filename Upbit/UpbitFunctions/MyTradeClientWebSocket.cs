using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Upbit.Functions;
using Upbit.UpbitFunctions.Interface;

namespace Upbit.UpbitFunctions
{
    public class MyTradeClientWebSocket: IAccess
    {
        #region Event
        public delegate void MyTradeEventHandler(Structs.MyTrade connect);
        public static event MyTradeEventHandler MyTradeEvent;
        #endregion Event

        #region Model
        private ClientWebSocket cws;
        private URLs url = new URLs();
        Thread thread;
        private static MyTradeClientWebSocket instance;
        public static MyTradeClientWebSocket Instance
        {
            get
            {
                if (instance is null) { instance = new MyTradeClientWebSocket(); }
                return instance;
            }
        }
        #endregion Model


        #region Functions
        /// <summary>
        /// 소켓 생성 및 실행
        /// </summary>
        /// <returns></returns>
        public async Task SetWebSocket()
        {
            //인증이 안됐을경우
            if (!Access.UpbitInstance.GetAccess())
            {
                return;
            }

            Uri serverUri = new Uri(url.WebSocketTick);
            string AuthToken = JWT.GetJWT();
            JObject ticket = new JObject();
            JObject mytrade = new JObject();
            JObject format = new JObject();
            string Message = "";
            ticket["ticket"] = Guid.NewGuid().ToString();//UUID
            mytrade["type"] = "myTrade";
            format["format"] = "SIMPLE";
            Message = string.Format("[{0},{1},{2}]", ticket.ToString(), mytrade.ToString(), format.ToString());

            try
            {
                cws = new ClientWebSocket();
                cws.Options.SetRequestHeader("Authorization", AuthToken);
                await cws.ConnectAsync(serverUri, CancellationToken.None);


                await SendMessage(cws, Message);

                while (cws.State == System.Net.WebSockets.WebSocketState.Open)
                {
                    await ReceiveMessage(cws);
                }
            }
            catch (Exception ex) 
            {
                
            }


        }

        private void CloseWebsocket(object sender, WebSocketCloseStatus e)
        {
            Console.WriteLine($"WebSocket closed with status: {e}");
        }
        /// <summary>
        /// 전달받은 데이터 처리
        /// </summary>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        private async Task ReceiveMessage(ClientWebSocket webSocket)
        {

            var buffer = new ArraySegment<byte>(new byte[1024]);
            var result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
            if (result.MessageType == WebSocketMessageType.Binary)
            {
                string Message = System.Text.Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                JObject jsonObject = JObject.Parse(Message);
                Structs.MyTrade obj = new Structs.MyTrade();
                obj.ty = (string)jsonObject.GetValue("ty");         //myTrade
                obj.cd = (string)jsonObject.GetValue("cd");         //마켓코드
                obj.ab = (string)jsonObject.GetValue("ab");         //매수 매도 구분
                obj.p = (double)jsonObject.GetValue("p");           //체결가격
                obj.v = (double)jsonObject.GetValue("v");           //체결량
                obj.ouid = (string)jsonObject.GetValue("ouid");     //주문의 고유 아이디
                obj.ot = (string)jsonObject.GetValue("ot");         //주문타입
                obj.ab = (string)jsonObject.GetValue("ab");         //체결의 고유 아이디
                obj.tuid = (string)jsonObject.GetValue("tuid");     //체결의 고유 아이디
                obj.ttms = (long)jsonObject.GetValue("ttms");       //체결타임스탬프
                obj.st = (string)jsonObject.GetValue("st");         //스트림타입
                if (MyTradeEvent != null)
                    MyTradeEvent(obj);

                //기능만 추가해둬가지고 체결이벤트 떨어지는 코드는 아래에 작업하면됨.
                Debug.WriteLine($"Received message: {Message}");
            }
        }

        private async Task SendMessage(ClientWebSocket webSocket, string message)
        {
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
            await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        /// <summary>
        /// 쓰레드를 생성해서 따로 처리함.
        /// </summary>
        public void SocketStart()
        {
            thread = new Thread(async () => await SetWebSocket());
            thread.Start();
        }

        public void D_AccessEvent(bool b)
        {
            if (b)
            {
                //다시 인증완료됐을때
                thread = new Thread(async () => await SetWebSocket());
                thread.Start();
            }
        }

        #endregion Functions


        public MyTradeClientWebSocket()
        {
            Access.CheckedAccess += (D_AccessEvent);
        }
    }
}
