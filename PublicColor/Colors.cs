using FileIO;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Media;

namespace PublicColor
{
    public class Colors : BindableBase
    {
        private static Colors colorinstance;
        public static Colors Colorinstance
        {
            get
            {
                if (colorinstance == null) colorinstance = new Colors();

                return colorinstance;
            }
        }

        #region Models
        //해당 색상을 베이스로 약간 변경 할 예정
        private Brush colorbid;                 //매수 기본색
        public Brush ColorBid
        {
            get { return colorbid; }
            set { SetProperty(ref colorbid, value);}
        }
        private Brush colorbidback;             //매수 기본 배경색
        public Brush ColorBidBack
        {
            get { return colorbidback; }
            set { SetProperty(ref colorbidback, value); }
        }

        private Brush colorask;                 //매도 기본색
        public Brush ColorAsk
        {
            get { return colorask; }
            set { SetProperty(ref colorask, value); }
        }
        private Brush coloraskback;             //매도 기본 배경색
        public Brush ColorAskBack
        {
            get { return coloraskback; }
            set { SetProperty(ref coloraskback, value); }
        }


        private Brush colortext;                //기본 텍스트색
        public Brush ColorText
        {
            get { return colortext; }
            set { SetProperty(ref colortext, value); }
        }

        public Brush colorback;                //기본 배경색
        public Brush ColorBack
        {
            get { return colorback; }
            set { SetProperty(ref colorback, value); }
        }

        private Brush colorborder;              //기본 배경색에 대한 테두리 색
        public Brush ColorBorder
        {
            get { return colorborder; }
            set { SetProperty(ref colorborder, value); }
        }


        //컬러색상이 지정 안됐을때
        private Brush colornonecolor;
        public Brush ColorNoneColor
        {
            get { return colornonecolor; }
            set { SetProperty(ref colornonecolor, value); }
        }




        private Brush colorchartorderbidtext;
        public Brush ColorChartOrderBidText
        {
            get { return colorchartorderbidtext; }
            set { SetProperty(ref colorchartorderbidtext, value); }
        }
        private Brush colorchartorderasktext;
        public Brush ColorChartOrderAskText
        {
            get { return colorchartorderasktext; }
            set { SetProperty(ref colorchartorderasktext, value); }
        }

        private Brush colorchartmaxtext;
        public Brush ColorChartMaxText
        {
            get { return colorchartmaxtext; }
            set { SetProperty(ref colorchartmaxtext, value); }
        }
        private Brush colorchartmintext;
        public Brush ColorChartMinText
        {
            get { return colorchartmintext; }
            set { SetProperty(ref colorchartmintext, value); }
        }
        private Brush colorchartcursortext;
        public Brush ColorChartCursorText
        {
            get { return colorchartcursortext; }
            set { SetProperty(ref colorchartcursortext, value); }
        }
        private Brush colorchartnowtext;
        public Brush ColorChartNowText
        {
            get { return colorchartnowtext; }
            set { SetProperty(ref colorchartnowtext, value); }
        }

        private Brush colorchartavgtext;
        public Brush ColorChartAvgText
        {
            get { return colorchartavgtext; }
            set { SetProperty(ref colorchartavgtext, value); }
        }


        private Brush colorchartorderbid;
        public Brush ColorChartOrderBid
        {
            get { return colorchartorderbid; }
            set { SetProperty(ref colorchartorderbid, value); }
        }
        private Brush colorchartorderask;
        public Brush ColorChartOrderAsk
        {
            get { return colorchartorderask; }
            set { SetProperty(ref colorchartorderask, value); }
        }

        private Brush colorchartmax;
        public Brush ColorChartMax
        {
            get { return colorchartmax; }
            set { SetProperty(ref colorchartmax, value); }
        }
        private Brush colorchartmin;
        public Brush ColorChartMin
        {
            get { return colorchartmin; }
            set { SetProperty(ref colorchartmin, value); }
        }
        private Brush colorchartcursor;
        public Brush ColorChartCursor
        {
            get { return colorchartcursor; }
            set { SetProperty(ref colorchartcursor, value); }
        }
        private Brush colorchartnow;
        public Brush ColorChartNow
        {
            get { return colorchartnow; }
            set { SetProperty(ref colorchartnow, value); }
        }

        private Brush colorchartavg;
        public Brush ColorChartAvg
        {
            get { return colorchartavg; }
            set { SetProperty(ref colorchartavg, value); }
        }

        //기본 색상 추후 커스텀 색상 파일로 저장 후 로드
        private Dictionary<string, string> dircolors;
        public Dictionary<string, string> DirColors
        {
            get { return dircolors; }
            set { SetProperty(ref dircolors, value); }
        }
        private string nonecolor = "FFFF0000";

        //파일 관련 변수
        private FncFileIO fileIO = new FncFileIO();
        private string FilePath = "Setting";
        private string FileName = "Color.log";

        #endregion Models


        #region Event
        public delegate void EventHandler();
        public static event EventHandler ColorUpdate;

        #endregion Event


        #region Functions
        private void DefaultColors()
        {
            DirColors = new Dictionary<string, string>();
            //기본 텍스트색상 배경색상
            DirColors.Add("text", "FF333333");
            DirColors.Add("back", "FFFFFFFF");

            //차트 색상
            DirColors.Add("bid", "FFDD2323");
            DirColors.Add("bidback", "66DD2323");
            DirColors.Add("ask", "FF244993");
            DirColors.Add("askback", "66244993");
            DirColors.Add("order", "FF333333");
            DirColors.Add("orderback", "FFFFFFFF");

            DirColors.Add("chartmax", "FFDC143C");
            DirColors.Add("chartmin", "FF4169E1");
            DirColors.Add("chartorderbid", "FFDD2323");
            DirColors.Add("chartorderask", "66DD2323");
            DirColors.Add("chartcursor", "FF008B8B");
            DirColors.Add("chartnow", "FFB8860B");
            DirColors.Add("chartavg", "FF228B22");

            DirColors.Add("chartmaxtext", "FFFFFFFF");
            DirColors.Add("chartmintext", "FFFFFFFF");
            DirColors.Add("chartorderbidtext", "FFFFFFFF");
            DirColors.Add("chartorderasktext", "FFFFFFFF");
            DirColors.Add("chartcursortext", "FFFFFFFF");
            DirColors.Add("chartnowtext", "FFFFFFFF");
            DirColors.Add("chartavgtext", "FFFFFFFF");

        }
        private string ReverseColor(string color)
        {
            string val = "";
            byte A = (Convert.ToByte((color[0].ToString() + color[1].ToString()), 16));
            byte G = (byte)(255 - Convert.ToByte((color[2].ToString() + color[3].ToString()), 16));
            byte B = (byte)(255 - Convert.ToByte((color[4].ToString() + color[5].ToString()), 16));
            byte R = (byte)(255 - Convert.ToByte((color[6].ToString() + color[7].ToString()), 16));
            val = $"{A:X2}{R:X2}{G:X2}{B:X2}";
            return val;
        }

        private void SetColors()
        {
            var bc = new BrushConverter();
            string bid = GetColor("bid");
            string bidback = GetColor("bidback");
            string ask = GetColor("ask");
            string askback = GetColor("askback");
            string text = GetColor("text");
            string back = GetColor("back");

            string chartbid = GetColor("chartorderbid");
            string chartask = GetColor("chartorderask");
            string chartavg = GetColor("chartavg");
            string chartmax = GetColor("chartmax");
            string chartmin = GetColor("chartmin");
            string chartnow = GetColor("chartnow");
            string chartcursor = GetColor("chartcursor");

            string chartbidtext = GetColor("chartorderbidtext");
            string chartasktext = GetColor("chartorderasktext");
            string chartavgtext = GetColor("chartavgtext");
            string chartmaxtext = GetColor("chartmaxtext");
            string chartmintext = GetColor("chartmintext");
            string chartnowtext = GetColor("chartnowtext");
            string chartcursortext = GetColor("chartcursortext");

            //기본색상

            ColorText = (Brush)bc.ConvertFrom($"#{text}");
            ColorBack = (Brush)bc.ConvertFrom($"#{back}");
            ColorBorder = (Brush)bc.ConvertFrom($"#{ReverseColor(back)}");

            ColorBid = (Brush)bc.ConvertFrom($"#{bid}");
            ColorBidBack = (Brush)bc.ConvertFrom($"#{bidback}");

            ColorAsk = (Brush)bc.ConvertFrom($"#{ask}");
            ColorAskBack = (Brush)bc.ConvertFrom($"#{askback}");

            ColorChartOrderBid = (Brush)bc.ConvertFrom($"#{chartbid}");
            ColorChartOrderAsk = (Brush)bc.ConvertFrom($"#{chartask}");
            ColorChartAvg = (Brush)bc.ConvertFrom($"#{chartavg}");
            ColorChartMax = (Brush)bc.ConvertFrom($"#{chartmax}");
            ColorChartMin = (Brush)bc.ConvertFrom($"#{chartmin}");
            ColorChartNow = (Brush)bc.ConvertFrom($"#{chartnow}");
            ColorChartCursor = (Brush)bc.ConvertFrom($"#{chartcursor}");

            ColorChartOrderBidText = (Brush)bc.ConvertFrom($"#{chartbidtext}");
            ColorChartOrderAskText = (Brush)bc.ConvertFrom($"#{chartasktext}");
            ColorChartAvgText = (Brush)bc.ConvertFrom($"#{chartavgtext}");
            ColorChartMaxText = (Brush)bc.ConvertFrom($"#{chartmaxtext}");
            ColorChartMinText = (Brush)bc.ConvertFrom($"#{chartmintext}");
            ColorChartNowText = (Brush)bc.ConvertFrom($"#{chartnowtext}");
            ColorChartCursorText = (Brush)bc.ConvertFrom($"#{chartcursortext}");

            ColorNoneColor = (Brush)bc.ConvertFrom($"#{nonecolor}");


        }
        private void SaveColors()
        {
            fileIO.FileWrite<Dictionary<string, string>>(FilePath, FileName, DirColors);
        }
        private void LoadColors()
        {
            DirColors = fileIO.FileRead<Dictionary<string, string>>(FilePath,FileName);
        }
        #endregion
        public void SetTest(ref Brush chang, Brush brush)
        {
            chang = brush;
        }
        public Colors()
        {
            LoadColors();

            //읽어온 색상이 없을경우 기본설정된 컬러를 불러옴.
            if(DirColors is null)
            {
                //기존 컬러를 저장하고
                DefaultColors();

                //저장된 기본컬러를 파일로 저장.
                SaveColors();
            }

            SetColors();
        }

        public void SetColor()
        {
            SetColors();
            ColorUpdate();
            SaveColors();
        }

        /// <summary>
        /// 딕셔너리에 저장된 값을 불러옴.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetColor(string name)
        {
            string Color = "";
            try
            {
                Color = DirColors[name];
            }
            catch (Exception e) 
            {
                Color = nonecolor;
                Logs.Loginstance.SettingLog($"등록된 색상이 없습니다 -> {name}");
            }

            return Color;
        }

        /// <summary>
        /// 딕셔너리에 저장된 값으 변경
        /// </summary>
        /// <param name="key"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public bool SetColor(string key,string color)
        {
            bool b = true;
            try
            {
                DirColors[key] = color;
            }
            catch (Exception e)
            {
                b = false;
            }
            return b;
        }

        /// <summary>
        /// 컬러를 등록시켜줌
        /// </summary>
        /// <param name="key"></param>
        /// <param name="color"></param>
        public void AddColor(string key, string color)
        {
            DirColors.Add(key, color);
            SaveColors();
        }

        /// <summary>
        /// DirColors에서 직접적으로 컬러값을 가져옴.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Brush DirGetColor(string key)
        {
            var bc = new BrushConverter();
            return (Brush)bc.ConvertFrom($"#FF{GetColor(key)}");
        }

        /// <summary>
        /// 목록 불러옴.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetColorList()
        {
            return DirColors;
        }
    }
}
