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
    public partial class ConversationItem : UserControl
    {
        public string ItemContent
        {
            get { return (string)GetValue(ItemContentProperty); }
            set { SetValue(ItemContentProperty, value); }
        }
        public decimal ItemWidth
        {
            get { return (decimal)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }
        
        public decimal ItemHeight
        {
            get { return (decimal)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }
        public static readonly DependencyProperty ItemWidthProperty =
           DependencyProperty.Register("ItemWidth", typeof(decimal), typeof(ConversationItem));
        public static readonly DependencyProperty ItemHeightProperty =
           DependencyProperty.Register("ItemHeight", typeof(decimal), typeof(ConversationItem));
        public static readonly DependencyProperty ItemContentProperty =
          DependencyProperty.Register("ItemContent", typeof(string), typeof(ConversationItem));
        public ConversationItem()
        {
            InitializeComponent();
        }
    }
}
