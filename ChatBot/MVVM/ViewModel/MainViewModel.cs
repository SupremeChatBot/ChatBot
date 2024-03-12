using ChatBot_Repo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ConversationViewModel ConversationVM { get; set; }
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            ConversationVM = new ConversationViewModel();
            CurrentView = ConversationVM;
        }
    }
}
