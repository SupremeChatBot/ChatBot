using ChatBot.MVVM.Model;
using ChatBot.Windows;
using ChatBot_Repo.Core;
using ChatBot_Repo.EventAggregator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ChatBot.MVVM.ViewModel
{
    public class NewConversationDetailsViewModel : ObservableObject
    {
        public ObservableCollection<PersonaItemModel> Personas { get; set; }
        public ICommand SaveNewConversationDetailsCommand
        {
            get =>
                new RelayCommand(SaveNewConversationDetails, CanCmdExec);
        }
        public ICommand ChangeSelectedPersonaCommand
        {
            get =>
                new RelayCommand(ChangeSelectedPersona, CanCmdExec);
        }
        public ICommand ChangeConversationNameCommand
        {
            get =>
               new RelayCommand(ChangeConversationName, CanCmdExec);
        }

        private string conversationName;
        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        public List<object> Options = new List<object>();
        public string ConversationName
        {
            get { return conversationName; }
            set
            {
                conversationName = value;
                OnPropertyChanged(nameof(ConversationName));
            }
        }
        private readonly IEventAggregator _eventAggregator;

        public NewConversationDetailsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            InitializeObjects();
            PopulateDummyData();
        }


        private void PopulateDummyData()
        {
            var personas = new[]
            {
            new { Id = 1, Name = "Johnny Depp", Description = "I want you to act as Johnny Depp, expert in acting and character immersion, specializing in eccentric roles and in-depth character study. "},
            new { Id = 2, Name = "Lionel Messi", Description = "I want you to act as Lionel Messi, expert in soccer and athleticism, specializing in dribbling and playmaking." },
            new { Id = 3, Name = "Taylor Swift", Description = "I want you to act as Taylor Swift, expert in music and songwriting, specializing in storytelling and relatable lyrics." },
            new { Id = 4, Name = "Anne Hathaway", Description = "I want you to act as Anne Hathaway, expert in acting and versatility, specializing in diverse roles and captivating performances." },
            new { Id = 5, Name = "Andrew Tate", Description = "I want you to act as Andrew Tate, expert in fitness and self-defense, specializing in kickboxing and weightlifting." }
            };
    
            foreach (var p in personas)
            {
                Personas.Add(new PersonaItemModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,                    

                }); ;
            }
        }
        private void InitializeObjects()
        {
            Personas = new ObservableCollection<PersonaItemModel>();
        }
        private void SaveNewConversationDetails(object obj)
        {
            string name = ConversationName;
            int index = selectedIndex;
            _eventAggregator.Publish(new ConversationItemModel()
            {
                Id = 1,
                Name = name,
            });
        }
        private void ChangeSelectedPersona(object obj)
        {
            if (obj != null)
            {
                SelectedIndex = (int)obj;
            }
        }
        private void ChangeConversationName(object obj)
        {
            ConversationName = obj.ToString();
        }

        private bool CanCmdExec(object obj) => true;
    }
}
