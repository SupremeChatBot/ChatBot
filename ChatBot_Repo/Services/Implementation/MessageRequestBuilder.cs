using ChatBot_Repo.Payload.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatBot_Repo.Services.Implementation
{    
    public static class MessageRequestBuilder
    {          
        private static CreateNewChatParameters _request;
        private static string _serializedMessage;
        private static string _result;
        public static Task<string> Build(CreateNewChatParameters request)
        {
            return Task.Run(() =>
            {
                _request = request;
                SerializedMessage();
                ConstructMessageRequest();
                return _result;
            });
        }
        private static void ConstructMessageRequest()
        {
            _result = 
                $"{{" +
                $"\"data\":{_serializedMessage}" +
                $"}}";         
        }
        private static void SerializedMessage()
        {
            _serializedMessage = JsonSerializer.Serialize(_request);
        }
    }
}
