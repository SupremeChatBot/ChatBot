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

    public partial class LoadingOverlay : UserControl
    {

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }
        public static readonly DependencyProperty IsLoadingProperty =
        DependencyProperty.Register("IsLoading", typeof(bool), typeof(LoadingOverlay));
        public int Height
        {
            get { return (int)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }
        public static readonly DependencyProperty HeightProperty =
        DependencyProperty.Register("Height", typeof(int), typeof(LoadingOverlay));
        public int Width
        {
            get { return (int)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }
        public static readonly DependencyProperty WidthProperty =
        DependencyProperty.Register("Width", typeof(int), typeof(LoadingOverlay));
        public LoadingOverlay()
        {
            InitializeComponent();
        }
    }
}
