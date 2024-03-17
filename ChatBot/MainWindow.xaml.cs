using ChatBot.MVVM.Model;
using ChatBot.MVVM.ViewModel;
using ChatBot.Windows;
using ChatBot_Repo.Services;
using System.Collections.ObjectModel;
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
        private ChoosePersonaWindow _choosePersonaWindow;        
        private IGoogleGeminiService _googleGeminiService;  
        public MainWindow(IGoogleGeminiService googleGeminiService)
        {
            _googleGeminiService = googleGeminiService;
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
            _choosePersonaWindow.Show();
        }

       
        private void InitializeObjects()
        {
            _choosePersonaWindow = new ChoosePersonaWindow();
            DataContext = new MainViewModel(_googleGeminiService);
        }
       
        private void input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                TextBox textBox = sender as TextBox;
                textBox.Clear();
            }
        }
    }
}