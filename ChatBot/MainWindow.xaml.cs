using ChatBot.MVVM.ViewModel;
using ChatBot.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatBot
{
    public partial class MainWindow : Window
    {

        private ChoosePersona _choosePersona;
        public MainWindow()
        {
            InitializeComponent();
            InitializeObjects();
        }
        private void headerThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void OpenChoosePersonaWindow(object sender, RoutedEventArgs e)
        {
            _choosePersona.Show();
        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void InitializeObjects()
        {
            _choosePersona = new ChoosePersona();
        }

    }
}