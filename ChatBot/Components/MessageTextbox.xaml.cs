using ChatBot.MVVM.ViewModel;
using System;
using System.Collections.Generic;
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

namespace ChatBot.Components
{
    public partial class MessageTextbox : UserControl
    {
        public event EventHandler<KeyEventArgs> TextBoxKeyDown;
        public MessageTextbox()
        {
            InitializeComponent();
        }
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {                
                var mainViewModel = DataContext as MainViewModel;
                TextBoxKeyDown.Invoke(this, e);
                messageTextbox.Text = "";
            }
        }
    }
}
