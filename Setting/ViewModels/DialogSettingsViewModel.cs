using Language;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using PublicColor.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Setting.ViewModels
{
    public class Btns : BindableBase
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string printname;
        public string PrintName
        {
            get { return printname; }
            set { SetProperty(ref printname, value); }
        }
    }
    public class DialogSettingsViewModel : BindableBase, ILanguage, IDialogAware
    {
        #region Lang
        private Language.Lang.Dialog lang;
        public Language.Lang.Dialog Lang
        {
            get { if (lang is null) lang = new Language.Lang.Dialog(); return lang; }
            set { SetProperty(ref lang, value); }
        }
        public void SetLanguage()
        {
            Lang.UpdateLang();
        }
        #endregion

        #region Prism
        private IEventAggregator _ea;
        private IRegionManager _rm;
        private IContainerProvider _cp;

        #endregion Prism

        #region Models
        private string _title = "Setting";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private List<Btns> optionlist;
        public List<Btns> OptionList
        {
            get { return optionlist; }
            set { SetProperty(ref optionlist, value); }
        }
        #endregion Models

        #region Event
        private DelegateCommand<string> btnmenuchangedcommand;
        public DelegateCommand<string> BtnMenuChangedCommand =>
            btnmenuchangedcommand ?? (btnmenuchangedcommand = new DelegateCommand<string>(ExecuteBtnMenuChangedCommand));

        void ExecuteBtnMenuChangedCommand(string parameter)
        {
            if(parameter is string str)
            {
                string name = str;
                var a = _rm.Regions.ContainsRegionWithName("SettingMain");
                //IRegion region = _rm.Regions["SettingMain"];

                _rm.RegisterViewWithRegion("SettingMain", StringToType(name));
                //_rm.RequestNavigate("SettingMain", name);// RegisterViewWithRegion;
            }
        }


        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));



        public event Action<IDialogResult> RequestClose;
        #endregion Event

        #region Interface
        protected virtual void CloseDialog(string parameter)
        {

            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
                result = ButtonResult.OK;
            else if (parameter?.ToLower() == "false")
                result = ButtonResult.Cancel;

            RaiseRequestClose(new DialogResult(result));
        }
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public virtual bool CanCloseDialog()
        {
            return true;
        }

        public virtual void OnDialogClosed()
        {

        }
        #endregion

        private Type StringToType(string str)
        {
            var type = Type.GetType(str);

            if (type is not null)
            {
                return type;
            }
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(str);
                if (type != null)
                    return type;
            }
            return type;
        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            string strtype = parameters.GetValue<string>("type");
            _rm.RegisterViewWithRegion("SettingMain", StringToType(strtype));
        }
        
        public DialogSettingsViewModel(IEventAggregator ea, IRegionManager rm, IContainerProvider cp )
        {
            _ea = ea;
            _rm = rm;
            _cp = cp;
            //SetOptions();
            //_rm.RegisterViewWithRegion("SettingMain", typeof(PublicColor.View.ColorView));


        }

        private void SetOptions()
        {
            OptionList = new List<Btns>();
            Btns ColorView = new Btns();
            ColorView.PrintName = "ColorView";
            ColorView.Name = "PublicColor.Views.ColorView";

            Btns test = new Btns();
            test.PrintName = "test";
            test.Name = "Coins.Views.test";

            OptionList.Add(ColorView);
            OptionList.Add(test);
        }

    }
}
