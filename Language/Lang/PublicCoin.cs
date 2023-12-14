using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language.Lang
{
    public class PublicCoin : BindableBase, ILang
    {
        public PublicCoin() 
        {
            UpdateLang();
        }

        public void UpdateLang()
        {
            LCoinBuy = Language.Lang.GetWord("PublicCoin", "CoinBuy");
            LCoinName = Language.Lang.GetWord("PublicCoin", "CoinName");
            LCoinSell = Language.Lang.GetWord("PublicCoin", "CoinSell");
            LRate = Language.Lang.GetWord("PublicCoin", "Rate");
            LMarket = Language.Lang.GetWord("PublicCoin", "Market");
            LLimit = Language.Lang.GetWord("PublicCoin", "Limit");

            LQuantity = Language.Lang.GetWord("PublicCoin", "Quantity");
            LPrice = Language.Lang.GetWord("PublicCoin", "Price");
            LOrderPrice = Language.Lang.GetWord("PublicCoin", "OrderPrice");
            LTotalPrice = Language.Lang.GetWord("PublicCoin", "TotalPrice");
            LMinOrderPrice = Language.Lang.GetWord("PublicCoin", "MinOrderPrice");
            LFee = Language.Lang.GetWord("PublicCoin", "Fee");
            LConclusion = Language.Lang.GetWord("PublicCoin", "Conclusion");
            LNotConclusion = Language.Lang.GetWord("PublicCoin", "NotConclusion");
            LCurrency = Language.Lang.GetWord("PublicCoin", "Currency");
            LOrderList = Language.Lang.GetWord("PublicCoin", "OrderList");

            LChanceMarketName = Language.Lang.GetWord("PublicCoin", "ChanceMarketName");
            LChanceOrderPrice = Language.Lang.GetWord("PublicCoin", "ChanceOrderPrice");
            LChanceOrderQuantity = Language.Lang.GetWord("PublicCoin", "ChanceOrderQuantity");
            LChanceOrderNotConclusion = Language.Lang.GetWord("PublicCoin", "ChanceOrderNotConclusion");
            LChanceOrderBtnCancel = Language.Lang.GetWord("PublicCoin", "ChanceOrderBtnCancel");

            LMin = Language.Lang.GetWord("PublicCoin", "Min");
            LDay = Language.Lang.GetWord("PublicCoin", "Day");
            LWeek = Language.Lang.GetWord("PublicCoin", "Week");
            LMonth = Language.Lang.GetWord("PublicCoin", "Month");

        }
        private string lmin;
        public string LMin
        {
            get { return lmin; }
            set { SetProperty(ref lmin, value); }
        }
        private string lday;
        public string LDay
        {
            get { return lday; }
            set { SetProperty(ref lday, value); }
        }
        private string lweek;
        public string LWeek
        {
            get { return lweek; }
            set { SetProperty(ref lweek, value); }
        }
        private string lmonth;
        public string LMonth
        {
            get { return lmonth; }
            set { SetProperty(ref lmonth, value); }
        }
        private string lcoinbuy;
        public string LCoinBuy
        {
            get { return lcoinbuy; }
            set { SetProperty(ref lcoinbuy, value); }
        }

        private string lcoinname;
        public string LCoinName
        {
            get { return lcoinname; }
            set { SetProperty(ref lcoinname, value); }
        }

        private string lcoinsell;
        public string LCoinSell
        {
            get { return lcoinsell; }
            set { SetProperty(ref lcoinsell, value); }
        }

        private string lmarket;
        public string LMarket
        {
            get { return lmarket; }
            set { SetProperty(ref lmarket, value); }
        }

        private string lrate;
        public string LRate
        {
            get { return lrate; }
            set { SetProperty(ref lrate, value); }
        }

        private string llimit;
        public string LLimit
        {
            get { return llimit; }
            set { SetProperty(ref llimit, value); }
        }

        private string lquantity;
        public string LQuantity
        {
            get { return lquantity; }
            set { SetProperty(ref lquantity, value); }
        }

        private string lprice;
        public string LPrice
        {
            get { return lprice; }
            set { SetProperty(ref lprice, value); }
        }

        private string lorderprice;
        public string LOrderPrice
        {
            get { return lorderprice; }
            set { SetProperty(ref lorderprice, value); }
        }

        private string ltotalprice;
        public string LTotalPrice
        {
            get { return ltotalprice; }
            set { SetProperty(ref ltotalprice, value); }
        }

        private string lminorderprice;
        public string LMinOrderPrice
        {
            get { return lminorderprice; }
            set { SetProperty(ref lminorderprice, value); }
        }

        private string lfee;
        public string LFee
        {
            get { return lfee; }
            set { SetProperty(ref lfee, value); }
        }

        private string lconclusion;
        public string LConclusion
        {
            get { return lconclusion; }
            set { SetProperty(ref lconclusion, value); }
        }

        private string lnotconclusion;
        public string LNotConclusion
        {
            get { return lnotconclusion; }
            set { SetProperty(ref lnotconclusion, value); }
        }

        private string lcurrency;
        public string LCurrency
        {
            get { return lcurrency; }
            set { SetProperty(ref lcurrency, value); }
        }

        private string lorderlist;
        public string LOrderList
        {
            get { return lorderlist; }
            set { SetProperty(ref lorderlist, value); }
        }

        private string chanceorderpice;
        public string LChanceOrderPrice
        {
            get { return chanceorderpice; }
            set { SetProperty(ref chanceorderpice, value); }
        }

        private string chanceorderquantity;
        public string LChanceOrderQuantity
        {
            get { return chanceorderquantity; }
            set { SetProperty(ref chanceorderquantity, value); }
        }

        private string chanceordernotconclusion;
        public string LChanceOrderNotConclusion
        {
            get { return chanceordernotconclusion; }
            set { SetProperty(ref chanceordernotconclusion, value); }
        }
        private string chancemarketname;
        public string LChanceMarketName
        {
            get { return chancemarketname; }
            set { SetProperty(ref chancemarketname, value); }
        }

        private string chanceorderbtncancel;
        public string LChanceOrderBtnCancel
        {
            get { return chanceorderbtncancel; }
            set { SetProperty(ref chanceorderbtncancel, value); }
        }
    }
}
