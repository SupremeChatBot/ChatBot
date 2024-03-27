using ChatBot_Repo.Payload.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatBot_Repo.Services.Implementation
{    
    public static class ImpersonateConversationRequestBuilder
    {          
        private static ImpersonateConversationRequest _request;
        private static string _serializedSampleMessages;
        private static string _result;
        public static Task<string> Build(ImpersonateConversationRequest request)
        {
            return Task.Run(() =>
            {
                _request = request;                
                SerializedSampleMessages();
                ConstructImpersonateConversationRequest();
                return _result;
            });
        }
        private static void ConstructImpersonateConversationRequest()
        {
            _result = 
                $"{{" +
                $"\"data\":{{" +
                $"\"name\":\"{_request.Name}\"," +
                $"\"sampleMessages\":{_serializedSampleMessages}" +
                $"}}" +
                $"}}";
                     
        }
        private static void SerializedSampleMessages()
        {
            _serializedSampleMessages += '[';
            foreach (var sampleMessage in _request.SampleMessages)
            {
                string serializedSampleMessage = $"{{\"data\":{JsonSerializer.Serialize(sampleMessage)}}}";
                _serializedSampleMessages += serializedSampleMessage;
                if (!sampleMessage.Equals(_request.SampleMessages.Last())){
                    _serializedSampleMessages += ',';
                }
            }
            _serializedSampleMessages += ']';

        }
    }
}
