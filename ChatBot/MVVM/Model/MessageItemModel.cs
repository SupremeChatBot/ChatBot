using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.MVVM.Model
{
    public class MessageItemModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Sender { get; set; }  
        public string Content { get; set; } 
    }
}
