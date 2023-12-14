using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language.Lang
{
    public class Menu : BindableBase, ILang
    {
        public Menu()
        {
            UpdateLang();
        }

        public void UpdateLang()
        {
            LAccess = Language.Lang.GetWord("Menu", "Access");
            LLang = Language.Lang.GetWord("Menu", "Lang");
            LSetting = Language.Lang.GetWord("Menu", "Setting");
            LUpbitAccess = Language.Lang.GetWord("Menu", "UpbitAccess");
            LColor = Language.Lang.GetWord("Menu", "Color");
        }

        private string laccess;
        public string LAccess
        {
            get { return laccess; }
            set { SetProperty(ref laccess, value); }
        }

        private string llang;
        public string LLang
        {
            get { return llang; }
            set { SetProperty(ref llang, value); }
        }

        private string lsetting;
        public string LSetting
        {
            get { return lsetting; }
            set { SetProperty(ref lsetting, value); }
        }

        private string lupbitaccess;
        public string LUpbitAccess
        {
            get { return lupbitaccess; }
            set { SetProperty(ref lupbitaccess, value); }
        }

        private string lcolor;
        public string LColor
        {
            get { return lcolor; }
            set { SetProperty(ref lcolor, value); }
        }
    }
}
