
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;

namespace PasswordPlaceHolder
{
    public class CPasswordPlaceHolder : UserControl, INotifyPropertyChanged
    {
        #region 의존성 속성
        public static readonly DependencyProperty PlaceHolderProperty = DependencyProperty.Register("PlaceHolder", typeof(string), typeof(CPasswordPlaceHolder), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty TextValueProperty = DependencyProperty.Register("TextValue", typeof(string), typeof(CPasswordPlaceHolder), new PropertyMetadata(string.Empty));

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
            }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        static CPasswordPlaceHolder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CPasswordPlaceHolder), new FrameworkPropertyMetadata(typeof(CPasswordPlaceHolder)));
        }
    }
}