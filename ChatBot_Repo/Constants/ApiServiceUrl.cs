using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Constants
{
    public static class ApiServiceUrl
    {
        private static string _baseUrl = "http://localhost:3002";
        public static string NewConversationEndpoint = $"{_baseUrl}/conversations";
        public static string GetConversationByIdEndpoint(int id)
        {
            return $"{_baseUrl}/conversations/{id}";
        }
        public static string NewMessageEndpoint = $"{_baseUrl}/messages";        
    }
}
