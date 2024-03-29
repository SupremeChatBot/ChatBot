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

    public partial class MessageItem : UserControl
    {
        public string ImageUrl
        {
            get { return (string)GetValue(ImageUrlProperty); }
            set { SetValue(ImageUrlProperty, value); }
        }
        public string Sender
        {
            get { return (string)GetValue(SenderProperty); }
            set { SetValue(SenderProperty, value); }
        }
        public string Content
        {
            get { return (string)GetValue(ContentProp); }
            set { SetValue(ContentProp, value); }
        }
        public static readonly DependencyProperty SenderProperty =
            DependencyProperty.Register("Sender", typeof(string), typeof(MessageItem));
        public static readonly DependencyProperty ImageUrlProperty =
          DependencyProperty.Register("ImageUrl", typeof(string), typeof(MessageItem));
        public static readonly DependencyProperty ContentProp =
            DependencyProperty.Register("Content", typeof(string), typeof(MessageItem));
        public MessageItem()
        {
            InitializeComponent();
        }                
    }
}
