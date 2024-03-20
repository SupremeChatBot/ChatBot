using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Entities
{
    
    public class Conversation
    {
        public int Id { get; set; }
        public string Name { get; set; }          
        public ICollection<Message> Messages { get; set; }
    }
}
