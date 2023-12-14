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
    public class CTextBoxPlaceHolder : UserControl, INotifyPropertyChanged
    {
        #region 의존성 속성
        public static readonly DependencyProperty PlaceHolderProperty = DependencyProperty.Register("PlaceHolder", typeof(string), typeof(CTextBoxPlaceHolder), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty TextValueProperty = DependencyProperty.Register("TextValue", typeof(string), typeof(CTextBoxPlaceHolder), new PropertyMetadata(string.Empty));

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
        



        static CTextBoxPlaceHolder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CTextBoxPlaceHolder), new FrameworkPropertyMetadata(typeof(CTextBoxPlaceHolder)));
        }

        private void Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            ColorAnimation colorAnimation = new ColorAnimation
            {
                To = (Color)ColorConverter.ConvertFromString("#ddd"),
                Duration = TimeSpan.FromSeconds(1)
            };

            // 애니메이션 적용 대상 설정
            //Storyboard.SetTarget(colorAnimation, rectangle);
            Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("(Rectangle.Fill).(SolidColorBrush.Color)"));

            // 스토리보드 생성 및 애니메이션 추가
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(colorAnimation);

            // 애니메이션 실행
            storyboard.Begin();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
