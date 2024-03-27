using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Payload.Response
{
    public class PersonaItemDTO
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }   
        public bool IsSelected { get; set; }
    }
}
