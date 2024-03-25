using ChatBot.Components.NewConversationDetails;
using ChatBot.MVVM.ViewModel;
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
    public partial class NewConversationDetails : Window
    {
        private ChoosePersona choosePersonaControl;
        private ConversationDetails conversationDetailsControl;
        private TextBlock _conversationNameTextBlock;
        private TextBlock _choosePersonaTextBlock;
        private NewConversationDetailsViewModel _newConversationDetailsViewModel;


        private int currentIndex = 0;
        public NewConversationDetails(NewConversationDetailsViewModel newConversationDetailsViewModel)
        {
            _newConversationDetailsViewModel = newConversationDetailsViewModel;
            this.DataContext = newConversationDetailsViewModel;
            InitializeComponent();
            InitializeUserControls();
        }
        private void headerThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }
        public void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {            
            if(this.DataContext is NewConversationDetailsViewModel viewModel && sender is ToggleButton toggleButton)
            {
                viewModel.SaveNewConversationDetailsCommand.Execute(toggleButton.Content);
            }            
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
      
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is NewConversationDetailsViewModel viewModel && sender is Button toggleButton)
            {
                // Call the command in the ViewModel
                viewModel.SaveNewConversationDetailsCommand.Execute(sender);
            }
            this.Hide();
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
    }
}
