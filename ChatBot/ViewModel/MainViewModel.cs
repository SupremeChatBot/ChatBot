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
using ChatBot_Repo.Payload.Request;
using ChatBot_Repo.Constants;

namespace ChatBot.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public ObservableCollection<ConversationItemDTO> Conversations { get; set; }
        public ObservableCollection<MessageItemDTO> Messages { get; set; }
        public string Request
        {
            get { return _request; }
            set
            {
                _request = value;
                OnPropertyChanged();
            }
        }
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        private bool _isLoading;
        private string _request;
        private IEventAggregator _eventAggregator;
        private ConversationItemDTO _selectedConversation;
        private readonly IGeminiService _geminiService;       
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
            IsLoading = true;
            if (_request == null) return;
            _request = await AddRequestToMessages();
            var response = await _geminiService.CreateNewMessage(new CreateNewChatParameters()
            {
                Content = _request,
                Sender = "user",
                ConversationId = _selectedConversation.Id                
            });
            await AddResponseToMessages(response);
            IsLoading = false;
        }
        public async void ShowMessagesBySelectedConversation(int index)
        {
            IsLoading = true;
            _selectedConversation = Conversations[index];
            var listMessages = await _geminiService.GetMessagesByConversationId(_selectedConversation.Id);
            if(listMessages.Count == 0) return;
            await RefreshMessageList(listMessages);
            IsLoading = false;
        }
        private async void LoadConversations()
        {
            IsLoading = true;
            var listConversation = await _geminiService.LoadConversationList();

            foreach (var conversation in listConversation)
            {
                AddNewConversation(conversation);
            }

            IsLoading = false;
        }
        private void AddNewConversation(ConversationItemDTO conversationItemModel)
        {
            Conversations.Add(conversationItemModel);
        }
        private async Task<string> AddRequestToMessages()
        {
            var request = await GetRequestAsync();
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Messages.Add(new MessageItemDTO()
                {
                    Content = request,
                    ImageUrl = ImageUrl.UserAvatar,
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
        private Task AddResponseToMessages(MessageItemDTO message)
        {
            return Application.Current.Dispatcher.InvokeAsync(() =>
            {
                message.ImageUrl = ImageUrl.BotAvatar;
                Messages.Add(message);
            }).Task;
        }

        private async Task RefreshMessageList(List<MessageItemDTO> messages)
        {
            await Task.Run(() =>
            {            
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Messages.Clear();

                    foreach (var msg in messages)
                    {
                        if (msg.Sender.Equals("model")) 
                            msg.ImageUrl = ImageUrl.BotAvatar;
                        else msg.ImageUrl = ImageUrl.UserAvatar;

                        Messages.Add(msg);
                    }
                });
            });
        }
        private void InitializeObjects()
        {
            Conversations = new ObservableCollection<ConversationItemDTO>();
            Messages = new ObservableCollection<MessageItemDTO>();
        }
    }
}
