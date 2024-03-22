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
using ChatBot_Repo.Services;
using ChatBot_Repo.Services.Implementation;
using ChatBot_Repo.Entities;
namespace ChatBot.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ObservableCollection<ConversationItemModel> Conversations { get; set; }
        public ObservableCollection<MessageItemModel> MessageItems { get; set; }
        public bool IsButtonChecked
        {
            get { return true; }
            set
            {
                if (true)
                {
                    //this.ResetRemainingConversationItemsColor();
                }
            }
        }
        private string _request;
        public string Request
        {
            get { return _request; }
            set
            {
                _request = value;
                OnPropertyChanged();
            }
        }
        private string _response;
        private IGoogleGeminiService _googleGeminiService;
        public MainViewModel(IGoogleGeminiService googleGeminiService)
        {
            _googleGeminiService = googleGeminiService;
            InitializeObjects();
            LoadConversations();
        }
        public async void SendMessage()
        {
            if (_request == null) return;
            _request = await AddRequestToMessagePanel();
            _response = await _googleGeminiService.Chat(_request);
            await AddResponseToMessageItems(_response);
        }
        public void ShowSelectedConversation(int index )
        {
            ConversationItemModel item = Conversations[index];  
            MessageBox.Show("Currently selected model: " + item.Name);
        }
        private async Task<string> AddRequestToMessagePanel()
        {

            var request = await GetRequestAsync();
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                MessageItems.Add(new MessageItemModel()
                {
                    Content = request,
                    ImageUrl = "https://i.pinimg.com/564x/a5/26/64/a526644653e3aa32e9164430ce66b304.jpg",
                    Sender = "User"
                });
            });
            return request;

        }
        private Task<string> GetRequestAsync()
        {
            return Task.Run(() =>
            {
                while (Request.Length == 0)
                {
                    Task.Delay(100);
                }
                return Request;
            });
        }
        private Task AddResponseToMessageItems(string message)
        {
            return Application.Current.Dispatcher.InvokeAsync(() =>
            {
                MessageItems.Add(new MessageItemModel()
                {
                    Content = message,
                    ImageUrl = "https://i.pinimg.com/564x/a5/26/64/a526644653e3aa32e9164430ce66b304.jpg",
                    Sender = "Gemini"
                });
            }).Task;
        }
        
        //private void ResetRemainingConversationItemsColor()
        //{
        //    var bc = new BrushConverter();
        //    string conversationName = "";
        //    foreach (ToggleButton conversation in Conversations)
        //    {
        //        if (conversation.Name != conversationName.ToString())
        //        {
        //            conversation.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
        //            conversation.Background = (Brush)bc.ConvertFrom("#7289da");
        //        }
        //    }
        //}
        private void InitializeObjects()
        {
            Conversations = new ObservableCollection<ConversationItemModel>();
            MessageItems = new ObservableCollection<MessageItemModel>();
        }
        private void LoadConversations()
        {
            List<string> conversationNames = new List<string>()
            {
                "Healthcare","Politics","Education","Science", "This is a super fucking long text",
                "Healthcare","Politics","Education","Science", "This is a super fucking long text"
            };
            int count = 1;
            foreach (string conversationName in conversationNames)
            {
                Conversations.Add(new ConversationItemModel()
                {
                    Name = conversationName,
                    
                });
            }
        }

    }
}
