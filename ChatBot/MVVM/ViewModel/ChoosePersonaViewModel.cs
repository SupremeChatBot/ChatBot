using ChatBot.MVVM.Model;
using ChatBot.Theme;
using ChatBot_Repo.Core;
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
    class ChoosePersonaViewModel : ObservableObject
    {
        public ObservableCollection<PersonaItemModel> Personas { get; set; }
        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    OnPropertyChanged(nameof(SelectedIndex));
                }
            }
        }
        public ICommand SaveSelectedPersonaCommand { get => 
                new RelayCommand(SaveSelectedPersona, CanCmdExec); }

        public ICommand SendSelectedPersonaToDbCommand
        {
            get =>
            new RelayCommand(SendSelectedPersonaToDb, CanCmdExec);
        }
        
        
        public ChoosePersonaViewModel()
        {
            InitializeObjects();
            PopulateDummyData();            
        }

        private void PopulateDummyData()
        {
            List<string> names = new List<string>() { "Albert", "Alice", "Hubert", "Lmao", "Programmer" };
            int id = 0;
            foreach (var name in names)
            {
                Personas.Add(new PersonaItemModel()
                {
                   Id = id,
                   Name = name,
                   Description = "Lorem ipsum dolot sit amet"                   
                });
                id++;
            }
        }
        private void InitializeObjects()
        {
            Personas = new ObservableCollection<PersonaItemModel>();
        }
        private void SaveSelectedPersona(object obj)
        {            
            Debug.WriteLine($"Current Content: {SelectedIndex}");
            
        }       
        private void SendSelectedPersonaToDb(object obj)
        {            
        }
        private bool CanCmdExec(object obj) => true;
    }
}
