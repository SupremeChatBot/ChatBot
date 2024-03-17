using GenerativeAI.Methods;
using GenerativeAI.Models;
using GenerativeAI.Types;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Services.Implementation
{
    public class GoogleGeminiService : IGoogleGeminiService
    {
        private GenerativeModel _model;
        private IConfiguration _configuration;
        private string _apiKey;
        private string _prompt;
        private ChatSession _chatSession;
        public GoogleGeminiService(IConfiguration configuration)
        {
            _configuration = configuration ;
            InitializeObjects();
        }
        public async Task<string> Chat(string prompt)
        {            
            var result = await _chatSession.SendMessageAsync(prompt);
            return result;            
        }
        
        private void InitializeObjects()
        {
            _apiKey = _configuration["AppSettings:ApiKey"];
            _model = new GenerativeModel(_apiKey);
            _chatSession = _model.StartChat(new StartChatParams());
        }
    }
}
