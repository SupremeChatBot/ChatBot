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

namespace ChatBot.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ConversationViewModel ConversationVM { get; set; }
        public ObservableCollection<RadioButton> Conversations { get; set; }
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
        }
        private void InitializeObjects()
        {
            Conversations = new ObservableCollection<RadioButton>();
            ConversationVM = new ConversationViewModel();
            CurrentView = ConversationVM;
        }
        private void LoadConversations()
        {
            List<string> contents = new List<string>()
            {
                "Healthcare","Politics","Education","Science",
            };
            int count = 1;
            foreach(string content in contents)
            {                
                Conversations.Add(new RadioButton()
                {
                    Name = $"ConversationItem{count}",
                    Content = content,
                    Height = 50,
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 14,
                    Style = GetRadioButtonStyle(),                    
                }) ;
            }
            
        }       
        private Style GetRadioButtonStyle()
        {
            return Application.Current.FindResource("ConversationItemTheme") as Style;
        }       
    }
}
