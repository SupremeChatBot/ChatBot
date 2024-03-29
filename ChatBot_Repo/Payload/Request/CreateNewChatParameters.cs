using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Payload.Request
{
    public class CreateNewChatParameters
    {
        public string Content { get; set; }

        public string Sender { get; set; }

        public string ConversationId { get; set; }
    }
}
