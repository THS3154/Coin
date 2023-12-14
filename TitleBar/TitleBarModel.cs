using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using Prism.Commands;

namespace TitleBar
{
    public class TitleBarModel : UserControl
    {
        //기본 타이틀바 넓이
        private static int TitleBarWidth = 50;
        private int iconWidth = 30;
        #region 의존성속성 값
        //배경색 및 오버됐을때 색상
        public static readonly DependencyProperty BackColorProperty = DependencyProperty.Register("BackColor", typeof(Brush), typeof(TitleBarModel), new PropertyMetadata(Brushes.Transparent));
        public static readonly DependencyProperty HoverBackColorProperty = DependencyProperty.Register("HoverBackColor", typeof(Brush), typeof(TitleBarModel), new PropertyMetadata(Brushes.Transparent));

        //타이틀바 텍스트 색상
        public static readonly DependencyProperty ContentColorProperty = DependencyProperty.Register("ContentColor", typeof(Brush), typeof(TitleBarModel), new PropertyMetadata(Brushes.Black));


        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(TitleBarModel), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty WindowFormProperty = DependencyProperty.Register("WindowForm", typeof(object), typeof(TitleBarModel), new PropertyMetadata(null));

        //종료 버튼
        public static readonly DependencyProperty CloseButtonProperty = DependencyProperty.Register("CloseButton", typeof(int), typeof(TitleBarModel), new PropertyMetadata(TitleBarWidth));

        //축소 최대화 버튼
        public static readonly DependencyProperty MinMaxButtonProperty = DependencyProperty.Register("MinMaxButton", typeof(int), typeof(TitleBarModel), new PropertyMetadata(TitleBarWidth));

        //최소화 버튼
        public static readonly DependencyProperty MinimizeButtonProperty = DependencyProperty.Register("MinimizeButton", typeof(int), typeof(TitleBarModel), new PropertyMetadata(TitleBarWidth));

        //타이틀 이름
        public static readonly DependencyProperty TitleNameProperty = DependencyProperty.Register("TitleName", typeof(string), typeof(TitleBarModel), new PropertyMetadata("WindowForm"));
        #endregion

        #region 각종 이벤트
        public ICommand CloseFormCommand { get; private set; }
        public ICommand MinMaxFormCommand { get; private set; }
        public ICommand MinimizeFormCommand { get; private set; }
        public ICommand DragFormCommand { get; private set; }
        #endregion

        #region 값들
        public Brush BackColor
        {
            get { return (Brush)GetValue(BackColorProperty); }
            set { SetValue(BackColorProperty, value); }
        }

        public Brush HoverBackColor
        {
            get { return (Brush)GetValue(HoverBackColorProperty); }
            set { SetValue(HoverBackColorProperty, value); }
        }

        public Brush ContentColor
        {
            get { return (Brush)GetValue(ContentColorProperty); }
            set { SetValue(ContentColorProperty, value); }
        }
        public int IconWidth
        {
            get
            {
                if (Icon == null || Icon == "")
                    return 0;
                else
                    return iconWidth;
            }
        }
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public string TitleName
        {
            get { return (string)GetValue(TitleNameProperty); }
            set { SetValue(TitleNameProperty, value); }
        }
        public object WindowForm
        {
            get { return (object)GetValue(WindowFormProperty); }
            set { SetValue(WindowFormProperty, value); }
        }

        public int CloseButton
        {
            get { return (int)GetValue(CloseButtonProperty); }
            set { SetValue(CloseButtonProperty, value); }
        }

        public int MinMaxButton
        {
            get { return (int)GetValue(MinMaxButtonProperty); }
            set { SetValue(MinMaxButtonProperty, value); }
        }

        public int MinimizeButton
        {
            get { return (int)GetValue(MinimizeButtonProperty); }
            set { SetValue(MinimizeButtonProperty, value); }
        }

        #endregion

        public TitleBarModel()
        {
            CloseFormCommand = new DelegateCommand(CloseForm);
            MinMaxFormCommand = new DelegateCommand(MinMaxForm);
            MinimizeFormCommand = new DelegateCommand(MinimizeForm);
            DragFormCommand = new DelegateCommand(DragForm);

        }

        /// <summary>
        /// 드래그 이벤트
        /// </summary>
        /// <param name="parameter"></param>
        public void DragForm()
        {
            if (WindowForm is Window windowForm)
            {
                Window w = ((Window)WindowForm);
                if (w.WindowState == WindowState.Maximized)
                {
                    //w.WindowState = WindowState.Normal;

                }
                w.DragMove();
            }
        }

        /// <summary>
        /// 폼 종료 이벤트
        /// </summary>
        /// <param name="parameter"></param>
        public void CloseForm()
        {
            if (WindowForm is Window windowForm)
            {
                ((Window)WindowForm).Close();
            }
        }

        /// <summary>
        /// 축소 및 확대 이벤트
        /// </summary>
        /// <param name="parameter"></param>
        public void MinMaxForm()
        {
            if (WindowForm is Window windowForm)
            {
                Window w = ((Window)WindowForm);
                //최대화
                if (w.WindowState == WindowState.Normal)
                    w.WindowState = WindowState.Maximized;
                else
                    w.WindowState = WindowState.Normal;
            }
        }

        /// <summary>
        /// 최소화 이벤트
        /// </summary>
        /// <param name="parameter"></param>
        public void MinimizeForm()
        {
            if (WindowForm is Window windowForm)
            {
                Window w = ((Window)WindowForm);
                w.WindowState = WindowState.Minimized;
            }
        }
    }
}