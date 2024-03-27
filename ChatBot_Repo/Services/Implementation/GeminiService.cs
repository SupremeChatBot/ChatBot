using ChatBot_Repo.Constants;
using ChatBot_Repo.Entities;
using ChatBot_Repo.Payload.Request;
using ChatBot_Repo.Payload.Response;
using ChatBot_Repo.Utils;
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
        public GeminiService()
        {
            InitializeObjects();
        }
        public async Task<ConversationItemDTO> CreateNewConversation(ImpersonateConversationRequest request)
        {
            var requestString = await ImpersonateConversationRequestBuilder.Build(request);
            _response = await _apiService.PostNewConversation(requestString);
            await MapResponseToConversationItemDTO();
            await SaveResponseToConversationsJson();
            return _conversationItemDto;
        }
        public async Task<List<MessageItemDTO>> GetMessagesByConversationId(int id) { throw new NotImplementedException(); }
        private Task MapResponseToConversationItemDTO()
        {
            return Task.Run(() =>
            {
                dynamic deserializedJson = JsonUtils.DeserializeJson<dynamic>(_response);                
                string id = deserializedJson.data.conversationId.ToString().Trim('{', '}');
                string name = deserializedJson.data.name.ToString().Trim('{', '}');
                _conversationItemDto = new ConversationItemDTO()
                {
                    Id = id ,
                    Name = name,                    
                };
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
        }
       
    }
}
