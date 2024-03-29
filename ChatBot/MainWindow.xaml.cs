using ChatBot.Components;
using ChatBot.ViewModel;
using ChatBot.Windows;
using ChatBot_Repo.EventAggregator;
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
        private NewConversationDetailsWindow _choosePersonaWindow;
        private IGeminiService _googleGeminiService;
        private NewConversationDetailsViewModel _newConversationDetailsViewModel; 
        private IEventAggregator _eventAggregator;
        public MainWindow(IGeminiService googleGeminiService,
            IEventAggregator eventAggregator,
            NewConversationDetailsViewModel newConversationDetailsViewModel)
        {
            _newConversationDetailsViewModel = newConversationDetailsViewModel;
            _googleGeminiService = googleGeminiService;            
            _eventAggregator = eventAggregator; 
            InitializeComponent();
            InitializeObjects();
            BindMsgTextBoxToScrollBottomEvent();
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
    
        private void InitializeObjects()
        {            
            DataContext = new MainViewModel(_googleGeminiService,_eventAggregator);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                SendMessage();
                msgTextbox.ClearTextbox();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, error.StackTrace);
            }
        }
        private void MsgTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            SendMessage();
        }
        private void BindMsgTextBoxToScrollBottomEvent()
        {
            msgTextbox.TextBoxKeyDown += MsgTextBox_KeyDown;
        }
        private void SendMessage()
        {
            MessageScrollViewer.ScrollToBottom();
            var mainViewModel = DataContext as MainViewModel;
            mainViewModel!.SendMessage();
        }

        private void ConversationListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mainViewModel = DataContext as MainViewModel;
            int selectedIndex = ConversationListView.SelectedIndex;
            mainViewModel!.ShowMessagesBySelectedConversation(selectedIndex);
        }

        private void NewChatButton_Click(object sender, RoutedEventArgs e)
        {
            _choosePersonaWindow = new NewConversationDetailsWindow(_newConversationDetailsViewModel);
            _choosePersonaWindow.Show();
            ConversationScrollViewer.ScrollToBottom();
        }
       
    }
}