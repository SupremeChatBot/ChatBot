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
using ChatBot.Components;
using GenerativeAI.Types;
using ChatBot_Repo.Services;
using ChatBot_Repo.Services.Implementation;
using ChatBot_Repo.Entities;
using ChatBot_Repo.EventAggregator;
using System.Printing;
using ChatBot_Repo.Payload.Response;
namespace ChatBot.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ObservableCollection<ConversationItemDTO> Conversations { get; set; }
        public ObservableCollection<MessageItemDTO> Messages { get; set; }
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
        private IEventAggregator _eventAggregator;
        private readonly IGeminiService _geminiService;
        private ConversationItemDTO _selectedConversationItemModel;
        private List<MessageItemDTO> Messages1 = new List<MessageItemDTO>()
        {
            new MessageItemDTO() {Sender="Me",Content="Hello world!"},
            new MessageItemDTO() {Sender="Gemini",Content="Hi there!"},
            new MessageItemDTO() {Sender="Me",Content="I love my life!"},
            new MessageItemDTO() {Sender="Gemini",Content="So do I!"},
        };
        private List<MessageItemDTO> Messages2 = new List<MessageItemDTO>()
        {
            new MessageItemDTO() {Sender="Me",Content="Hello Gemini!"},
            new MessageItemDTO() {Sender="Gemini",Content="Hi there, User!"},
            new MessageItemDTO() {Sender="Me",Content="How's the weather today?"},
            new MessageItemDTO() {Sender="Gemini",Content="Perfect for a morning ride!"},
        };

        public MainViewModel(IGeminiService geminiService,
            IEventAggregator eventAggregator)
        {
            _geminiService = geminiService;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe<ConversationItemDTO>(AddNewConversation);
            InitializeObjects();
            LoadConversations();
        }
        public async void SendMessage()
        {
            if (_request == null) return;
            _request = await AddRequestToMessages();
            //_response = await _geminiService.Chat(_request);
            await AddResponseToMessages(_response);
        }
        public void ShowSelectedConversation(int index)
        {
            ConversationItemDTO item = Conversations[index];
            //b2: Lấy Messages (nhiều cái Messages) dựa vào Id, nằm trong ConversationItemModel
            // Lấy bằng cách nào: tạo 1 service lấy Messages, v.d. _messageService.Get(int ConversationId)
            //b3: Sau khi lấy xong từ _messageService, thì nạp vô cái ObservableCollection<=
            Messages.Clear();
            if (index == 0)
            {
                foreach (var msg in Messages1)
                {
                    Messages.Add(new MessageItemDTO()
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
                    Messages.Add(new MessageItemDTO()
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
            List<ConversationItemDTO> conversations = new List<ConversationItemDTO>() {

                new ConversationItemDTO()
                {

                }
            };
            //B3. Bốc từng thằng ConversationItemModel nạp vô
            //ObservableCollection<ConversationItemModel> (load ConversationItemModel lên UI.)

        }
        private void AddNewConversation(ConversationItemDTO conversationItemModel)
        {
            _selectedConversationItemModel = conversationItemModel;
            Conversations.Add(conversationItemModel);
            TellGeminiToApplyPersona();
        }
        private void TellGeminiToApplyPersona()
        {

        }
        private async Task<string> AddRequestToMessages()
        {
            var request = await GetRequestAsync();
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Messages.Add(new MessageItemDTO()
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
                Messages.Add(new MessageItemDTO()
                {
                    Content = message,
                    ImageUrl = "https://i.pinimg.com/564x/a5/26/64/a526644653e3aa32e9164430ce66b304.jpg",
                    Sender = "Gemini"
                });
            }).Task;
        }

        private void InitializeObjects()
        {
            Conversations = new ObservableCollection<ConversationItemDTO>();
            Messages = new ObservableCollection<MessageItemDTO>();
        }


    }
}
