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
using System.Diagnostics;
using ChatBot.MVVM.Model;
using ChatBot.Components;
using GenerativeAI.Types;
namespace ChatBot.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {        
        public ConversationViewModel ConversationVM { get; set; }
        public ObservableCollection<ToggleButton> Conversations { get; set; }
        public ObservableCollection<MessageItemModel> MessageItems { get; set; }
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
        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }
       

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        private object _currentView;

        public MainViewModel()
        {
            InitializeObjects();
            LoadConversations();
            SeedMessageItems();
        }
        public void SendMessage()
        {
            MessageItems.Add(new ()
            {                
                Content = Message,
                ImageUrl = "https://i.pinimg.com/564x/a5/26/64/a526644653e3aa32e9164430ce66b304.jpg",
                Sender = "User"
            });
        }
        private void SeedMessageItems()
        {
            List<string> contents = new()
            {
                "I love music too!", "Something is right"
            };
            int id = 0;
            foreach (var content in contents)
            {
                MessageItemModel msgItemModel = new MessageItemModel()
                {
                    Id = id,
                    Content = content,
                    ImageUrl = "https://i.pinimg.com/564x/a5/26/64/a526644653e3aa32e9164430ce66b304.jpg",
                    Sender = "User"

                };
                MessageItems.Add(msgItemModel);
            }
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
            MessageItems = new ObservableCollection<MessageItemModel>();
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
