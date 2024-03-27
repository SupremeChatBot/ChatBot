using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Payload.Request
{
    public class ImpersonateConversationRequest
    {
        public string Name { get; set; }
        public List<ImpersonateMessageRequest> SampleMessages { get; set; } 
        
    }
}
