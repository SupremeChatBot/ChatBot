using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.MVVM.Model
{
    public class PersonaItemModel
    {
        public int Id { get; set; } 

        public string Image { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; } 
        public bool IsSelected { get; set; }
        public string ImageUrl { get; set; }
    }
}
