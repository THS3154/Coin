using Prism.Mvvm;

namespace Language.Lang
{
    public class Balance : BindableBase, ILang
    {
        public Balance() 
        {
            UpdateLang();
        }

        public void UpdateLang()
        {
            LBalance = Language.Lang.GetWord("Balance", "_Balance");
            LAvgBuyPrice = Language.Lang.GetWord("Balance", "AvgBuyPrice");
            LCoin = Language.Lang.GetWord("Balance", "Coin");
            LCurrency = Language.Lang.GetWord("Balance", "Currency");
            LLocked = Language.Lang.GetWord("Balance", "Locked");
            LMoney = Language.Lang.GetWord("Balance", "Money");
            LNotAccess = Language.Lang.GetWord("Balance", "NotAccess");
        }

        private string lnotaccess;
        public string LNotAccess
        {
            get { return lnotaccess; }
            set { SetProperty(ref lnotaccess, value); }
        }

        private string lcurrency;
        public string LCurrency
        {
            get { return lcurrency; }
            set { SetProperty(ref lcurrency, value); }
        }

        private string lcoin;
        public string LCoin
        {
            get { return lcoin; }
            set { SetProperty(ref lcoin, value); }
        }
        private string lbalance;
        public string LBalance
        {
            get { return lbalance; }
            set { SetProperty(ref lbalance, value); }
        }
        private string llocked;
        public string LLocked
        {
            get { return llocked; }
            set { SetProperty(ref llocked, value); }
        }
        private string lavgbuyprice;
        public string LAvgBuyPrice
        {
            get { return lavgbuyprice; }
            set { SetProperty(ref lavgbuyprice, value); }
        }

        private string lmoney;
        public string LMoney
        {
            get { return lmoney; }
            set { SetProperty(ref lmoney, value); }
        }
    }
}
