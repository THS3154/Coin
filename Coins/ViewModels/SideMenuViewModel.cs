using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Resources;
using Microsoft.Xaml.Behaviors.Layout;
using System.Reflection;
using System.Xml.Linq;
using System.Collections;
using System.IO;
using System.Windows;
using Prism.Regions;
using Coins.Views;
using DB;
using System.Data.SqlClient;
using Permission;
using Prism.Events;
using System.Windows.Media;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using AdminSetting.Evnets;

namespace Coins.ViewModels
{
    public class MarketInfo : BindableBase
    {
        private int number;
        public int Number
        {
            get { return number; }
            set { SetProperty(ref number, value); }
        }
        private string marketname;
        public string MarketName
        {
            get { return marketname; }
            set { SetProperty(ref marketname, value); }
        }
        private string viewer;
        public string Viewer
        {
            get { return viewer; }
            set { SetProperty(ref viewer, value); }
        }
        private BitmapImage image;
        public BitmapImage Image
        {
            get { return image; }
            set { SetProperty(ref image, value); }
        }
        private Visibility isimage;
        public Visibility IsImage
        {
            get { return isimage; }
            set { SetProperty(ref isimage, value); }
        }
        private Visibility istext;
        public Visibility IsText
        {
            get { return istext; }
            set { SetProperty(ref istext, value); }
        }
        private bool iselected;
        public bool IsSelected
        {
            get { return iselected; }
            set { SetProperty(ref iselected, value); }
        }
        private Brush backcolor;
        public Brush BackColor
        {
            get { return backcolor; }
            set { SetProperty(ref backcolor, value); }
        }
        public MarketInfo()
        {
            MarketName = "";
            Viewer = "";
            IsText = Visibility.Visible;
            IsImage = Visibility.Collapsed;
            IsSelected = false;
            SolidColorBrush color = (SolidColorBrush)Application.Current.Resources["MenuButtonBackColor"];
            BackColor = color;
        }
    }
    public class SideMenuViewModel : BindableBase
	{
        #region Prism
        IEventAggregator _ea;
        IRegionManager _rm;
        PM _pm;
        #endregion Prism

        #region Model

        private Visibility isadmin;
        public Visibility IsAdmin
        {
            get { return isadmin; }
            set { SetProperty(ref isadmin, value); }
        }

        private MarketInfo setting;
        public MarketInfo Setting
        {
            get { return setting; }
            set { SetProperty(ref setting, value); }
        }
        private MarketInfo dashboard;
        public MarketInfo Dashboard
        {
            get { return dashboard; }
            set { SetProperty(ref dashboard, value); }
        }

        private List<MarketInfo> marketlist;
        public List<MarketInfo> MarketList
        {
            get { return marketlist; }
            set { SetProperty(ref marketlist, value); }
        }

        private Brush DefalutColor { get; set; }
        private Brush HoverColor { get; set; } 
        #endregion Model

        #region Event
        private DelegateCommand<MarketInfo> commandviewer;
        public DelegateCommand<MarketInfo> CommandViewer =>
            commandviewer ?? (commandviewer = new DelegateCommand<MarketInfo>(ExecuteCommandViewer));

        void ExecuteCommandViewer(MarketInfo View)
        {
            Setting.BackColor = DefalutColor;
            Dashboard.BackColor = DefalutColor;
            for (int i = 0; i < MarketList.Count; i++)
            {
                if (MarketList[i].IsSelected)
                {
                    MarketList[i].BackColor = DefalutColor;
                    MarketList[i].IsSelected = false;
                    break;
                }

            }

            View.IsSelected = true;
            View.BackColor = HoverColor;
            _rm.RequestNavigate("Viewer", View.Viewer);


        }
        #endregion Event

        #region Function

        /// <summary>
        /// 바이트 배열을 비트맵이미지로 변경해줌.
        /// </summary>
        /// <param name="Arr"></param>
        /// <returns></returns>
        private BitmapImage ByteArrayToBitmapImage(byte[] Arr)
        {
            if (Arr is null || Arr.Length == 0)
                return null;

            BitmapImage bitmapImage = new BitmapImage();

            using (MemoryStream stream = new MemoryStream(Arr))
            {
                try
                {
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze(); // Important: Freeze the BitmapImage to make it accessible from other threads.
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error converting byte array to BitmapImage: " + ex.Message);
                }
            }

            return bitmapImage;
        }


        private List<MarketInfo> GetMarketList()
        {
            List<MarketInfo> list = new List<MarketInfo>();


            string query = $"SELECT * FROM MARKETS";

            SqlDataReader read = Mssql.Instance.ExecuteReaderQuery(query);
            MarketInfo val = new MarketInfo();
            if (read is not null)
            {
                while (read.Read())
                {
                    val = new MarketInfo();
                    val.MarketName = read["NAME"].ToString();
                    val.Number = Convert.ToInt32(read["NUMBER"]);
                    val.Viewer = read["VIEWNAME"].ToString();
                    if (read["IMGSIZE"] is not null)
                    {
                        object imagebytes = read["IMG"];
                        val.Image = ByteArrayToBitmapImage((byte[])imagebytes);
                    }
                    if( val.Image is null)
                    {
                        val.IsText = Visibility.Visible;
                        val.IsImage = Visibility.Collapsed;
                    }
                    else
                    {
                        val.IsText = Visibility.Collapsed;
                        val.IsImage = Visibility.Visible;
                    }
                    list.Add(val);
                }
                read.Close();

            }
            else
            {
                //DB끊어졌을때 로컬
                val = new MarketInfo();
                val.MarketName = "업비트";
                val.Viewer = "UpbitViewer";
                val.IsText = Visibility.Visible;
                val.IsImage = Visibility.Collapsed;
                list.Add(val);
            }

            return list;
        }
        private void UpdateMarketList()
        {
            List<MarketInfo> list = GetMarketList();
            int index = -1;
            for (int i = 0; i < MarketList.Count; i++)
            {
                if (MarketList[i].IsSelected)
                {
                    index = i;
                    break;
                }
            }
            if(index != -1)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Number == MarketList[index].Number)
                    {
                        list[i].IsSelected = MarketList[index].IsSelected;
                        break;
                    }
                }
            }

            MarketList = list;
        }
        #endregion Function
        public SideMenuViewModel(IRegionManager rm, IEventAggregator ea, PM pm)
        {
            _rm = rm;
            _ea = ea;
            IsAdmin = (pm.Auth >= 1 ? Visibility.Visible : Visibility.Collapsed);
            MarketList = GetMarketList();

            DefalutColor = (SolidColorBrush)Application.Current.Resources["MenuButtonBackColor"];
            HoverColor = (SolidColorBrush)Application.Current.Resources["MenuButtonHoverBackColor"];

            Dashboard = new MarketInfo();
            Dashboard.MarketName = "DashboardViewer";
            Dashboard.Viewer = "DashboardViewer";
            Dashboard.Image = ByteArrayToBitmapImage(Properties.Resources.dashboard);
            Dashboard.BackColor = HoverColor;

            Setting = new MarketInfo();
            Setting.MarketName = "SettingViewer";
            Setting.Viewer = "SettingViewer";
            Setting.Image = ByteArrayToBitmapImage(Properties.Resources.setting);

            //사이드 이미지 변경이벤트가 발생됐을때 업데이트
            _ea.GetEvent<MessageSideUpdate>().Subscribe(UpdateMarketList);

        }

        
	}
}
