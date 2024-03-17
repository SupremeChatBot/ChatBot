﻿using ChatBot.Components;
using ChatBot.MVVM.Model;
using ChatBot_Repo.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.MVVM.ViewModel
{
    public class ConversationViewModel:ObservableObject
    {
        public ObservableCollection<MessageItem> MessageItems { get; set; }
        public ConversationViewModel() { }
        
    }
}
