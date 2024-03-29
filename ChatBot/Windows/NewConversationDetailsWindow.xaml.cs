using ChatBot.Components.NewConversationDetails;
using ChatBot.ViewModel;
using ChatBot_Repo.EventAggregator;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ChatBot.Windows
{
    public partial class NewConversationDetailsWindow : Window
    {
        private ChoosePersona choosePersonaControl;
        private ConversationDetails conversationDetailsControl;
        private TextBlock _conversationNameTextBlock;
        private TextBlock _choosePersonaTextBlock;
        private NewConversationDetailsViewModel _newConversationDetailsViewModel;


        private int currentIndex = 0;
        public NewConversationDetailsWindow(NewConversationDetailsViewModel newConversationDetailsViewModel)
        {
            _newConversationDetailsViewModel = newConversationDetailsViewModel;
            _newConversationDetailsViewModel.OnInputValidationFail += ShowInvalidConversationDetails;
            _newConversationDetailsViewModel.OnSuccessInsertion+= HandleSuccessfulConversationCreation;
            this.DataContext = newConversationDetailsViewModel;
            InitializeComponent();
            InitializeUserControls();
        }
        private void headerThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }       
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
      
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is NewConversationDetailsViewModel viewModel && sender is Button toggleButton)
            {
                // Call the command in the ViewModel
                viewModel.CreateNewConversationCommand.Execute(sender);
                UserControlContentControl.Content = choosePersonaControl;                                                             
            }            
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            currentIndex++;
            if (currentIndex > 1)
                currentIndex = 0;

            UpdateDisplayedUserControl();
            UpdateDisplayedLabel();
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            currentIndex++;
            if (currentIndex > 1)
                currentIndex = 0;

            UpdateDisplayedUserControl();
            UpdateDisplayedLabel();
        }
        private void UpdateDisplayedUserControl()
        {
            // Display the appropriate user control based on the currentIndex
            if (currentIndex == 0)
                UserControlContentControl.Content = choosePersonaControl;
            else
                UserControlContentControl.Content = conversationDetailsControl;
        }
        private void UpdateDisplayedLabel()
        {
            if (currentIndex == 0)
                LabelContentControl.Content = _choosePersonaTextBlock;
            else
                LabelContentControl.Content = _conversationNameTextBlock;
        }
        private void InitializeUserControls()
        {
            choosePersonaControl = new ChoosePersona(_newConversationDetailsViewModel);            
            conversationDetailsControl= new ConversationDetails(_newConversationDetailsViewModel);
            _choosePersonaTextBlock = ChoosePersonaTextBlock();
            _conversationNameTextBlock = ConversationNameTextBlock();
            UserControlContentControl.Content = choosePersonaControl;
            LabelContentControl.Content = _choosePersonaTextBlock; 
        }
        private TextBlock ChoosePersonaTextBlock()
        {
            return new TextBlock()
            {
                Text = "Choose Persona",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Foreground = Brushes.White,
                FontSize = 24,
                FontFamily = new FontFamily("/Fonts/#Poppins"),
                Margin = new(30,10, 0, 0)
            };
        }
        private TextBlock ConversationNameTextBlock()
        {
            return new TextBlock()
            {
                Text = "Conversation Name",
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Foreground = Brushes.White,
                FontSize = 24,
                FontFamily = new FontFamily("/Fonts/#Poppins"),
                Margin = new(30, 10, 0, 0)
            };
        }  
        private void ShowInvalidConversationDetails(object sender, EventArgs e)
        {
            MessageBox.Show("The persona is not chosen, or the conversation name is invalid.",
                   "Warning"
                   , MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void HandleSuccessfulConversationCreation(object sender,EventArgs e)
        {

            MessageBox.Show("Successfully inserted the conversation.",
                   "Info"
                   , MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();   
        }
       
       
    }
}
