using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using Upbit.UpbitFunctions;
using Language;

namespace Upbit.ViewModels
{
    public class AddAccessViewModel : BindableBase , ILanguage, IDialogAware
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
        private string _title = "키등록";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        

        #region Models
        private string _message;

        private string accesskey;
        public string AccessKey
        {
            get { return accesskey; }
            set { SetProperty(ref accesskey, value); }
        }
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string secretkey;
        public string SecretKey
        {
            get { return secretkey; }
            set { SetProperty(ref secretkey, value); }
        }


        #endregion Models   

        #region Prism
        private IEventAggregator _ea;

        #endregion Prism

        #region Event
        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand =>
            _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog));

        public event Action<IDialogResult> RequestClose;
        #endregion Event

        #region Function

        #endregion Function

        #region Interface
        protected virtual void CloseDialog(string parameter)
        {
            Access.UpbitInstance.SetKeys(AccessKey, SecretKey);

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

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            //Message = parameters.GetValue<string>("message");
        }
        #endregion Interface


        public AddAccessViewModel(IEventAggregator ea)
        {
            //언어 설정
            SetLanguage();
            _ea = ea;
            Access.UpbitInstance.LoadKey();
            AccessKey = Access.UpbitInstance.GetAccessKey();
            SecretKey = Access.UpbitInstance.GetSecretKey();
        }


    }
}
