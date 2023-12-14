using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chart
{
    public struct line
    {
        public double FromX { get; set; }
        public double FromY { get; set; }
        public double ToX { get; set; }
        public double ToY { get; set; }
        public Brush LineColor { get; set; }
        public double LineThickness { get; set; }
    }
    public class CChart : UserControl, INotifyPropertyChanged
    {
        #region PropertyChange
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion PropertyChange

        #region 의존성 속성
        //값 상승 컬러
        public static readonly DependencyProperty TickUpColorProperty = DependencyProperty.Register("TickUpColor", typeof(Brush), typeof(CChart), new PropertyMetadata(Brushes.Red));

        //값 다운 컬러
        public static readonly DependencyProperty TickDownColorProperty = DependencyProperty.Register("TickDownColor", typeof(Brush), typeof(CChart), new PropertyMetadata(Brushes.Blue));

        //패널 배경값
        public static readonly DependencyProperty BackColorProperty = DependencyProperty.Register("BackColor", typeof(Brush), typeof(CChart), new PropertyMetadata(Brushes.Gray));

        //데이터값
        public static readonly DependencyProperty TickValueProperty = DependencyProperty.Register("TickValue", typeof(List<Structs.Tick>), typeof(CChart), new PropertyMetadata(new List<Structs.Tick>()));

        //X축 텍스트
        public static readonly DependencyProperty AxisXValueProperty = DependencyProperty.Register("AxisXValue", typeof(string), typeof(CChart), new PropertyMetadata(string.Empty));

        //Y축 텍스트
        public static readonly DependencyProperty AxisYValueProperty = DependencyProperty.Register("AxisYValue", typeof(string), typeof(CChart), new PropertyMetadata(string.Empty));


        public List<Structs.Tick> TickValue
        {
            get { return (List<Structs.Tick>)GetValue(TickValueProperty); }
            set { SetValue(TickValueProperty, value);}
        }

        public Brush TickUpColor
        {
            get { return (Brush)GetValue(TickUpColorProperty); }
            set { SetValue(TickUpColorProperty, value);}
        }

        public Brush TickDownColor
        {
            get { return (Brush)GetValue(TickDownColorProperty); }
            set { SetValue(TickDownColorProperty, value); }
        }

        public Brush BackColor
        {
            get { return (Brush)GetValue(BackColorProperty); }
            set { SetValue(BackColorProperty, value); }
        }

        public string AxisXValue
        {
            get { return (string)GetValue(AxisXValueProperty); }
            set { SetValue(AxisXValueProperty, value); }
        }

        public string AxisYValue
        {
            get { return (string)GetValue(AxisYValueProperty); }
            set { SetValue(AxisYValueProperty, value); }
        }

        
        #endregion 의존성 속성
        #region Model
        private List<line> horline;
        public List<line> HorLine
        {
            get { return horline; }
            set { horline = value;
                OnPropertyChanged("HorLine");
            }
        }

        private List<line> verline;
        public List<line> VerLine
        {
            get { return verline; }
            set { verline = value;
                OnPropertyChanged("MyCanvas");
            }
        }
        private Canvas mycanvas;
        public Canvas MyCanvas
        {
            get { return mycanvas; }
            set
            {
                mycanvas = value;
                OnPropertyChanged("HorLine");
            }
        }
        #endregion
        #region

        #endregion

        #region Event
        private DelegateCommand<string> _fieldName;
        public DelegateCommand<string> CommandName =>
            _fieldName ?? (_fieldName = new DelegateCommand<string>(ExecuteCommandName));

        void ExecuteCommandName(string parameter)
        {

        }


        public void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }
        public void Canvas_Loaded(object sender, RoutedEventArgs e)
        {

        }
        #endregion Event

        public CChart()
        {
            DataContext = this;
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CChart), new FrameworkPropertyMetadata(typeof(CChart)));
        }



    }
}
