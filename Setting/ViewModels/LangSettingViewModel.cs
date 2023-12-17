using Language.Lang;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Setting.ViewModels
{
    public class LangSettingViewModel : BindableBase
    {
        private bool kr;
        public bool KR
        {
            get { return kr; }
            set { SetProperty(ref kr, value); }
        }

        private bool en;
        public bool EN
        {
            get { return en; }
            set { SetProperty(ref en, value); }
        }

        private DelegateCommand commandupdate;
        public DelegateCommand CommandUpdate =>
            commandupdate ?? (commandupdate = new DelegateCommand(ExecuteCommandUpdate));

        void ExecuteCommandUpdate()
        {
            string lang = "";
            if (KR)
                lang = "KR";
            if (EN)
                lang = "EN";
            if (Language.Language.Lang.LangType != lang.ToLower())
            {
                Language.Language.Lang.SetLang(lang.ToLower());
                MessageBox.Show("변경됐습니다.");
            }
        }
        public LangSettingViewModel()
        {
            if (Language.Language.Lang.LangType == "kr")
            {
                KR = true;
                EN = false;
            }
            else
            {
                EN = true;
                KR = false;
            }
        }
    }
}
