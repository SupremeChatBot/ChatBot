using ChatBot_Repo.Payload.Response;
using ChatBot_Repo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Services.Implementation
{
    public static class ResponseMapperService
    {
        public static Task<ConversationItemDTO> MapResponseToConversationItemDTO(dynamic response)
        {
            return Task.Run(() =>
            {
                dynamic deserializedJson = JsonUtils.DeserializeJson<dynamic>(response);
                string id = deserializedJson.data.conversationId.ToString().Trim('{', '}');
                string name = deserializedJson.data.name.ToString().Trim('{', '}');
                return new ConversationItemDTO()
                {
                    Id = id,
                    Name = name,
                };
            });
        }
        public static Task<MessageItemDTO> MapResponseToMessageItemDTO(dynamic response)
        {
            return Task.Run(() =>
            {
                dynamic deserializedJson = JsonUtils.DeserializeJson<dynamic>(response);
                string id = deserializedJson.data.messageId.ToString().Trim('{', '}');
                string content = deserializedJson.data.content.ToString().Trim('{', '}');
                string sender = deserializedJson.data.sender.ToString().Trim('{', '}');
                return new MessageItemDTO()
                {
                    Id = id,
                    Content = content,
                    Sender = sender,
                };
            });
        }

        public static Task<List<MessageItemDTO>> MapResponseToMessageItemDTOList(dynamic response)
        {
            return Task.Run(() =>
            {
                List<MessageItemDTO> results = new List<MessageItemDTO>();
                dynamic deserializedJson = JsonUtils.DeserializeJson<dynamic>(response);

                foreach (dynamic tempMessageDto in deserializedJson.data)
                {
                    results.Add(new MessageItemDTO()
                    {
                        Id = tempMessageDto.messageId.ToString().Trim('{', '}'),
                        Content = tempMessageDto.content.ToString().Trim('{', '}'),
                        Sender = tempMessageDto.sender.ToString().Trim('{', '}')
                    });                    
                }
                return results;
            });
        }

        public static Task<List<ConversationItemDTO>> MapResponseToConversationItemDTOList(dynamic response)
        {
            return Task.Run(() =>
            {
                dynamic deserializedJson = JsonUtils.DeserializeJson<dynamic>(response);

                List<ConversationItemDTO> results = new List<ConversationItemDTO>();
                foreach (dynamic tempMessageDto in deserializedJson.data)
                {
                    string id = tempMessageDto.conversationId.ToString().Trim('{', '}');
                    string name = tempMessageDto.name.ToString().Trim('{', '}');
                    results.Add(new ConversationItemDTO()
                    {
                        Id = id,
                        Name = name,
                    });
                }
                return results;
            });
        }
    }
}
