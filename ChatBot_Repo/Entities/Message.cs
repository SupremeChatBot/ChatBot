using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Entities
{
    public class Message
    {                     
        public string Content { get; set; }
        public string Sender { get; set; }
        public int ConversationId { get; set; } 

        
    }
}
