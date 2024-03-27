using ChatBot.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatBot.Components.NewConversationDetails
{
    /// <summary>
    /// Interaction logic for ConversationDetails.xaml
    /// </summary>
    public partial class ConversationDetails : UserControl
    {
        public static readonly DependencyProperty ConversationNameProperty =
           DependencyProperty.Register("ConversationName", typeof(string), typeof(ConversationDetails),
           new FrameworkPropertyMetadata(
         "", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public string ConversationName
        {
            get { return (string)GetValue(ConversationNameProperty); }
            set { SetValue(ConversationNameProperty, value); }
        }
        public ConversationDetails(NewConversationDetailsViewModel newConversationDetailsViewModel)
        {
            this.DataContext = newConversationDetailsViewModel;
            InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataContext is NewConversationDetailsViewModel viewModel)
            {
                viewModel.ChangeConversationNameCommand.Execute(ConversationName);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Access the TextBox's value
            if (DataContext is NewConversationDetailsViewModel viewModel)
            {
                viewModel.ChangeConversationNameCommand.Execute(ConversationNameTextBox.Text);
            }
        }
    }
}
