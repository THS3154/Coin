using System.Windows;
using System.Windows.Input;

namespace Coins.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isResizing = false;
        private Point resizeStartPoint;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AdjustMaxHeight();
        }

        private void AdjustMaxHeight()
        {
            // 작업 표시줄의 높이 가져오기
            double taskbarHeight = SystemParameters.PrimaryScreenHeight - SystemParameters.WorkArea.Height - 12;

            // 창의 MaxHeight 설정
            this.MaxHeight = SystemParameters.PrimaryScreenHeight - taskbarHeight;
        }
    }
}
