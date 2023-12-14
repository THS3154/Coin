using Language;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PublicColor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Upbit.Event;
using Upbit.Functions;
using Upbit.Functions.Interface;
using Upbit.UpbitFunctions;
using static Upbit.Structs;

namespace Upbit.ViewModels
{
    public struct line
    {
        public double FromX { get; set; }
        public double FromY { get; set; }
        public double ToX { get; set; }
        public double ToY { get; set; }
        public Brush LineColor { get; set; }
        public double LineThickness { get; set; }
    }

    public struct CandlePosition
    {
        public int X { get; set; }
        public int Y { get; set; }
    }


    public class ChartViewModel : BindableBase, IWebSocketTicker, ILanguage, IColors
    {

        #region Lang

        private Language.Lang.PublicCoin lang;
        public Language.Lang.PublicCoin Lang
        {
            get { if (lang is null) lang = new Language.Lang.PublicCoin(); return lang; }
            set { SetProperty(ref lang, value); }
        }
        public virtual void SetLanguage()
        {
            Lang.UpdateLang();
        }

        #endregion Lang

        #region Models
        private List<line> horline;
        public List<line> HorLine
        {
            get { return horline; }
            set { SetProperty(ref horline, value); }
        }

        private List<line> verline;
        public List<line> VerLine
        {
            get { return verline; }
            set { SetProperty(ref verline, value); }
        }

        private List<Structs.Candle> tickdata;
        public List<Structs.Candle> TickData
        {
            get { return tickdata; }
            set { SetProperty(ref tickdata, value); }
        }

        private ObservableCollection<Structs.Tick> printtick;
        public ObservableCollection<Structs.Tick> PrintTick
        {
            get { return printtick; }
            set { SetProperty(ref printtick, value); }
        }

        private double tickwidth;
        public double TickWidth
        {
            get { return tickwidth; }
            set { SetProperty(ref tickwidth, value); }
        }

        //차트 캔들 비율값
        private double charttickheight;
        public double ChartTickHeight
        {
            get { return charttickheight; }
            set { SetProperty(ref charttickheight, value); }
        }

        private int gridvolumeheight = 100;
        public int GridVolumeHeight
        {
            get { return gridvolumeheight; }
            set { SetProperty(ref gridvolumeheight, value); }
        }

        private double volumetickheight;
        public double VolumeTickHeight
        {
            get { return volumetickheight; }
            set { SetProperty(ref volumetickheight, value); }
        }

        private Thickness tickcanvasmargin;
        public Thickness TickCanvasMargin
        {
            get { return tickcanvasmargin; }
            set { SetProperty(ref tickcanvasmargin, value); }
        }

        private Thickness tickmargin;
        public Thickness TickMargin
        {
            get { return tickmargin; }
            set { SetProperty(ref tickmargin, value); }
        }

        //차트 최대 최소금액
        private double minprice;
        public double MinPrice
        {
            get { return minprice; }
            set { SetProperty(ref minprice, value); }
        }
        private double maxprice;
        public double MaxPrice
        {
            get { return maxprice; }
            set { SetProperty(ref maxprice, value); }
        }
        private string Market = "";





        private int lines = 30;             //그리드에 그려지는 라인개수
        private int KeepPrintCursor = 0;
        private int PrintCandle = 200;
        private const int PrintDefaultCandle = 200;
        private const int PrintMaxCandle = 360;   //출력 최대 캔들 수
        private const int PrintMinCandle = 50;    //출력 최소 캔들 수
        private const double CandleMargin = 1.5;

        private double GridHeight;
        private int printcursor;
        public int PrintCursor
        {
            get { return printcursor; }
            set { SetProperty(ref printcursor, value); }
        }


        private string prinmvolume;
        public string PrintVolume
        {
            get
            {
                return prinmvolume;
            }

            set
            {

                SetProperty(ref prinmvolume, value);
            }
        }





        //고가
        private string printhp;
        public string PrintHp
        {
            get { return printhp; }
            set { SetProperty(ref printhp, value); }
        }

        //저가
        private string printlp;
        public string PrintLp
        {
            get { return printlp; }
            set { SetProperty(ref printlp, value); }
        }

        //시가
        private string printop;
        public string PrintOp
        {
            get { return printop; }
            set { SetProperty(ref printop, value); }
        }

        //종가
        private string printtp;
        public string PrintTp
        {
            get { return printtp; }
            set { SetProperty(ref printtp, value); }
        }

        private string printrate;
        public string PrintRate
        {
            get { return printrate; }
            set { SetProperty(ref printrate, value); }
        }

        private string printtime;
        public string PrintTime
        {
            get { return printtime; }
            set { SetProperty(ref printtime, value); }
        }
        private double maxvolume;
        public double MaxVolume
        {
            get { return maxvolume * 1.1; }//10퍼 늘려서 출력
            set { SetProperty(ref maxvolume, value); }
        }


        private const int GapYAxis = 22;//Y축 마진 22계산
        const int LeaveCnt = 5; //캔들 최소 출력치
        string FLen = "F8";     //소수점 출력

        private enum YAxis
        {
            Max = 0,
            Min = 1,
            Cursor = 2,
            Now = 3,
            Avg = 4
        }

        //사이드 커서 출력
        private ObservableCollection<Structs.ChartSideCursor> printchartyaxis;
        public ObservableCollection<Structs.ChartSideCursor> PrintChartYAxis
        {
            get { return printchartyaxis; }
            set { SetProperty(ref printchartyaxis, value); }
        }


        private ObservableCollection<Structs.ChartSideCursor> printchartxaxis;
        public ObservableCollection<Structs.ChartSideCursor> PrintChartXAxis
        {
            get { return printchartxaxis; }
            set { SetProperty(ref printchartxaxis, value); }
        }


        //분 / 일 / 주 / 월 타입선택
        private int PreMinNumber = 1;
        private Visibility visiblecombboxmin;
        public Visibility VisibleComboBoxMin
        {
            get { return visiblecombboxmin; }
            set { SetProperty(ref visiblecombboxmin, value); }
        }
        private string ticktype;
        public string TickType
        {
            get { return ticktype; }
            set { SetProperty(ref ticktype, value); }
        }
        private List<string> listticktype;
        public List<string> ListTickType
        {
            get { return listticktype; }
            set { SetProperty(ref listticktype, value); }
        }

        private List<int> listtickmin;
        public List<int> ListTickMin
        {
            get { return listtickmin; }
            set { SetProperty(ref listtickmin, value); }
        }

        //분틱으로 검색시 틱시간 
        private int minticktime;
        public int MinTickTime
        {
            get { return minticktime; }
            set { SetProperty(ref minticktime, value); }
        }




        private PublicFunctions pfnc = new PublicFunctions();
        #endregion Models


        #region Prism
        private IEventAggregator _ea;
        #endregion Prism

        #region Colors
        /// <summary>
        /// 색상 업데이트 이벤트
        /// </summary>
        public void EventUpdateColor()
        {
            MyColors.ColorText = PublicColor.Colors.Colorinstance.ColorText;
            MyColors.ColorBack = PublicColor.Colors.Colorinstance.ColorBack;
            MyColors.ColorBid = PublicColor.Colors.Colorinstance.ColorBid;
            MyColors.ColorAsk = PublicColor.Colors.Colorinstance.ColorAsk;
            MyColors.ColorChartOrderAsk = PublicColor.Colors.Colorinstance.ColorChartOrderAsk;
            MyColors.ColorChartOrderAskText = PublicColor.Colors.Colorinstance.ColorChartOrderAskText;
            MyColors.ColorChartOrderBid = PublicColor.Colors.Colorinstance.ColorChartOrderBid;
            MyColors.ColorChartOrderBidText = PublicColor.Colors.Colorinstance.ColorChartOrderBidText;

            MyColors.ColorChartCursor = PublicColor.Colors.Colorinstance.ColorChartCursor;
            MyColors.ColorChartCursorText = PublicColor.Colors.Colorinstance.ColorChartCursorText;
            MyColors.ColorChartAvg = PublicColor.Colors.Colorinstance.ColorChartAvg;
            MyColors.ColorChartAvgText = PublicColor.Colors.Colorinstance.ColorChartAvgText;
            MyColors.ColorChartMax = PublicColor.Colors.Colorinstance.ColorChartMax;
            MyColors.ColorChartMaxText = PublicColor.Colors.Colorinstance.ColorChartMaxText;
            MyColors.ColorChartMin = PublicColor.Colors.Colorinstance.ColorChartMin;
            MyColors.ColorChartMinText = PublicColor.Colors.Colorinstance.ColorChartMinText;
            MyColors.ColorChartNow = PublicColor.Colors.Colorinstance.ColorChartNow;
            MyColors.ColorChartNowText = PublicColor.Colors.Colorinstance.ColorChartNowText;

            //차트 색상변경
            ChangeChartColor();
            if(PrintChartYAxis is not null)
            {
                if (PrintChartYAxis.Count >= 5)
                {
                    PrintChartYAxis[(int)YAxis.Max].Color = MyColors.ColorChartMaxText;
                    PrintChartYAxis[(int)YAxis.Max].BackColor = MyColors.ColorChartMax;

                    PrintChartYAxis[(int)YAxis.Min].Color = MyColors.ColorChartMinText;
                    PrintChartYAxis[(int)YAxis.Min].BackColor = MyColors.ColorChartMin;

                    PrintChartYAxis[(int)YAxis.Cursor].Color = MyColors.ColorChartCursorText;
                    PrintChartYAxis[(int)YAxis.Cursor].BackColor = MyColors.ColorChartCursor;

                    PrintChartYAxis[(int)YAxis.Now].Color = MyColors.ColorChartNowText;
                    PrintChartYAxis[(int)YAxis.Now].BackColor = MyColors.ColorChartNow;

                    PrintChartYAxis[(int)YAxis.Avg].Color = MyColors.ColorChartAvgText;
                    PrintChartYAxis[(int)YAxis.Avg].BackColor = MyColors.ColorChartAvg;

                    for(int i = 5;i<PrintChartYAxis.Count;i++)
                    {
                        if (PrintChartYAxis[i].Type == "bid")
                        {
                            PrintChartYAxis[i].Color = MyColors.ColorChartOrderBidText; 
                            PrintChartYAxis[i].BackColor = MyColors.ColorChartOrderBid;
                        }
                        else if (PrintChartYAxis[i].Type == "ask") 
                        {
                            PrintChartYAxis[i].Color = MyColors.ColorChartOrderAskText;
                            PrintChartYAxis[i].BackColor = MyColors.ColorChartOrderAsk; 
                        }
                    }
                }
            }
            
        }

        private PublicColor.Colors mycolors;
        public PublicColor.Colors MyColors
        {
            get
            {
                if (mycolors is null) mycolors = new PublicColor.Colors();
                return mycolors;
            }
            set { SetProperty(ref mycolors, value); }
        }

        /// <summary>
        /// 색상변경 이벤트가 발생했을경우 캔들 색상을 변경시켜줌.
        /// 23.11.14
        /// </summary>
        private void ChangeChartColor()
        {
            if (PrintTick is not null)
            {
                for (int i = 0; i < PrintTick.Count; i++)
                {
                    Structs.Tick _t = PrintTick[i];

                    _t.Colors = SetCandleColor(_t.OP, _t.TP);

                    PrintTick[i] = _t;
                }
            }
        }
        #endregion Colors

        #region Event

        /// <summary>
        /// 캔버스에 그리드를 그리는 이벤트 Load때와 SizeChange때 발생함.
        /// 추후 시간에 따른 라인으로 변경할예정
        /// 23.11.11
        /// </summary>
        private DelegateCommand<object> commandgridlines;
        public DelegateCommand<object> CommandGridLines =>
            commandgridlines ?? (commandgridlines = new DelegateCommand<object>(ExecuteCommandGridLines));

        void ExecuteCommandGridLines(object sender)
        {
            if (sender is not Grid)
                return;

            Grid grid = (Grid)sender;
            MyGrid = (Grid)sender;
            GridHeight = grid.ActualHeight;
            TickWidth = Math.Max(grid.ActualWidth / PrintCandle, 1.6) - CandleMargin;
            ChartTickHeight = GridHeight / (MaxPrice - MinPrice);
            VolumeTickHeight = GridVolumeHeight / MaxVolume;

            double height = (grid.ActualHeight / lines);
            double width = (grid.ActualWidth / lines);
            HorLine = DrawLine(height, true);
            VerLine = DrawLine(width, false);

            if (PrintChartYAxis is not null)
            {
                PrintChartYAxis[(int)YAxis.Min].Y = GridHeight + GapYAxis;
            }

            GetCandleTime();
            GetCoinPrice();
        }



        private bool CandleMouseState = false;      //현재는 클릭시 작동 추후 Left Right나눠야함 23.11.11
        private double FirstXPos = 0;               //눌러졌을때 X축값을 받아와서 Move쪽에서 사용
        private Grid MyGrid = null;                 //캔버스로 받아오니 이벤트가 잘작동안돼서 그냥 그리드로 받아옴.
        /// <summary>
        /// 마우스 클릭 변수를 비활성화시킴
        /// 추후 Left Right클릭 나눌예정
        /// 23.11.11
        /// </summary>
        private DelegateCommand<object> commandmouseup;
        public DelegateCommand<object> CommandMouseUp =>
            commandmouseup ?? (commandmouseup = new DelegateCommand<object>(ExecuteCommandMouseUp));

        void ExecuteCommandMouseUp(object obj)
        {
            if (MyGrid is null)
                return;
            CandleMouseState = false;

        }

        /// <summary>
        /// 캔들 움직일때 사용하기위한 첫 클릭지점 저장 
        /// 추후 Left Right클릭 나눌예정
        /// 23.11.11
        /// </summary>
        private DelegateCommand<MouseEventArgs> commandmousedown;
        public DelegateCommand<MouseEventArgs> CommandMouseDown =>
            commandmousedown ?? (commandmousedown = new DelegateCommand<MouseEventArgs>(ExecuteCommandMouseDown));

        void ExecuteCommandMouseDown(MouseEventArgs e)
        {
            if (MyGrid is null)
                return;
            CandleMouseState = true;
            FirstXPos = e.GetPosition(MyGrid).X;
        }


        /// <summary>
        /// 그리드에서  마우스가 움직였을때 차트를 갱신시켜주기위한 이벤트
        /// 23.11.14
        /// </summary>
        private DelegateCommand<MouseEventArgs> commandmousemove;
        public DelegateCommand<MouseEventArgs> CommandMouseMove =>
            commandmousemove ?? (commandmousemove = new DelegateCommand<MouseEventArgs>(ExecuteCommandMouseMove));

        double move;
        void ExecuteCommandMouseMove(MouseEventArgs e)
        {
            if (MyGrid != null)
            {
                move = e.GetPosition(MyGrid).X;
                if (Market != "")
                {
                    GetCandleInfo(move);
                    double price = MaxPrice - ((MaxPrice - MinPrice) * (e.GetPosition(MyGrid).Y / MyGrid.ActualHeight));
                    PrintChartYAxis[(int)YAxis.Cursor].CursorValue = price.ToString(FLen);
                    PrintChartYAxis[(int)YAxis.Cursor].Y = e.GetPosition(MyGrid).Y + GapYAxis;
                    if (CandleMouseState)
                    {
                        if (PrintTick.Count <= 0)
                            return;
                        ShowChart();
                    }

                }

            }

        }

        /// <summary>
        /// 차트 확대 축소기능을 사용하기위한 이벤트
        /// 23.11.14 -완
        /// </summary>
        private DelegateCommand<MouseWheelEventArgs> commandmousewheel;
        public DelegateCommand<MouseWheelEventArgs> CommandMouseWheel =>
            commandmousewheel ?? (commandmousewheel = new DelegateCommand<MouseWheelEventArgs>(ExecuteCommandMouseWheel));

        void ExecuteCommandMouseWheel(MouseWheelEventArgs e)
        {
            //선택된 마켓이없을경우 실행하지않음.
            if (Market == "")
                return;
            if (PrintTick.Count <= 0)
                return;

            int AddTicks = 10;      //스크롤 한번에 10개씩 늘리고 줄임.

            int Keep = PrintCandle; //현재 출력하는 캔들수를 저장해둠
            int sign = 1;
            int PrePrintCursor = PrintCursor;
            bool Type = false;
            int PreCnt = 0;//앞에 삭제 및 추가값
            int NextCnt = 0;//뒤에 삭제 및 추가값

            try
            {
                if (e.Delta > 0)
                {
                    //확대
                    if (PrintCandle > PrintMinCandle)
                    {
                        PrintCandle = Math.Max(PrintCandle - AddTicks, PrintMinCandle);
                        sign *= -1;
                        Type = false;

                    }
                    else
                        return;//최대 확대상태여서 더이상 확대불가
                }
                else if (e.Delta < 0)
                {
                    //축소
                    if (PrintCandle < PrintMaxCandle)
                    {
                        PrintCandle = Math.Min(PrintCandle + AddTicks, PrintMaxCandle);
                        Type = true;
                    }
                    else
                        return;//최소 축소상태여서 더이상 축소불가
                }

                if (Keep != PrintCandle)
                {
                    TickWidth = Math.Max(MyGrid.ActualWidth / PrintCandle, 1.6) - CandleMargin;
                    double rate = move / MyGrid.ActualWidth;

                    PreCnt = Convert.ToInt32(AddTicks * rate * (sign * -1));    //N
                    NextCnt = Math.Abs(AddTicks - PreCnt);                      //10 - N
                    int ActivePreCnt = 0;


                    if (Type == true)
                    {
                        if (PreCnt != 0)
                        {
                            for (int i = 0; i < PreCnt; i++)
                            {
                                if (PrintCursor - (i - 1) < 0)
                                {
                                    ActivePreCnt++;
                                }
                            }
                            if (ActivePreCnt != 0)
                            {
                                for (int i = 0; i < PrintTick.Count; i++)
                                {
                                    PrintTick[i].NextNumber(ActivePreCnt);
                                }

                                for (int i = 0; i < ActivePreCnt; i++)
                                {
                                    if (PrintTick.Count < PrintCandle)
                                    {
                                        Structs.Tick tick = GetTick(PrintCursor - (ActivePreCnt - (i + 1)), ActivePreCnt - (i + 1));
                                        PrintTick.Insert(0, tick);
                                    }

                                }

                                PrintCursor -= ActivePreCnt;
                                NextCnt = AddTicks - ActivePreCnt;


                            }
                        }

                        if (NextCnt != 0)
                        {
                            for (int i = 0; i < NextCnt; i++)
                            {
                                int index = (PrintCursor + PrintTick.Count);
                                if (index < TickData.Count && PrintTick.Count < PrintCandle)
                                {
                                    Structs.Tick tick = GetTick(index, PrintTick.Count);
                                    PrintTick.Add(tick);
                                }
                            }
                        }

                    }
                    else
                    {

                        if (PreCnt != 0)
                        {
                            //앞에 데이터 삭제시킴
                            for (int i = 0; i < PreCnt; i++)
                            {
                                if (PrintTick.Count > LeaveCnt)
                                {
                                    PrintTick.RemoveAt(0);
                                    ActivePreCnt++;
                                }
                            }

                            if (ActivePreCnt != 0)
                            {
                                //삭제시킨 데이터만큼 땡겨옴
                                for (int i = 0; i < PrintTick.Count; i++)
                                {
                                    PrintTick[i].PreNumber(ActivePreCnt);
                                }
                            }


                            //TickData에서가져오는 커서값을 변경함.
                            //이후부턴 변경된 기준으로 가져올예정
                            PrintCursor += ActivePreCnt;
                            NextCnt = AddTicks - ActivePreCnt;
                        }
                        if (NextCnt != 0)
                        {
                            for (int i = 0; i < NextCnt; i++)
                            {
                                if (PrintTick.Count >= PrintCandle && PrintTick.Count > LeaveCnt)
                                {
                                    PrintTick.RemoveAt(PrintTick.Count - 1);
                                }
                            }
                        }
                    }


                    CandleMaxMin();
                    GetCandleTime();
                    GetCoinPrice();
                    KeepPrintCursor = PrintCursor;
                }
            }
            catch (Exception ex) 
            {
                Debug.WriteLine("MouseWheelEvent");
            }
            

        }

        /// <summary>
        /// 그리드에서 마우스가 떠났을때 이벤트
        /// 23.11.11
        /// </summary>
        private DelegateCommand<object> commandmouseleave;
        public DelegateCommand<object> CommandMouseLeave =>
            commandmouseleave ?? (commandmouseleave = new DelegateCommand<object>(ExecuteCommandMouseLeave));

        void ExecuteCommandMouseLeave(object parameter)
        {
            CandleMouseState = false;
        }


        /// <summary>
        /// 캔들 불러오는 시간 타입을 변경시켜줌.
        /// 23.11.15 
        /// </summary>
        private DelegateCommand<string> commandselectedticktype;
        public DelegateCommand<string> CommandSelectedTickType =>
            commandselectedticktype ?? (commandselectedticktype = new DelegateCommand<string>(ExecuteCommandSelectedTickType));

        void ExecuteCommandSelectedTickType(string type)
        {
            TickType = type;

            if (type == "days")
            {
                VisibleComboBoxMin = Visibility.Collapsed;
                MinTickTime = 1440;
            }
            else if (type == "weeks")
            {
                VisibleComboBoxMin = Visibility.Collapsed;
                MinTickTime = 1440 * 7;
            }
            else if (type == "months")
            {
                VisibleComboBoxMin = Visibility.Collapsed;
                MinTickTime = 1440 * 30;
            }
            else
            {
                //23.11.15 - 해당부분 수정 후 완료로 변경
                //후에 TickTime값을 설정해주는작업을 완료했을때 1말고 설정된 값으로 변경시켜줘야함
                VisibleComboBoxMin = Visibility.Visible;
                MinTickTime = PreMinNumber;
            }
            CreateChart();

        }

        private DelegateCommand<object> commandselectedmin;
        public DelegateCommand<object> CommandSelectedMin =>
            commandselectedmin ?? (commandselectedmin = new DelegateCommand<object>(ExecuteCommandSelectedMin));

        void ExecuteCommandSelectedMin(object parameter)
        {
            if (parameter is ComboBox)
            {
                ComboBox cb = (ComboBox)parameter;
                PreMinNumber = (int)cb.SelectedItem;
                MinTickTime = PreMinNumber;
                CreateChart();
            }
        }
        #endregion

        #region Functions

        /// <summary>
        /// 해당 코인이 보유중일경우 차트에 출력.
        /// 23.12.08
        /// </summary>
        private void GetCoinPrice()
        {
            if (Market == "")
                return;

            if (PrintChartYAxis[(int)YAxis.Avg].Y <= 0)
            {
                UpbitFunctions.Accounts FAccount = new UpbitFunctions.Accounts();
                double price = FAccount.GetAccountCoinPrice(Market);
                PrintChartYAxis[(int)YAxis.Avg].CursorValue = price.ToString();
            }
            
            for(int i = (int)YAxis.Avg; i < PrintChartYAxis.Count; i++)
            {
                if (PrintChartYAxis[i].Type == "bid" || PrintChartYAxis[i].Type == "ask" || PrintChartYAxis[i].Type == "AvgPrice")
                {

                    double value = Convert.ToDouble(PrintChartYAxis[i].CursorValue);
                    if (value >= 0)
                    {
                        double yvalue = (GridHeight * ((MaxPrice - value) / (MaxPrice - MinPrice))) + GapYAxis;

                        //차트 범위 초과했을경우 안보이게 수정
                        if (GridHeight <= yvalue || 0 >= yvalue)
                            yvalue = -100;

                        PrintChartYAxis[i].Y = yvalue;
                    }
                    else
                    {
                        PrintChartYAxis[i].Y = -100;
                    }

                }
            }
        }
        private void GetCandleTime()
        {
            if (PrintTick is null && Market == "")
                return;
            PrintChartXAxis = new ObservableCollection<ChartSideCursor>();
            double TickWidth = (this.TickWidth + CandleMargin);
            for (int i = PrintCursor + (PrintTick.Count - 1), j = PrintCandle + 1; i >= PrintCursor; i -= 50, j -= 50)
            {
                ChartSideCursor chart = new ChartSideCursor();
                chart.X = (j - (PrintCandle - PrintTick.Count)) * TickWidth - (TickWidth / 2);
                chart.Y = 0;
                chart.Type = "Time";
                chart.CursorValue = TickData[i].kst;
                chart.BackColor = Brushes.Gray;
                chart.Color = Brushes.White;

                PrintChartXAxis.Add(chart);
            }

        }
        /// <summary>
        /// 해당 Index에 캔들값을 가져옴. 현재 포커스 캔들 데이터출력을위해 사용
        /// 23.11.15 - 완
        /// </summary>
        /// <param name="X"></param>
        private void GetCandleInfo(double X)
        {
            double value = (MyGrid.ActualWidth - Math.Max((MyGrid.ActualWidth - X), 0.1)) / (TickWidth + CandleMargin);
            int index = PrintCursor + Convert.ToInt32(Math.Floor(value));
            Structs.Tick tick = GetTick(index);
            if (tick is not null)
            {
                string len = tick.Volume.ToString().Split(".")[0];
                string lenvolume = len.Length > 1 ? (len.Length > 2 ? "F0" : "F4") : "F8";
                PrintVolume = tick.Volume.ToString(lenvolume);


                len = tick.OP.ToString().Split(".")[0];
                FLen = len.Length > 1 ? (len.Length > 2 ? "F0" : "F4") : "F8";
                PrintOp = tick.OP.ToString(FLen);
                PrintHp = tick.HP.ToString(FLen);
                PrintLp = tick.LP.ToString(FLen);
                PrintTp = tick.TP.ToString(FLen);
                PrintTime = Convert.ToDateTime(tick.KST).ToString("yy.MM.dd HH:mm");

                
                //PrintRate
            }

        }
        /// <summary>
        /// TickData에서 PrintTick으로 데이터를 뽑아올때 사용. 구조체 Candle -> Tick으로 변경이여서 이렇게 사용.
        /// 23.11.14 -완
        /// </summary>
        /// <param name="i">받아올 TickData Index Value</param>
        /// <param name="x">PrintTick X axis</param>
        /// <returns></returns>
        private Structs.Tick GetTick(int i = 0, int x = 0)
        {
            if (i >= TickData.Count)
                return null;
            Structs.Tick tick = new Structs.Tick();
            tick.X = x;
            tick.Y = 0;
            tick.HP = TickData[i].hp;
            tick.LP = TickData[i].lp;
            tick.TP = TickData[i].tp;
            tick.OP = TickData[i].op;
            tick.UTC = TickData[i].utc;
            tick.KST = TickData[i].kst;
            tick.Timestamp = TickData[i].timestamp;
            tick.Volume = TickData[i].candle_acc_trade_volume;
            tick.CandleStartPoint = TickData[i].hp;
            tick.Colors = SetCandleColor(TickData[i].op, TickData[i].tp);

            return tick;

        }

        /// <summary>
        /// ShowChart함수 최소한 기능 첫 로드 차트만 생성해줌
        /// 23.11.11 -완
        /// </summary>
        private void CreateChart()
        {
            PrintTick = new ObservableCollection<Structs.Tick>();

            if (Market != "")
            {
                //초기 차트 생성
                TickData = pfnc.Candles(TickType, MinTickTime, this.Market, "", 200);
                TickData.Reverse();

                int cnt = Math.Min(PrintCursor + PrintCandle, TickData.Count);

                for (int i = PrintCursor, j = 0; i < cnt; i++, j++)
                {

                    Structs.Tick tick = GetTick(i, j);
                    PrintTick.Add(tick);
                }
                CandleMaxMin();
                GetCandleTime();
                GetCoinPrice();
            }

        }

        /// <summary>
        /// 차트를 갱신시켜줌.
        /// 23.11.14 -완
        /// </summary>
        private void ShowChart()
        {
            int newLeft = (int)((FirstXPos - move) / 10);
            if (newLeft == 0)
                return;



            int AddCnt = 0;
            bool MoveType = false;



            FirstXPos = move;

            //이전 데이터를 받아옴.
            if ((PrintCursor + newLeft) < 0)
            {
                //갱신
                try
                {
                    DateTime FirstTime = Convert.ToDateTime(TickData[0].utc);
                    List<Candle> item = new List<Candle>();
                    item = pfnc.Candles(TickType, MinTickTime, this.Market, FirstTime.ToString("yyyy-MM-dd'T'HH:mm:ss"), 200);
                    for (int i = 0; i < item.Count; i++)
                    {
                        TickData.Insert(0, item[i]);
                    }

                    //더 갱신한 코인데이터 개수만큼 더해줌
                    KeepPrintCursor += item.Count;
                    PrintCursor += item.Count;
                }
                catch(Exception ex) 
                {
                    Debug.WriteLine($"CharViewShowChart : {ex.ToString()}");
                }
                
            }



            if (newLeft > 0)
            {
                PrintCursor = Math.Min(PrintCursor + newLeft, TickData.Count - LeaveCnt);
                MoveType = true;
            }
            else if (newLeft < 0)
            {
                PrintCursor = Math.Max(PrintCursor + newLeft, 0);
                MoveType = false;
            }
            AddCnt = Math.Abs(PrintCursor - KeepPrintCursor);
            KeepPrintCursor = PrintCursor;

            for (int i = 0; i < AddCnt; i++)
            {
                if (MoveType == true && PrintTick.Count > 1)
                    PrintTick.RemoveAt(0);

                if (MoveType == false && PrintCandle - AddCnt < PrintTick.Count)
                    PrintTick.RemoveAt(PrintTick.Count - 1);
            }

            //기존데이터 X축 땡겨주고 밀어줌
            if (AddCnt != 0)
            {
                foreach (Structs.Tick tick in PrintTick)
                {
                    if (MoveType == true)
                        tick.PreNumber(Math.Abs(AddCnt));
                    else
                        tick.NextNumber(Math.Abs(AddCnt));
                }
            }

            //데이터 등록
            for (int i = 0; i < AddCnt; i++)
            {
                //정해진 개수를 넘어가면 추가하지않음.
                int index = PrintCursor + PrintTick.Count + i;
                if (MoveType && TickData.Count <= index)
                {
                    break;
                }

                Candle c;
                Structs.Tick tick = new Structs.Tick();
                if (MoveType == true)
                {
                    c = TickData[PrintCursor + PrintTick.Count];
                    tick.X = PrintTick.Count;
                }
                else
                {
                    c = TickData[PrintCursor + (AddCnt - (i + 1))];
                    tick.X = AddCnt - (i + 1);
                }
                tick.Y = 0;
                tick.HP = c.hp;
                tick.LP = c.lp;
                tick.TP = c.tp;
                tick.OP = c.op;
                tick.UTC = c.utc;
                tick.KST = c.kst;
                tick.Timestamp = c.timestamp;
                tick.Volume = c.candle_acc_trade_volume;
                tick.CandleStartPoint = tick.HP;
                tick.Colors = SetCandleColor(c.op, c.tp);

                if (MoveType == true)
                {
                    PrintTick.Add(tick);
                }
                else
                {
                    PrintTick.Insert(0, tick);
                }
            }

            CandleMaxMin();
            GetCandleTime();
            GetCoinPrice();
        }

        //데이터 병렬처리 테스트해봤음 음... 그냥 마우스 드래그할떄 제약을 좀 주는게 나을듯.
        private async Task SizeUpdateDataAsync(int start)
        {
            await Task.Run(() => 
            {
                Debug.WriteLine(start.ToString());
                for(int i = start; i < start + 50; i++)
                {
                    if (PrintTick.Count <= i)
                        break;

                    if (MinPrice > PrintTick[i].LP)
                        MinPrice = PrintTick[i].LP;
                    if (MaxPrice < PrintTick[i].HP)
                        MaxPrice = PrintTick[i].HP;
                    if (MaxVolume < PrintTick[i].Volume)
                        MaxVolume = PrintTick[i].Volume;

                }
            });
        }
        /// <summary>
        /// 캔들에 등록된 데이터를 가져와 Max갑과 Min값을 구해줌.
        /// 23.11.14 -완
        /// </summary>
        private async void CandleMaxMin()
        {
            double keepMaxPrice = 0;
            double keepMinPrice = 999999999;
            double keepMaxVolume = 0;
            

            if (PrintTick.Count <= 0)
                return;
            
            foreach (Structs.Tick tick in PrintTick)
            {
                if (keepMinPrice > tick.LP)
                    keepMinPrice = tick.LP;
                if (keepMaxPrice < tick.HP)
                    keepMaxPrice = tick.HP;
                if (keepMaxVolume < tick.Volume)
                    keepMaxVolume = tick.Volume;
            }

            /*
            List<Task> tasks = new List<Task>();
            for (int i = 0; i< PrintTick.Count;i += 50)
            {
                tasks.Add(SizeUpdateDataAsync(i));
            }
            await Task.WhenAll(tasks);

            Debug.WriteLine("과연");
            */

            MaxPrice = keepMaxPrice;
            MinPrice = keepMinPrice;
            MaxVolume = keepMaxVolume;

            string len = MaxPrice.ToString().Split(".")[0];
            FLen = len.Length > 1 ? (len.Length > 2 ? "F0" : "F4") : "F8";

            PrintChartYAxis[(int)YAxis.Max].CursorValue = MaxPrice.ToString(FLen);
            PrintChartYAxis[(int)YAxis.Min].CursorValue = MinPrice.ToString(FLen);
            ChartTickHeight = GridHeight / (MaxPrice - MinPrice);
            VolumeTickHeight = GridVolumeHeight / MaxVolume;

            PrintChartYAxis[(int)YAxis.Now].CursorValue = PrintTick[PrintTick.Count - 1].TP.ToString();
            PrintChartYAxis[(int)YAxis.Now].Y = (GridHeight * ((MaxPrice - PrintTick[PrintTick.Count - 1].TP) / (MaxPrice - MinPrice))) + GapYAxis;
        }

        /// <summary>
        /// 캔버스에 라인을 그려줌.
        /// 23.11.11
        /// </summary>
        /// <param name="gap">줄 간격</param>
        /// <param name="type">가로 / 세로</param>
        /// <returns></returns>
        private List<line> DrawLine(double gap, bool type)
        {
            List<line> lines = new List<line>();

            for (int i = 0; i < this.lines; i++)
            {
                line add = new line();
                if (type)
                {
                    //가로
                    add.FromX = 0;
                    add.FromY = i * gap;
                    add.ToY = i * gap;
                }
                else
                {
                    //세로
                    add.FromX = i * gap;
                    add.ToX = i * gap;
                    add.FromY = 0;
                }
                if (i % 10 == 0)
                {
                    add.LineThickness = 0.6;
                    add.LineColor = Brushes.Transparent;//Brushes.Black;
                }
                else
                {
                    add.LineThickness = 0.3;
                    add.LineColor = Brushes.Transparent;//Brushes.Gray;
                }
                lines.Add(add);
            }

            return lines;
        }

        /// <summary>
        /// 외부에서 마켓을 선택했을경우 호출되는 함수 
        /// 23.11.14
        /// </summary>
        /// <param name="market"></param>
        private void GetMarket(Structs.MarketCodes market)
        {
            Market = market.Market;

            //기본값 셋팅
            SetCtor();

            //초기 차트 생성
            CreateChart();
        }

        private void GetAccountMarket(Structs.Accounts market)
        {
            Market = market.Market;

            //기본값 셋팅
            SetCtor();

            //초기 차트 생성
            CreateChart();

            GetOrderList(true);
        }

        /// <summary>
        /// 초기값 설정함수
        /// 23.11.14
        /// </summary>
        private void SetCtor()
        {
            PrintChartYAxis = new ObservableCollection<ChartSideCursor>();
            ChartSideCursor max = new ChartSideCursor();
            max.X = 0;
            max.Y = GapYAxis;
            max.Type = "MaxPrice";
            max.CursorValue = "";
            max.BackColor = MyColors.ColorChartMax;
            max.Color = MyColors.ColorChartMaxText;

            ChartSideCursor min = new ChartSideCursor();
            min.X = 0;
            min.Y = MyGrid.ActualHeight + GapYAxis;
            min.Type = "MinPrice";
            min.CursorValue = "";
            min.BackColor = MyColors.ColorChartMin;
            min.Color = MyColors.ColorChartMinText;

            ChartSideCursor cursor = new ChartSideCursor();
            cursor.X = 0;
            cursor.Y = -100;
            cursor.Type = "Cursor";
            cursor.CursorValue = "";
            cursor.BackColor = MyColors.ColorChartCursor;
            cursor.Color = MyColors.ColorChartCursorText;

            ChartSideCursor nowprice = new ChartSideCursor();
            nowprice.X = 0;
            nowprice.Y = -100;
            nowprice.Type = "NowPrice";
            nowprice.CursorValue = "";
            nowprice.BackColor = MyColors.ColorChartNow;
            nowprice.Color = MyColors.ColorChartNowText;

            ChartSideCursor avgprice = new ChartSideCursor();
            avgprice.X = 0;
            avgprice.Y = -100;
            avgprice.Type = "AvgPrice";
            avgprice.CursorValue = "-100";
            avgprice.BackColor = MyColors.ColorChartAvg;
            avgprice.Color = MyColors.ColorChartAvgText;

            PrintChartYAxis.Add(max);
            PrintChartYAxis.Add(min);
            PrintChartYAxis.Add(cursor);
            PrintChartYAxis.Add(nowprice);
            PrintChartYAxis.Add(avgprice);

            MaxVolume = 0;
            KeepPrintCursor = 0;
            PrintCandle = PrintDefaultCandle;
            TickWidth = Math.Max(MyGrid.ActualWidth / PrintCandle, 1.6) - CandleMargin;
            PrintCursor = 0;
            MaxPrice = 0;
            MinPrice = 9999999999;
        }

        /// <summary>
        /// 매수 매도 걸어놓은거 확인
        /// </summary>
        /// <param name="b"></param>

        private void GetOrderList() { GetOrderList(true); }
        private void GetOrderList(bool b)
        {
            if(Market != "")
            {
                Order Orderfnc = new Order();
                List<OrderList> list = Task.Run(() => Orderfnc.OrderListAsync(Market, "wait")).Result;
                int cnt = PrintChartYAxis.Count;
                if (cnt >= 5)
                {
                    for (int i = 5; i < cnt; i++)
                    {
                        PrintChartYAxis.RemoveAt(5);
                    }
                }
                foreach(var item in list)
                {
                    ChartSideCursor i = new ChartSideCursor();
                    i.X = 0;
                    i.Type = item.side;
                    i.CursorValue = item.price;
                    i.BackColor = item.side == "bid" ?  MyColors.ColorChartOrderBid : MyColors.ColorChartOrderAsk;
                    i.Color = item.side == "bid" ? MyColors.ColorChartOrderBidText : MyColors.ColorChartOrderAskText;

                    double value = Convert.ToDouble(i.CursorValue);
                    if (value >= 0)
                    {
                        double yvalue = (GridHeight * ((MaxPrice - value) / (MaxPrice - MinPrice))) + GapYAxis;

                        //차트 범위 초과했을경우 안보이게 수정
                        if (GridHeight <= yvalue || 0 >= yvalue)
                            yvalue = -100;

                        i.Y = yvalue;
                    }
                    else
                    {
                        i.Y = -100;
                    }

                    PrintChartYAxis.Add(i);
                }
            }
            
        }

        private Brush SetCandleColor(double OP, double TP)
        {
            if (OP > TP)
                return MyColors.ColorAsk;
            else if (OP < TP)
                return MyColors.ColorBid;
            else
                return MyColors.ColorBid;
        }
        public void TradeEvent(Trade tr)
        {
            throw new NotImplementedException();
        }

        public void OrderEvent(Orderbook ob)
        {
        }

        /// <summary>
        /// 실시간 데이터를 웹소켓에서 받아와서 차트에 갱신시켜줌.
        /// 23.11.14
        /// </summary>
        /// <param name="tick"></param>
        public void TickerEvent(Ticker tick)
        {
            if (Market == tick.cd)
            {
                if (TickData is not null && TickData.Count > 0)
                {
                    int cnt = TickData.Count - 1;
                    DateTime StartTime = Convert.ToDateTime(TickData[cnt].kst);
                    StartTime = StartTime.AddSeconds(StartTime.Second * -1);
                    DateTime EndTime = StartTime.AddMinutes(MinTickTime).AddSeconds(-1);
                    DateTime dtTradeTime = FncDateTime.LongToDateTime(tick.ttms);         //한국시간
                    UInt32 TradeTime = FncDateTime.DateTimeToInt(dtTradeTime);
                    try
                    {
                        //실시간 데이터 갱신
                        if (FncDateTime.DateTimeToInt(StartTime) <= TradeTime && TradeTime <= FncDateTime.DateTimeToInt(EndTime))
                        {
                            Candle candle = TickData[cnt];
                            candle.timestamp = tick.ttms;
                            candle.tp = tick.tp;
                            candle.candle_acc_trade_price = tick.atp;
                            candle.candle_acc_trade_volume += tick.tv;
                            //candle.kst = FncDateTime.LongToDateTime(tick.ttms).ToString();

                            //고가 갱신
                            if (candle.tp > candle.hp)
                                candle.hp = candle.tp;

                            //저가 갱신
                            if (candle.lp > candle.tp)
                                candle.lp = candle.tp;

                            //현재 갱신중인 틱을 보여줌.
                            if ((TickData.Count - PrintCursor) < PrintCandle)
                            {
                                Tick _tick = PrintTick[PrintTick.Count - 1];
                                _tick.TP = tick.tp;

                                //해당 기준 시간을 변경시 틱데이터를 불러올때 데이터가 불러와지지 않는 버그 발생 Timestamp를 변경해야할듯
                                //_tick.KST = candle.kst;
                                _tick.Timestamp = tick.ttms;

                                _tick.Colors = SetCandleColor(_tick.OP, _tick.TP);

                                if (_tick.TP < _tick.LP)
                                    _tick.LP = _tick.TP;
                                if (_tick.TP > _tick.HP)
                                    _tick.HP = _tick.TP;

                                _tick.Tail = 0;
                                _tick.Candle = 0;
                                _tick.CandleStartPoint = _tick.HP;
                                _tick.Volume += tick.tv;

                                if (MinPrice > _tick.LP)
                                    MinPrice = _tick.LP;
                                if (MaxPrice < _tick.HP)
                                    MaxPrice = _tick.HP;
                                if (MaxVolume < _tick.Volume)
                                    MaxVolume = _tick.Volume;
                                VolumeTickHeight = GridVolumeHeight / MaxVolume;
                                ChartTickHeight = GridHeight / (MaxPrice - MinPrice);

                                PrintChartYAxis[(int)YAxis.Max].CursorValue = MaxPrice.ToString(FLen);
                                PrintChartYAxis[(int)YAxis.Min].CursorValue = MinPrice.ToString(FLen);

                                //현재가 표시해줌
                                PrintChartYAxis[(int)YAxis.Now].CursorValue = _tick.TP.ToString();
                                PrintChartYAxis[(int)YAxis.Now].Y = (GridHeight * ((MaxPrice - _tick.TP) / (MaxPrice - MinPrice))) + GapYAxis;

                            }

                            TickData[cnt] = candle;
                        }
                        else
                        {
                            EndTime = StartTime.AddMinutes(MinTickTime);
                            Candle precandle = TickData[cnt];
                            Candle candle = new Candle();
                            candle.timestamp = tick.ttms;
                            candle.tp = tick.tp;
                            candle.op = precandle.tp;
                            candle.hp = tick.tp;
                            candle.lp = tick.tp;
                            candle.kst = EndTime.ToString();
                            candle.candle_acc_trade_volume = tick.tv;
                            TickData.Add(candle);


                            //실시간 데이터 생성
                            if ((TickData.Count - PrintCursor) < PrintCandle)
                            {
                                Tick _tick = new Tick();
                                _tick.TP = tick.tp;
                                _tick.OP = precandle.tp;
                                _tick.HP = tick.tp;
                                _tick.LP = tick.tp;
                                _tick.KST = EndTime.ToString();
                                _tick.Volume = tick.tv;
                                if (MinPrice > _tick.LP)
                                    MinPrice = _tick.LP;
                                if (MaxPrice < _tick.HP)
                                    MaxPrice = _tick.HP;
                                if (MaxVolume < _tick.Volume)
                                    MaxVolume = _tick.Volume;
                                VolumeTickHeight = GridVolumeHeight / MaxVolume;
                                ChartTickHeight = GridHeight / (MaxPrice - MinPrice);

                                _tick.Tail = 0;
                                _tick.Candle = 0;
                                _tick.CandleStartPoint = _tick.HP;
                                _tick.X = PrintTick[PrintTick.Count - 1].X + 1;


                                _tick.Colors = SetCandleColor(_tick.OP, _tick.TP);

                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    PrintTick.Add(_tick);
                                    GetCandleTime();
                                });


                            }


                        }



                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("ChartView TickEvent");
                        //MessageBox.Show("aaaaaa");
                    }
                }
            }
        }

        

        #endregion Functions




        public ChartViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _ea.GetEvent<MessageCoinNameEvent>().Subscribe(GetMarket);          //코인목록에서 불러왔을때
            _ea.GetEvent<MessageAccountEvent>().Subscribe(GetAccountMarket);   //잔고에서 불러왔을때

            _ea.GetEvent<MessageCoinOrderEvent>().Subscribe(GetOrderList);      //주문들어왔을때
            _ea.GetEvent<MessageCoinOrderCancle>().Subscribe(GetOrderList);     //주문 캔슬했을때
            _ea.GetEvent<MessageCoinOrderChangeEvent>().Subscribe(GetOrderList);     //주문 캔슬했을때
            

            TickMargin = new Thickness { Left = 1, Right = 1 };

            SetLanguage();

            //색변경 이벤트 등록
            PublicColor.Colors.ColorUpdate += (EventUpdateColor);

            //틱 종류 저장
            ListTickType = new List<string>();
            ListTickType.Add("minutes"); ListTickType.Add("days");
            ListTickType.Add("weeks"); ListTickType.Add("months");
            TickType = ListTickType[0];


            ListTickMin = new List<int>();
            ListTickMin.Add(1); ListTickMin.Add(3);
            ListTickMin.Add(5); ListTickMin.Add(15);
            ListTickMin.Add(10); ListTickMin.Add(30);
            ListTickMin.Add(60); ListTickMin.Add(240);
            MinTickTime = ListTickMin[0];

            //언어 변경시 이벤트 등록
            Language.Language.LangChangeEvent += (SetLanguage);

            //WebSocketTicker.OrderEvent += new WebSocketTicker.OrderEventHandler(OrderEvent);
            WebSocketTicker.TickerEvent += new WebSocketTicker.TickEventHandler(TickerEvent);
        }
    }
}
