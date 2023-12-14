using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language.Lang
{
    public class Dialog : BindableBase, ILang
    {
        public Dialog()
        {
            UpdateLang();
        }
        public void UpdateLang()
        {
            LAccess = Language.Lang.GetWord("Dialog", "Access");
            LAdd = Language.Lang.GetWord("Dialog", "Add");
            LCancel = Language.Lang.GetWord("Dialog", "Cancel");
            LYes = Language.Lang.GetWord("Dialog", "Yes");
            LNo = Language.Lang.GetWord("Dialog", "No");
        }
        private string laccess;
        public string LAccess
        {
            get { return laccess; }
            set { SetProperty(ref laccess, value); }
        }

        private string ladd;
        public string LAdd
        {
            get { return ladd; }
            set { SetProperty(ref ladd, value); }
        }

        private string lcancel;
        public string LCancel
        {
            get { return lcancel; }
            set { SetProperty(ref lcancel, value); }
        }

        private string lno;
        public string LNo
        {
            get { return lno; }
            set { SetProperty(ref lno, value); }
        }

        private string lyes;
        public string LYes
        {
            get { return lyes; }
            set { SetProperty(ref lyes, value); }
        }
    }
}
