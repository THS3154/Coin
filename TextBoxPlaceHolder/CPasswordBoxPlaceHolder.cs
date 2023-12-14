using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;

namespace TextBoxPlaceHolder
{
    public class CPasswordBoxPlaceHolder : UserControl, INotifyPropertyChanged
    {
        #region 의존성 속성
        public static readonly DependencyProperty PlaceHolderProperty = DependencyProperty.Register("PlaceHolder", typeof(string), typeof(CPasswordBoxPlaceHolder), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty TextValueProperty = DependencyProperty.Register("TextValue", typeof(string), typeof(CPasswordBoxPlaceHolder), new PropertyMetadata(string.Empty));

        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set
            {
                SetValue(PlaceHolderProperty, value);
            }

        }

        public string TextValue
        {
            get
            {
                return (string)GetValue(TextValueProperty);
            }
            set
            {
                SetValue(TextValueProperty, value);
                OnPropertyChanged(nameof(TextValue));
            }
        }

        #endregion
        #region IsPasswordBindable

        public static bool GetIsPasswordBindable(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPasswordBindableProperty);
        }

        public static void SetIsPasswordBindable(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPasswordBindableProperty, value);
        }

        public static readonly DependencyProperty IsPasswordBindableProperty =
            DependencyProperty.RegisterAttached("IsPasswordBindable", typeof(bool), typeof(CPasswordBoxPlaceHolder), new PropertyMetadata(false, IsPasswordBindableChanged));

        private static void IsPasswordBindableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is PasswordBox passwordBox)) return;

            if ((bool)e.NewValue)
            {
                passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
            else
            {
                passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
            }
        }
        #endregion


        #region BindablePassword

        public static string GetBindablePassword(DependencyObject obj)
        {
            return (string)obj.GetValue(BindablePasswordProperty);
        }

        public static void SetBindablePassword(DependencyObject obj, string value)
        {
            obj.SetValue(BindablePasswordProperty, value);
        }

        public static readonly DependencyProperty BindablePasswordProperty =
            DependencyProperty.RegisterAttached("BindablePassword", typeof(string), typeof(CPasswordBoxPlaceHolder), new PropertyMetadata(string.Empty));

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            SetBindablePassword(passwordBox, passwordBox.Password);
        }

        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _text;

        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                _text = value;  
            }
        }
        



        static CPasswordBoxPlaceHolder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CPasswordBoxPlaceHolder), new FrameworkPropertyMetadata(typeof(CPasswordBoxPlaceHolder)));
        }


    }
}
