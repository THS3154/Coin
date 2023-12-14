using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language.Lang
{
    public class Message : BindableBase, ILang
    {
        public Message()
        {
            UpdateLang();
        }

        public void UpdateLang()
        {
            LErrorOrderMinPrice = Language.Lang.GetWord("Message", "ErrorOrderMinPrice");
            LErrorOrderOverflowQuantity = Language.Lang.GetWord("Message", "ErrorOrderOverflowQuantity");
        }

        private string errororderminprice;
        public string LErrorOrderMinPrice
        {
            get { return errororderminprice; }
            set { SetProperty(ref errororderminprice, value); }
        }

        private string errororderoverflowquantity;
        public string LErrorOrderOverflowQuantity
        {
            get { return errororderoverflowquantity; }
            set { SetProperty(ref errororderoverflowquantity, value); }
        }
    }
}
