using Coins.Views;
using DB;
using Permission;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Coins.ViewModels
{
    public class Settings : BindableBase
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private Brush backcolor;
        public Brush BackColor
        {
            get { return backcolor; }
            set { SetProperty(ref backcolor, value); }
        }
        private bool isselected;
        public bool IsSelected
        {
            get { return isselected; }
            set { SetProperty(ref isselected, value); }
        }
        private string viewer;
        public string Viewer
        {
            get { return viewer; }
            set { SetProperty(ref viewer, value); }
        }

        private int auth;
        public int Auth
        {
            get { return auth; }
            set { SetProperty(ref auth, value); }
        }
        private bool isenable;
        public bool IsEnable
        {
            get { return isenable; }
            set { SetProperty(ref isenable, value); }
        }

        private Visibility isswho;
        public Visibility IsShow
        {
            get { return isswho; }
            set { SetProperty(ref isswho, value); }
        }
        public Settings(Brush color)
        {
            BackColor = color;
            IsSelected = false;
            Viewer = "";
            Name = "Setting";
            Auth = 0;
            IsEnable = true;
            IsShow = Visibility.Visible;
        }

    }
    public class SettingViewerViewModel : BindableBase
    {
        #region Prism
        IRegionManager _rm;
        PM _pm;
        #endregion Prism

        #region Model
        private Brush hovercolor;
        public Brush HoverColor
        {
            get { return hovercolor; }
            set { SetProperty(ref hovercolor, value); }
        }
        private Brush selectedcolor;
        public Brush SelectedColor
        {
            get { return selectedcolor; }
            set { SetProperty(ref selectedcolor, value); }
        }
        private Brush defaultcolor;
        public Brush DefaultColor
        {
            get { return defaultcolor; }
            set { SetProperty(ref defaultcolor, value); }
        }
        private Visibility isadmin;
        public Visibility IsAdmin
        {
            get { return isadmin; }
            set { SetProperty(ref isadmin, value); }
        }

        private List<Settings> usersettings;
        public List<Settings> UserSettings
        {
            get { return usersettings; }
            set { SetProperty(ref usersettings, value); }
        }
        private List<Settings> adminsettings;
        public List<Settings> AdminSettings
        {
            get { return adminsettings; }
            set { SetProperty(ref adminsettings, value); }
        }
        #endregion Model

        #region Event
        private DelegateCommand<object> commandusersetting;
        public DelegateCommand<object> CommandUserSetting =>
            commandusersetting ?? (commandusersetting = new DelegateCommand<object>(ExecuteCommandSetting));

        void ExecuteCommandSetting(object sender)
        {
            if(sender is Settings setting)
            {
                
                if(_pm.Auth >= setting.Auth)
                {
                    UpdateSettingClick(ref usersettings);
                    UpdateSettingClick(ref adminsettings);
                    setting.BackColor = SelectedColor;
                    setting.IsSelected = true;
                    _rm.Regions["SettingViewer"].RemoveAll();
                    _rm.RequestNavigate("SettingViewer", setting.Viewer);
                }
                else
                {
                    MessageBox.Show("권한부족");
                }
                
                
            }
        }
        #endregion Event
        private void UpdateSettingClick(ref List<Settings> settings)
        {
            for(int i = 0; i<settings.Count; i++)
            {
                if (settings[i].IsSelected)
                {
                    settings[i].IsSelected = false;
                    settings[i].BackColor = DefaultColor;
                }
            }
        }
        /// <summary>
        /// 셋팅목록 불러옴
        /// </summary>
        private void SetUserSettings()
        {
            UserSettings = new List<Settings>();
            AdminSettings = new List<Settings>();
            string query = $"SELECT * FROM AUTHLIST";

            SqlDataReader read = Mssql.Instance.ExecuteReaderQuery(query);
            if (read is not null)
            {
                while (read.Read())
                {
                    Settings set = new Settings(DefaultColor);
                    set.Name = read["NAME"].ToString();
                    set.Auth = Convert.ToInt32(read["LEVEL"]);
                    set.IsEnable = Convert.ToBoolean(read["ENABLE"]);
                    set.Viewer = read["VIEWNAME"].ToString();
                    set.IsShow = Convert.ToBoolean(read["SHOW"]) ? Visibility.Visible : Visibility.Collapsed;
                    if (set.Auth <= 1)
                    {
                        UserSettings.Add(set);
                    }
                    else if(set.Auth >= 9999)
                    {
                        AdminSettings.Add(set);
                    }
                }
            }
            else
            {
                
                Settings Color = new Settings(DefaultColor);
                Color.Name = "색상";
                Color.Viewer = "ColorView";
                UserSettings.Add(Color);

                Settings Lang = new Settings(DefaultColor);
                Lang.Name = "언어";
                Lang.Viewer = "LangSetting";
                UserSettings.Add(Lang);

                Settings UpbitAPI = new Settings(DefaultColor);
                UpbitAPI.Name = "UpbitAPI Key";
                UpbitAPI.Viewer = "AddAccess";
                UserSettings.Add(UpbitAPI);
            }
            

        }
        #region Functions

        #endregion Functions

        public SettingViewerViewModel(IRegionManager rm, IEventAggregator ea, PM pm)
        {
            _rm = rm;
            _pm = pm;
            var bc = new BrushConverter();
            HoverColor = (Brush)bc.ConvertFrom($"#11333333");
            SelectedColor = (Brush)bc.ConvertFrom($"#33333333");
            DefaultColor = Brushes.Transparent;
            IsAdmin = (pm.AdminCheck() ? Visibility.Visible : Visibility.Collapsed);

            SetUserSettings();


        }
    }
}
