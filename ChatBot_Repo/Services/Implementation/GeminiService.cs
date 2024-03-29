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
        public GeminiService()
        {
            InitializeObjects();
        }

        public async Task<ConversationItemDTO> CreateNewConversation(ImpersonateConversationRequest request)
        {
            var requestString = await ImpersonateConversationRequestBuilder.Build(request);
            var response = await _apiService.PostNewConversation(requestString);
            var conversationItemDto = await ResponseMapperService.MapResponseToConversationItemDTO(response);            
            return conversationItemDto;
        }

        public async Task<MessageItemDTO> CreateNewMessage(CreateNewChatParameters request)
        {
            var requestString = await MessageRequestBuilder.Build(request);
            var response = await _apiService.PostNewMeassge(requestString);
            var messageItemDto = await ResponseMapperService.MapResponseToMessageItemDTO(response);
            return messageItemDto;
        }

        public async Task<List<MessageItemDTO>> GetMessagesByConversationId(string id)
        {
            var response = await _apiService.GetMessagesByConversationId(id);
            var messageItemDtoList = await ResponseMapperService.MapResponseToMessageItemDTOList(response);
            return messageItemDtoList;
        }

        public async Task<List<ConversationItemDTO>> LoadConversationList()
        {
            var response = await _apiService.LoadConversations();
            var conversationItemDtoList = await ResponseMapperService.MapResponseToConversationItemDTOList(response);
            return conversationItemDtoList;
        }
        
        private void InitializeObjects()
        {
            _apiService = new ApiService();           
        }
    }
}