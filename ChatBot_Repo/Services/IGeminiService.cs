using ChatBot_Repo.Payload.Request;
using ChatBot_Repo.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Services
{
    public interface IGeminiService
    {
        Task<ConversationItemDTO> CreateNewConversation(ImpersonateConversationRequest request);
        Task<List<MessageItemDTO>> GetMessagesByConversationId(int id);
    }
}
