using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Entities
{
    public class Persona
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Prompt { get; set; }

        public ICollection<Conversation> Conversations { get; set; }
    }
}
