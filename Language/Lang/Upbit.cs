using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language.Lang
{
    public class Upbit : BindableBase, ILang
    {
        public Upbit() 
        {
            UpdateLang();
        }
        public virtual void UpdateLang()
        {
            LKeyError = Language.Lang.GetWord("Upbit", "KeyError");
            LKeyComment = Language.Lang.GetWord("Upbit", "KeyComment");
        }
        private string lkeyError;
        public string LKeyError
        {
            get { return lkeyError; }
            set { SetProperty(ref lkeyError, value); }
        }
        private string lkeycomment;
        public string LKeyComment
        {
            get { return lkeycomment; }
            set { SetProperty(ref lkeycomment, value); }
        }
        private string lorderchance;
        public string LOrderChance
        {
            get { return lorderchance; }
            set { SetProperty(ref lorderchance, value); }
        }
    }
}
