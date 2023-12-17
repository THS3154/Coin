using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls.Ribbon;
using Prism.Events;
using Prism.Regions;
using Permission;
using DB;
using System.Data.SqlClient;
using Microsoft.Win32;
using System.IO;
using AdminSetting.Evnets;

namespace AdminSetting.ViewModels
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
        private bool isenable;
        public bool IsEnable
        {
            get { return isenable; }
            set { SetProperty(ref isenable, value); }
        }
        private int imagesize;
        public int ImageSize
        {
            get { return imagesize; }
            set { SetProperty(ref imagesize, value); }
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
            IsEnable = true;
            SolidColorBrush color = (SolidColorBrush)Application.Current.Resources["MenuButtonBackColor"];
            BackColor = color;
        }
    }
    public class MarketListViewModel : BindableBase
    {


        #region Prism
        IRegionManager _rm;
        IEventAggregator _ea;
        PM _pm;
        #endregion Prism

        #region Model
        private List<MarketInfo> marketlist;
        public List<MarketInfo> MarketList
        {
            get { return marketlist; }
            set { SetProperty(ref marketlist, value); }
        }
        private List<MarketInfo> KeepMarketList;
        #endregion Model

        #region Event
        string filePath = "";
        private DelegateCommand<MarketInfo> commandpicture;
        public DelegateCommand<MarketInfo> CommandPicture =>
            commandpicture ?? (commandpicture = new DelegateCommand<MarketInfo>(ExecuteCommandPicture));

        void ExecuteCommandPicture(MarketInfo info)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "이미지 파일|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog.Multiselect = false;

            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                filePath = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(filePath);
                BitmapImage imageSource = new BitmapImage(new Uri(filePath));
                info.Image = imageSource;
                info.IsImage = Visibility.Visible;
                info.IsText = Visibility.Collapsed;
                if (Mssql.Instance.GetConnection())
                {
                    string query = $"UPDATE MARKETS SET NAME = '{info.MarketName}', IMG =@IMAGE, IMGSIZE={imageBytes.Length}, VIEWNAME = '{info.Viewer}',ENABLE = {(info.IsEnable ? 1 : 0)} WHERE NUMBER = {info.Number}";
                    Mssql.Instance.ExecuteNonQuery(query,"@IMAGE",imageBytes);
                    _ea.GetEvent<MessageSideUpdate>().Publish();
                }
                    
                //info.ImageSize = imageSource;
            } 
        }
        /// <summary>
        /// 마켓 수정용
        /// </summary>
        private DelegateCommand<MarketInfo> commandupdate;
        public DelegateCommand<MarketInfo> CommandUpdate =>
            commandupdate ?? (commandupdate = new DelegateCommand<MarketInfo>(ExecuteCommandUpdate));

        void ExecuteCommandUpdate(MarketInfo info)
        {
            string query = $"UPDATE MARKETS SET NAME = '{info.MarketName}', IMG ={info.Image}, IMGSIZE={info.ImageSize}, VIEWNAME = '{info.Viewer}',ENABLE = {info.IsEnable} WHERE NUMBER = {info.Number}";
            Mssql.Instance.ExecuteNonQuery(query);
            //여기 수정로그 등록해줘야함.
        }

        /// <summary>
        /// 마켓추가용
        /// </summary>
        private DelegateCommand<MarketInfo> commandinsert;
        public DelegateCommand<MarketInfo> CommandInsert =>
            commandinsert ?? (commandinsert = new DelegateCommand<MarketInfo>(ExecuteCommandInsert));

        void ExecuteCommandInsert(MarketInfo info)
        {
            
            string query = "";
            if (filePath != "")
            {
                byte[] imageBytes = File.ReadAllBytes(filePath);
                query = $"UPDATE MARKETS SET NAME = '{info.MarketName}', IMG =@IMAGE, IMGSIZE={imageBytes.Length}, VIEWNAME = '{info.Viewer}',ENABLE = {(info.IsEnable ? 1 : 0)} WHERE NUMBER = {info.Number}";
                if (Mssql.Instance.GetConnection())
                    Mssql.Instance.ExecuteNonQuery(query, "@IMAGE", imageBytes);
            }
            else
            {
                query = $"UPDATE MARKETS SET NAME = '{info.MarketName}', VIEWNAME = '{info.Viewer}',ENABLE = {(info.IsEnable ? 1 : 0)} WHERE NUMBER = {info.Number}";
                if (Mssql.Instance.GetConnection())
                    Mssql.Instance.ExecuteNonQuery(query);
            }
            _ea.GetEvent<MessageSideUpdate>().Publish();
        }
        #endregion

        #region Fucntion
        /// <summary>
        /// 거래소 목록 읽어옴.
        /// </summary>
        private List<MarketInfo> GetMarketList()
        {
            List< MarketInfo > list = new List< MarketInfo >();
            string query = $"SELECT * FROM MARKETS";

            if (Mssql.Instance.GetConnection())
            {
                SqlDataReader read = Mssql.Instance.ExecuteReaderQuery(query);
                if (read is not null)
                {
                    while (read.Read())
                    {
                        MarketInfo info = new MarketInfo();
                        info.Number = Convert.ToInt32(read["NUMBER"]);
                        info.MarketName = read["NAME"].ToString();
                        info.ImageSize = Convert.ToInt32(read["IMGSIZE"]);
                        if (info.ImageSize != 0)
                        {
                            object imagebytes = read["IMG"];
                            info.Image = ByteArrayToBitmapImage((byte[])imagebytes);
                            info.IsText = Visibility.Collapsed;
                            info.IsImage = Visibility.Visible;
                        }
                        info.Viewer = read["VIEWNAME"].ToString();
                        info.IsEnable = Convert.ToBoolean(read["ENABLE"]);
                        list.Add(info);
                    }
                    read.Close();
                }
            }
                

            return list;
        }



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

        #endregion Function



        public MarketListViewModel(IRegionManager rm, IEventAggregator ea, PM pm)
        {
            _rm = rm;
            _ea = ea;
            _pm = pm;

            MarketList = GetMarketList();
            KeepMarketList = MarketList;
        }
    }
}
