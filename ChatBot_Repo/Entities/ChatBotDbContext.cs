using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Entities
{
    public class ChatBotDbContext:DbContext
    {
        public ChatBotDbContext()
        {

        }
        public DbSet<Conversation> Conversations { get; set; }  
        public DbSet<Message> Messages { get; set; }    
    }
}
