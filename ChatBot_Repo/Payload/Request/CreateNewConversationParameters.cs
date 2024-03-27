using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Payload.Request
{
    public class CreateNewConversationParameters
    {
        public string ConversationName { get; set; }
        public List<ImpersonateMessageRequest> SampleMessages { get; set; }

    }
}
