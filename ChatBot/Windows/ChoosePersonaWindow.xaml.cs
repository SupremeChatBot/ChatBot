using ChatBot.MVVM.ViewModel;
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
    public partial class ChoosePersonaWindow : Window
    {
        public ChoosePersonaWindow()
        {
            InitializeComponent();
        }
        private void headerThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Left = Left + e.HorizontalChange;
            Top = Top + e.VerticalChange;
        }
        public void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            
            if(DataContext is ChoosePersonaViewModel viewModel && sender is ToggleButton toggleButton)
            {
                viewModel.SaveSelectedPersonaCommand.Execute(toggleButton.Content);
            }            
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
      

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ChoosePersonaViewModel viewModel)
            {
                // Call the command from the ViewModel
                viewModel.SaveSelectedPersonaCommand.Execute(sender);
            }
            this.Hide();
        }
    }
}
