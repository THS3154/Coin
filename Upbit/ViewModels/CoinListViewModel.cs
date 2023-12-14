using Language;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using Upbit.Event;
using Upbit.Functions;
using Upbit.Functions.Interface;
using Upbit.UpbitFunctions;
using Upbit.Views;
using System.Resources;
using PublicColor;
using System.Windows.Media;
using System.Diagnostics;

namespace Upbit.ViewModels
{

    public static class DispatcherService
    {
        public static void Invoke(Action action)
        {
            try
            {
                Dispatcher dispatchObject = Application.Current != null ? Application.Current.Dispatcher : null;
                if (dispatchObject == null || dispatchObject.CheckAccess())
                    action();
                else
                    dispatchObject.Invoke(action);
            }
            catch (Exception ex)
            {

            }
            
        }
    }
    public class CoinListViewModel : BindableBase,IWebSocketTicker, ILanguage, IColors
    {

        private PublicFunctions Upbitpf = new PublicFunctions();
        
        private WebSocketTicker wsc;

        #region Colors
        public void EventUpdateColor()
        {
            MyColors.ColorText = PublicColor.Colors.Colorinstance.ColorText;
            MyColors.ColorBack = PublicColor.Colors.Colorinstance.ColorBack;
            MyColors.ColorBid = PublicColor.Colors.Colorinstance.ColorBid;
            mycolors.ColorAsk = PublicColor.Colors.Colorinstance.ColorAsk;
        }

        private PublicColor.Colors mycolors;
        public PublicColor.Colors MyColors
        {
            get {  if (mycolors is null) mycolors = new PublicColor.Colors();
                return mycolors; }
            set { SetProperty(ref mycolors, value); }
        }
        #endregion Colors
        #region Models
        private string selectmartket;
        public string SelectMarket
        {
            get { return selectmartket; }
            set { SetProperty(ref selectmartket, value); }
        }

        private CollectionViewSource viewmarket { get; set; }

        public ICollectionView ViewMarket
        {
            get { return viewmarket.View;}
        }

        private Timer refreshTimer;

        /// <summary>
        /// 모든 마켓 정보들
        /// </summary>
        private List<Structs.MarketCodes> markets;
        public List<Structs.MarketCodes> Markets
        {
            get { return markets; }
            set { SetProperty(ref markets, value); }
        }

        private bool namevisibility;
        public bool NameVisiblity
        {
            get { return namevisibility; }
            set { SetProperty(ref namevisibility, value); }
        }
        #endregion Models

        #region Lang

        private Language.Lang.PublicCoin lang;
        public Language.Lang.PublicCoin Lang
        {
            get { if (lang is null) lang = new Language.Lang.PublicCoin(); return lang; }
            set { SetProperty(ref lang, value); }
        }

        public virtual void SetLanguage()
        {
            Lang.UpdateLang();
            if ("kr" == Language.Language.Lang.LangType)
                NameVisiblity = true;
            else
                NameVisiblity = false;

            UpdatePrintMarket();
        }
        #endregion Lang

        #region Prism
        private IEventAggregator _ea;
        #endregion Prism

        #region Event
        /// <summary>
        /// 코인 선택 이벤트
        /// </summary>
        private DelegateCommand<object> _sendcoin;
        public DelegateCommand<object> CommandSendCoin =>
            _sendcoin ?? (_sendcoin = new DelegateCommand<object>(ExecuteCommandSendCoin));

        void ExecuteCommandSendCoin(object obj)
        {
            if (obj is null) return;
            Structs.MarketCodes marketCodes = (Structs.MarketCodes)obj;

            _ea.GetEvent<MessageCoinNameEvent>().Publish(marketCodes);
           
        }

        private DelegateCommand<object> selectcurrency;
        public DelegateCommand<object> SelectCurrencyCommand =>
            selectcurrency ?? (selectcurrency = new DelegateCommand<object>(ExecuteSelectCurrency));

        void ExecuteSelectCurrency(object obj)
        {
            ComboBoxItem typeItem = (ComboBoxItem)((ComboBox)obj).SelectedItem;
            SelectMarket = typeItem.Content.ToString();

            UpdatePrintMarket();

            //Markets.ForEach(x => x.Print_name = NameVisiblity ? x.Korean_name : x.English_name) ;
            if (SelectMarket == "ALL")
                SelectMarket = "";
            CoinFilter = SelectMarket;

        }
        #endregion Event

        #region Functions
        private void UpdatePrintMarket()
        {
            if(Markets is not null)
            {
                for (int i = 0; i < Markets.Count; i++)
                {
                    Markets[i].Print_name = NameVisiblity ? Markets[i].Korean_name : Markets[i].English_name; 
                }
                OnFilterChange();
            }
            
        }
        /// <summary>
        /// 등락율 갱신 1초마다 변경
        /// </summary>
        private void RefreshTimer()
        {
            //기본 1초
            refreshTimer = new Timer(1000);
            refreshTimer.AutoReset = true;
            refreshTimer.Elapsed += (sender, e) =>
            {
                DispatcherService.Invoke(() =>
                {
                    viewmarket.View.Refresh();
                });
            };

            refreshTimer.Start();
        }
        #endregion Functions

        #region Interface
        public virtual void TradeEvent(Structs.Trade tr)
        {
            throw new NotImplementedException();
        }

        public virtual void OrderEvent(Structs.Orderbook ob)
        {
            throw new NotImplementedException();
        }

        public virtual void TickerEvent(Structs.Ticker tick)
        {
            try
            {
                Structs.Ticker ticker = tick;

                double rate = Math.Round(ticker.cr * 100, 2);
                string c = ticker.c;
                if (c == "FALL")
                    rate *= -1.0f;
                for (int i = 0; i < Markets.Count; i++)
                {
                    if (Markets[i].Market == ticker.cd)
                    {
                        Markets[i].Rate = rate;
                        Markets[i].Color = rate >= 0 ? MyColors.ColorBid : MyColors.ColorAsk;
                        break;
                    }

                }
            }
            catch(Exception ex) 
            {
                Debug.WriteLine($"CoinListView TradeEvent : {ex.ToString()}");
            }
            


        }
        #endregion Interface

        #region Filter
        private string coinfilter;
        public string CoinFilter
        {
            get { return coinfilter; }
            set { 
                SetProperty(ref coinfilter, value);
                OnFilterChange();
            }
        }
        private void ApplyFilter(object sender, FilterEventArgs e)
        {
            Structs.MarketCodes mc = (Structs.MarketCodes)e.Item;
            
            if (string.IsNullOrWhiteSpace(this.CoinFilter) || this.CoinFilter.Length == 0)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = mc.Market.Split("-")[0].Contains(CoinFilter);
            }

        }

        private void OnFilterChange()
        {
            if(viewmarket.View is not null)
                viewmarket.View.Refresh();
        }
        #endregion



        public CoinListViewModel(IEventAggregator ea)
        {
            //언어 변경됐을경우 이벤트호출
            Language.Language.LangChangeEvent += (SetLanguage);
            SetLanguage();

            

            _ea = ea;
            Markets = Task.Run(() => Upbitpf.MarketsAsync()).Result;
            viewmarket = new CollectionViewSource();
            viewmarket.Source = this.Markets;
            viewmarket.Filter += ApplyFilter;

            //색변경 이벤트 등록
            PublicColor.Colors.ColorUpdate += (EventUpdateColor);

            UpdatePrintMarket();
            //Markets.ForEach(x => PrintMarkets.Add(x));
            


            List<string> TickList = new List<string>();
            foreach (var coin in Markets)
            {
                coin.Color = MyColors.ColorText;
                TickList.Add(coin.Market);
            }
            wsc = new WebSocketTicker(TickList);
            WebSocketTicker.TickerEvent += new WebSocketTicker.TickEventHandler(TickerEvent);

            

            RefreshTimer();
        }

        ~CoinListViewModel()
        {
            wsc.CloseWebsocket();
            WebSocketTicker.TickerEvent -= new WebSocketTicker.TickEventHandler(TickerEvent);
        }
    }
}
