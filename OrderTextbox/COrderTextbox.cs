using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace OrderTextbox
{
    public class COrderTextbox : UserControl, INotifyPropertyChanged
    {
        #region 의존성 속성
        public static readonly DependencyProperty TextboxEnableProperty = DependencyProperty.Register("TextboxEnable", typeof(bool), typeof(COrderTextbox), new PropertyMetadata(true));
        public static readonly DependencyProperty PrefixProperty = DependencyProperty.Register("Prefix", typeof(string), typeof(COrderTextbox), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty SubffixProperty = DependencyProperty.Register("Subffix", typeof(string), typeof(COrderTextbox), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty TextValueProperty = DependencyProperty.Register("TextValue", typeof(string), typeof(COrderTextbox), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty BorderColorValueProperty = DependencyProperty.Register("BorderColor", typeof(Brush), typeof(COrderTextbox), new PropertyMetadata(Brushes.Transparent));
        public static readonly DependencyProperty BackColorValueProperty = DependencyProperty.Register("BackColor", typeof(Brush), typeof(COrderTextbox), new PropertyMetadata(Brushes.Transparent));
        public static readonly DependencyProperty TextColorValueProperty = DependencyProperty.Register("TextColor", typeof(Brush), typeof(COrderTextbox), new PropertyMetadata(Brushes.Black));

        public bool TextboxEnable
        {
            get
            {
                return (bool)GetValue(TextValueProperty);
            }
            set
            {
                SetValue(TextValueProperty, value);
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
        public string Prefix
        {
            get { return (string)GetValue(PrefixProperty); }
            set
            {
                SetValue(PrefixProperty, value);
            }

        }

        public string Subffix
        {
            get
            {
                return (string)GetValue(SubffixProperty);
            }
            set
            {
                SetValue(SubffixProperty, value);
            }
        }

        public Brush BorderColor
        {
            get
            {
                return (Brush)GetValue(BorderColorValueProperty);
            }
            set
            {
                SetValue(BorderColorValueProperty, value);
            }
        }

        public Brush BackColor
        {
            get
            {
                return (Brush)GetValue(BackColorValueProperty);
            }
            set
            {
                SetValue(BackColorValueProperty, value);
            }
        }
        public Brush TextColor
        {
            get
            {
                return (Brush)GetValue(TextColorValueProperty);
            }
            set
            {
                SetValue(TextColorValueProperty, value);
            }
        }
        /*
        public string Subffix
        {
            get
            {
                return (string)GetValue(SubffixProperty);
            }
            set
            {
                SetValue(SubffixProperty, value);
            }
        }
        public string Subffix
        {
            get
            {
                return (string)GetValue(SubffixProperty);
            }
            set
            {
                SetValue(SubffixProperty, value);
            }
        }
        */
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
        static COrderTextbox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(COrderTextbox), new FrameworkPropertyMetadata(typeof(COrderTextbox)));
        }

    }
}
