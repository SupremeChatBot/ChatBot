using ChatBot_Repo.Constants;
using ChatBot_Repo.Entities;
using ChatBot_Repo.Payload.Request;
using ChatBot_Repo.Payload.Response;
using ChatBot_Repo.Utils;
using GenerativeAI.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace ChatBot_Repo.Services.Implementation
{
    public class GeminiService : IGeminiService
    {
        private ApiService _apiService;
        private string _response;
        private ConversationItemDTO _conversationItemDto;
        private MessageItemDTO _messageItemDto;
        private List<MessageItemDTO> _messageItemsDto;
        private List<ConversationItemDTO> _listConversation;

        public GeminiService()
        {
            InitializeObjects();
        }

        public async Task<ConversationItemDTO> CreateNewConversation(ImpersonateConversationRequest request)
        {
            var requestString = await ImpersonateConversationRequestBuilder.Build(request);
            _response = await _apiService.PostNewConversation(requestString);
            await MapResponseToConversationItemDTO();            
            return _conversationItemDto;
        }

        public async Task<MessageItemDTO> CreateNewMessage(CreateNewChatParameters request)
        {
            var requestString = await MessageRequestBuilder.Build(request);
            _response = await _apiService.PostNewMeassge(requestString);
            await MapResponseToMessageItemDTO();
            return _messageItemDto;
        }

        public async Task<List<MessageItemDTO>> GetMessagesByConversationId(string id)
        {
            _response = await _apiService.GetMessagesByConversationId(id);
            await MapResponseToMessageItemDTOList();
            return _messageItemsDto;
        }

        public async Task<List<ConversationItemDTO>> LoadConversationList()
        {
            _response = await _apiService.LoadConversations();
            await MapResponseToConversationItemDTOList();
            return _listConversation;
        }

        private Task MapResponseToConversationItemDTO()
        {
            return Task.Run(() =>
            {
                dynamic deserializedJson = JsonUtils.DeserializeJson<dynamic>(_response);
                string id = deserializedJson.data.conversationId.ToString().Trim('{', '}');
                string name = deserializedJson.data.name.ToString().Trim('{', '}');                
                _conversationItemDto = new ConversationItemDTO()
                {
                    Id = id,
                    Name = name,                    
                };
            });
        }
        private Task MapResponseToMessageItemDTO()
        {
            return Task.Run(() =>
            {
                dynamic deserializedJson = JsonUtils.DeserializeJson<dynamic>(_response);
                string id = deserializedJson.data.messageId.ToString().Trim('{', '}');
                string content = deserializedJson.data.content.ToString().Trim('{', '}');
                string sender = deserializedJson.data.sender.ToString().Trim('{', '}');
                _messageItemDto = new MessageItemDTO()
                {
                    Id = id,
                    Content = content,
                    Sender = sender,
                };
            });
        }

        private Task MapResponseToMessageItemDTOList()
        {
            return Task.Run(() =>
            {
                _messageItemsDto.Clear(); 
                dynamic deserializedJson = JsonUtils.DeserializeJson<dynamic>(_response);

                foreach (dynamic tempMessageDto in deserializedJson.data)
                {
                    _messageItemsDto.Add(new MessageItemDTO()
                    {
                        Id = tempMessageDto.messageId.ToString().Trim('{', '}'),
                        Content = tempMessageDto.content.ToString().Trim('{', '}'),
                        Sender = tempMessageDto.sender.ToString().Trim('{', '}')
                    });
                }
            });
        }

        private Task MapResponseToConversationItemDTOList()
        {
            return Task.Run(() =>
            {
                dynamic deserializedJson = JsonUtils.DeserializeJson<dynamic>(_response);

                _listConversation.Clear();
                foreach (dynamic tempMessageDto in deserializedJson.data)
                {
                    string id = tempMessageDto.conversationId.ToString().Trim('{', '}');
                    string name = tempMessageDto.name.ToString().Trim('{', '}');
                    _listConversation.Add(new ConversationItemDTO()
                    {
                        Id = id,
                        Name = name,
                    });
                }
            });
        }

        private Task SaveResponseToConversationsJson()
        {
            return Task.Run(() =>
            {
                string conversationIdFilePath = FilePaths.ConversationIdJson;
                JsonUtils.AppendToJson<ConversationItemDTO>(_conversationItemDto, conversationIdFilePath);
            });
        }


        private void InitializeObjects()
        {
            _apiService = new ApiService();
            _messageItemsDto = new List<MessageItemDTO>();
            _listConversation = new List<ConversationItemDTO>();
        }
    }
}