﻿using ChatBot_Repo.Constants;
using ChatBot_Repo.Entities;
using ChatBot_Repo.Payload.Request;
using GenerativeAI.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatBot_Repo.Services.Implementation
{
    
    public class ApiService
    {
        private HttpClient httpClient;
        public ApiService() {
            InitializeObjects();
        }
        public async Task<HttpResponseMessage> GetConversationById(int conversationId)
        {
            var response = await httpClient.GetAsync(ApiServiceUrl.GetConversationByIdEndpoint(conversationId));
            return response;
        }
        public async Task<string> PostNewConversation(string request)
        {
            HttpContent content = new StringContent(request, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(ApiServiceUrl.NewConversationEndpoint,content);
            var responseData = await response.Content.ReadAsStringAsync();            
            return responseData;
        }
        private void InitializeObjects()
        {
            httpClient = new HttpClient();
        }
    }
}
