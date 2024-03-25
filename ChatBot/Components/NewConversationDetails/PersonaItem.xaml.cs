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

namespace ChatBot.Components.NewConversationDetails
{
 
    public partial class PersonaItem : UserControl
    {
        public int Id { get; set; } 
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }
        public string BackgroundColor
        {
            get { return (string)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }
        public string ForegroundColor
        {
            get { return (string)GetValue(ForegroundColorProperty); }
            set { SetValue(ForegroundColorProperty, value); }
        }
        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }
        public string PersonaName
        {
            get { return (string)GetValue(PersonaNameProperty); }
            set { SetValue(PersonaNameProperty, value); }
        }
        public string PersonaDescription
        {
            get { return (string)GetValue(PersonaDescriptionProperty); }
            set { SetValue(PersonaDescriptionProperty, value); }
        }

        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.Register("ItemWidth", typeof(double), typeof(PersonaItem));
        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register("ItemHeight", typeof(double), typeof(PersonaItem));

        public static readonly DependencyProperty PersonaNameProperty =
           DependencyProperty.Register("PersonaName", typeof(string), typeof(PersonaItem));
        public static readonly DependencyProperty PersonaDescriptionProperty =
            DependencyProperty.Register("PersonaDescription", typeof(string), typeof(PersonaItem));
        public static readonly DependencyProperty IsSelectedProperty =
          DependencyProperty.Register("IsSelected", typeof(bool), typeof(PersonaItem));
        public static readonly DependencyProperty BackgroundColorProperty =
         DependencyProperty.Register("BackgroundColor", typeof(string), typeof(PersonaItem));
        public static readonly DependencyProperty ForegroundColorProperty =
        DependencyProperty.Register("ForegroundColor", typeof(string), typeof(PersonaItem));
        public PersonaItem()
        {
            InitializeComponent();
        }
    }
}
