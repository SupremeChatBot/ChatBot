using ChatBot.Windows;
using ChatBot_Repo.Core;
using ChatBot_Repo.Payload.Request;
using ChatBot_Repo.EventAggregator;
using ChatBot_Repo.Payload.Response;
using ChatBot_Repo.Services;
using ChatBot_Repo.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ChatBot_Repo.Utils;
using ChatBot_Repo.Constants;
using Microsoft.IdentityModel.Tokens;
using ChatBot_Repo.Entities;

namespace ChatBot.ViewModel
{
    public class NewConversationDetailsViewModel : ObservableObject
    {
        public ObservableCollection<PersonaItemDTO> Personas { get; set; }
        public ICommand CreateNewConversationCommand
        {
            get =>
                new RelayCommand(CreateNewConversation, CanCmdExec);
        }
        public ICommand ChangeSelectedPersonaCommand
        {
            get =>
                new RelayCommand(ChangeSelectedPersona, CanCmdExec);
        }
        public ICommand ChangeConversationNameCommand
        {
            get =>
               new RelayCommand(ChangeConversationName, CanCmdExec);
        }
        public event EventHandler OnInputValidationFail;
        public event EventHandler OnSuccessInsertion;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }
        public string ConversationName
        {
            get { return _conversationName; }
            set
            {
                _conversationName = value;
                OnPropertyChanged(nameof(ConversationName));
            }
        }
        public bool IsOperationSuccessful
        {
            get { return _isOperationSuccessful; }
            set
            {
                _isOperationSuccessful = value;
                OnPropertyChanged(nameof(IsOperationSuccessful));
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
        private bool _isOperationSuccessful;
        private string _conversationName;
        private int _selectedIndex;
        private PersonaItemDTO _selectedPersonaItemDto;
        private readonly IEventAggregator _eventAggregator;
        private readonly IGeminiService _geminiService;
        private readonly IGoogleGeminiService _googleGeminiService;


        public NewConversationDetailsViewModel(IEventAggregator eventAggregator, IGeminiService geminiService, IGoogleGeminiService googleGeminiService)
        {
            _eventAggregator = eventAggregator;
            _geminiService = geminiService;
            _googleGeminiService = googleGeminiService;
            InitializeObjects();
            LoadPersonaData();
        }


        private void LoadPersonaData()
        {
            var personas = JsonUtils.DeserializeJsonList<PersonaItemDTO>(FilePaths.PersonasJson);

            foreach (var p in personas)
            {
                Personas.Add(new PersonaItemDTO()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl
                });
            }
        }

        private async void CreateNewConversation(object obj)
        {
            if (!AreConversationDetailsValid())
            {
                OnInputValidationFail.Invoke(this, EventArgs.Empty);
                return;
            }
            IsLoading = true;
            var conversationItemDto = await SendCreateRequestToGemini();
            _eventAggregator.Publish(conversationItemDto);
            OnSuccessInsertion.Invoke(this, EventArgs.Empty);
            IsLoading = false;

        }
        private async Task<ConversationItemDTO> SendCreateRequestToGemini()
        {
            var respondPrompt = await _googleGeminiService.Chat(_selectedPersonaItemDto.Description);

            var result = await _geminiService.CreateNewConversation(new ImpersonateConversationRequest()
            {
                Name = ConversationName,
                SampleMessages = new List<ImpersonateMessageRequest>()
               {
                   new ImpersonateMessageRequest(){ Content =_selectedPersonaItemDto.Description,Sender = "user" },
                   new ImpersonateMessageRequest(){ Content = respondPrompt,Sender = "model" }
               }
            });

            return result;
        }
        private void ChangeSelectedPersona(object obj)
        {
            if (obj != null)
            {
                _selectedPersonaItemDto = Personas[(int)obj];
            }
        }
        private void ChangeConversationName(object obj)
        {
            ConversationName = obj.ToString();
        }
        private bool AreConversationDetailsValid()
        {
            if (_selectedPersonaItemDto == null ||
                !ValidationUtils.IsStringValid(_conversationName, RegexStrings.ValidConversationName))
                return false;
            return true;
        }
        private bool CanCmdExec(object obj) => true;
        private void InitializeObjects()
        {
            Personas = new ObservableCollection<PersonaItemDTO>();
        }
    }
}
