using ChatBot_Repo.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Input;
namespace ChatBot.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {        
        public ConversationViewModel ConversationVM { get; set; }
        public ObservableCollection<ToggleButton> Conversations { get; set; }
        public bool IsButtonChecked
        {
            get { return true; }
            set
            {
                if (true)
                {
                    this.ResetRemainingConversationItemsColor();
                }
            }
        }
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            InitializeObjects();
            LoadConversations();
            //ConversationCommand = new RelayCommand(ResetRemainingConversationItemsColor);
        }
        
        private void ResetRemainingConversationItemsColor()
        {
            var bc = new BrushConverter();
            string conversationName = "";
            foreach (ToggleButton conversation in Conversations) {
                if (conversation.Name != conversationName.ToString())
                {
                    conversation.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
                    conversation.Background= (Brush)bc.ConvertFrom("#7289da");
                }
            }
        }
        private void InitializeObjects()
        {
            Conversations = new ObservableCollection<ToggleButton>();
            ConversationVM = new ConversationViewModel();            
            CurrentView = ConversationVM;
        }
        private void LoadConversations()
        {
            List<string> contents = new List<string>()
            {
                "Healthcare","Politics","Education","Science", "This is a super fucking long text",
                "Healthcare","Politics","Education","Science", "This is a super fucking long text"
            };
            int count = 1;
            foreach(string content in contents)
            {                
                Conversations.Add(new ToggleButton()
                {
                    Name = $"ConversationItem{count}",
                    Content = content,                                                          
                    BorderThickness = new Thickness(0),                                                            
                }) ;
            }            
        }    
        
    }
}
