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
using ChatBot_Repo.EventAggregator;
using System.Printing;
namespace ChatBot.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ObservableCollection<ConversationItemModel> Conversations { get; set; }
        public ObservableCollection<MessageItemModel> Messages { get; set; }
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
        private IEventAggregator _eventAggregator;
        private List<MessageItemModel> Messages1 = new List<MessageItemModel>()
        {
            new MessageItemModel() {Sender="Me",Content="Hello world!"},
            new MessageItemModel() {Sender="Gemini",Content="Hi there!"},
            new MessageItemModel() {Sender="Me",Content="I love my life!"},
            new MessageItemModel() {Sender="Gemini",Content="So do I!"},
        };
        private List<MessageItemModel> Messages2 = new List<MessageItemModel>()
        {
            new MessageItemModel() {Sender="Me",Content="Hello Gemini!"},
            new MessageItemModel() {Sender="Gemini",Content="Hi there, User!"},
            new MessageItemModel() {Sender="Me",Content="How's the weather today?"},
            new MessageItemModel() {Sender="Gemini",Content="Perfect for a morning ride!"},
        };


        public MainViewModel(IGoogleGeminiService googleGeminiService,IEventAggregator eventAggregator)
        {
            _googleGeminiService = googleGeminiService;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe<ConversationItemModel>(AddNewConversation);
            InitializeObjects();
            LoadConversations();
        }       
        public async void SendMessage()
        {
            if (_request == null) return;
            _request = await AddRequestToMessages();
            _response = await _googleGeminiService.Chat(_request);
            await AddResponseToMessages(_response);
        }
        public void ShowSelectedConversation(int index)
        {
            ConversationItemModel item = Conversations[index];
            //b2: Lấy Messages (nhiều cái Messages) dựa vào Id, nằm trong ConversationItemModel
            // Lấy bằng cách nào: tạo 1 service lấy Messages, v.d. _messageService.Get(int ConversationId)
            //b3: Sau khi lấy xong từ _messageService, thì nạp vô cái ObservableCollection<=
            Messages.Clear();
            if (index == 0)
            {
                foreach (var msg in Messages1)
                {
                    Messages.Add(new MessageItemModel()
                    {
                        Content = msg.Content,
                        ImageUrl = "https://i.pinimg.com/564x/a5/26/64/a526644653e3aa32e9164430ce66b304.jpg",
                        Sender = msg.Sender
                    });
                }
            }
            else
            {
                foreach (var msg in Messages2)
                {
                    Messages.Add(new MessageItemModel()
                    {
                        Content = msg.Content,
                        ImageUrl = "https://i.pinimg.com/564x/a5/26/64/a526644653e3aa32e9164430ce66b304.jpg",
                        Sender = msg.Sender
                    });
                }
            }
        }
        private void LoadConversations()
        {
            //B1. Lấy từ Db Lên: List<Conversation> = _conversationService.GetAll()
            //B2. Map từng Conversation Entity sang ConversationItemModel
            List<ConversationItemModel> conversations = new List<ConversationItemModel>() { 
                
                new ConversationItemModel()
                {
                    
                }    
            };
            //B3. Bốc từng thằng ConversationItemModel nạp vô
            //ObservableCollection<ConversationItemModel> (load ConversationItemModel lên UI.)

        }
        private async Task<string> AddRequestToMessages()
        {
            var request = await GetRequestAsync();
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Messages.Add(new MessageItemModel()
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
        private Task AddResponseToMessages(string message)
        {
            return Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Messages.Add(new MessageItemModel()
                {
                    Content = message,
                    ImageUrl = "https://i.pinimg.com/564x/a5/26/64/a526644653e3aa32e9164430ce66b304.jpg",
                    Sender = "Gemini"
                });
            }).Task;
        }
        
        private void InitializeObjects()
        {
            Conversations = new ObservableCollection<ConversationItemModel>();
            Messages = new ObservableCollection<MessageItemModel>();
        }       

        private void AddNewConversation(ConversationItemModel conversationItemModel)
        {
            Conversations.Add(conversationItemModel);
        }
    }
}
