using ChatBot.ViewModel;
using ChatBot_Repo.Payload.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatBot.Components.NewConversationDetails
{
    public partial class ChoosePersona : UserControl
    {
        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(ChoosePersona),
            new FrameworkPropertyMetadata(
          0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        public static readonly DependencyProperty PersonasSourceProperty =
         DependencyProperty.Register("PersonasSource", typeof(ObservableCollection<PersonaItemDTO>), typeof(ChoosePersona),
              new FrameworkPropertyMetadata(
            null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public ObservableCollection<PersonaItemDTO> PersonasSource
        {
            get { return (ObservableCollection<PersonaItemDTO>)GetValue(PersonasSourceProperty); }
            set { SetValue(PersonasSourceProperty, value); }
        }      
              
        public ChoosePersona(NewConversationDetailsViewModel newConversationDetailsViewModel)
        {
            this.DataContext = newConversationDetailsViewModel; 
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is NewConversationDetailsViewModel viewModel)
            {
                viewModel.ChangeSelectedPersonaCommand.Execute(SelectedIndex);
            }
        }
      
    }
}
