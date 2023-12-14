using Language;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Resources;
using Prism.Services.Dialogs;
using FileIO;
using System.Windows.Controls;
using Prism.Common;
using System.Collections.ObjectModel;
using System.Numerics;
using Language.Lang;
using System.Windows.Media;
using System.Drawing;
using PublicColor.View;

namespace Coins.ViewModels
{

    public struct StructMenu
    {
        public string Header { get; set; }
        public ICommand Command { get; set; }
        public bool IsEnable { get; set; }
        public Visibility IsVisible { get; set; }

        public bool IsChecked { get; set; }
        public List<StructMenu> ChildMenuItems { get; set; }

        public StructMenu()
        {
            Header = string.Empty;
            Command = null;
            IsEnable = true;
            IsVisible = Visibility.Visible;
            IsChecked = false;
            ChildMenuItems = new List<StructMenu>();
        }
    }

    public class MenusViewModel : BindableBase, ILanguage
    {

        #region Models
        private ObservableCollection<StructMenu> accessmenu;
        public ObservableCollection<StructMenu> AccessMenu
        {
            get { return accessmenu; }
            set { SetProperty(ref accessmenu, value); }
        }

        private ObservableCollection<StructMenu> settingmenu;
        public ObservableCollection<StructMenu> SettingMenu
        {
            get { return settingmenu; }
            set { SetProperty(ref settingmenu, value); }
        }
        #endregion Models

        #region Lang
        private Language.Lang.Menu lang;
        public Language.Lang.Menu Lang
        {
            get { if (lang is null) lang = new Language.Lang.Menu(); return lang; }
            set { SetProperty(ref lang, value); }
        }
        public virtual void SetLanguage()
        {
            Lang.UpdateLang();
            CreateMenu();
        }
        #endregion

        #region Prism
        IDialogService Dialog;
        #endregion EndPrism

        /// <summary>
        /// 메뉴 생성
        /// </summary>
        private void CreateMenu()
        {
            AccessMenu = new ObservableCollection<StructMenu>();
            StructMenu AccessMenu_Upbit = new StructMenu();
            AccessMenu_Upbit.Header = Lang.LUpbitAccess;
            AccessMenu_Upbit.Command = new DelegateCommand(ShowUpbitAccess);

            SettingMenu = new ObservableCollection<StructMenu>();
            StructMenu LangMenu = new StructMenu();
            LangMenu.Header = Lang.LLang;
            foreach (var lang in Language.Language.Lang.Langs)
            {
                StructMenu menu = new StructMenu();
                menu.Header = lang.ToUpper();
                menu.Command = new DelegateCommand<object>(ChangeLang);
                if(Language.Language.Lang.LangType == lang)
                    menu.IsChecked = true;
                LangMenu.ChildMenuItems.Add(menu);
            }

            StructMenu ColorMenu = new StructMenu();
            ColorMenu.Header = Lang.LColor;
            ColorMenu.Command = new DelegateCommand(ColorChangeTest);
            SettingMenu.Add(LangMenu);
            SettingMenu.Add(ColorMenu);
            AccessMenu.Add(AccessMenu_Upbit);
        }

        /// <summary>
        /// 언어 변경
        /// </summary>
        /// <param name="obj"></param>
        private void ChangeLang(object obj)
        {
            if(obj is StructMenu?)
            {
                StructMenu structLangMenu = (StructMenu)obj;

                //이전과 선택된 메뉴가 달라졌을경우 언어 변경
                if (Language.Language.Lang.LangType != structLangMenu.Header.ToLower())
                    Language.Language.Lang.SetLang(structLangMenu.Header.ToLower());

            }
        }
        private void ShowUpbitAccess()
        {
            Dialog.ShowDialog("DialogAccess");
        }

        private void ColorChangeTest()
        {
            //PublicColor.Colors.Colorinstance.SetColor(ref PublicColor.Colors.Colorinstance.ColorBack, "FF0000");
            Type view = typeof(ColorView);
            Dialog.ShowDialog("DialogSettings", new DialogParameters($"type={view}"), r =>
            {
                /*
                if (r.Result == ButtonResult.None)
                    Title = "Result is None";
                else if (r.Result == ButtonResult.OK)
                    Title = "Result is OK";
                else if (r.Result == ButtonResult.Cancel)
                    Title = "Result is Cancel";
                else
                    Title = "I Don't know what you did!?";
                */
            });
            //PublicColor.Colors.Colorinstance.back = "FF0000";
            //PublicColor.Colors.Colorinstance.SetColor();
        }
        public MenusViewModel(IDialogService dialog)
        {
            //언어등록
            SetLanguage();

            Dialog = dialog;

            //언어 변경 이벤트추가
            Language.Language.LangChangeEvent += new Language.Language.LangEventHandler(SetLanguage);


        }
    }
}
