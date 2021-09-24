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

namespace DoorConfig
{
    /// <summary>
    /// Interaction logic for DoorEditor.xaml
    /// </summary>
    public partial class DoorEditor : UserControl
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(
                                                                    "Label", typeof(string),typeof(DoorEditor));
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty IsIsOpenProperty = DependencyProperty.Register(
                                                                    "IsOpen", typeof(bool), typeof(DoorEditor));
        public bool IsOpen
        {
            get { return (bool)GetValue(IsIsOpenProperty); }
            set { SetValue(IsIsOpenProperty, value); }
        }

        public static readonly DependencyProperty IsIsLockedProperty = DependencyProperty.Register(
                                                                    "IsLocked", typeof(bool), typeof(DoorEditor));
        public bool IsLocked
        {
            get { return (bool)GetValue(IsIsLockedProperty); }
            set { SetValue(IsIsLockedProperty, value); }
        }

        public bool IsOk { get; set; }
        public DoorEditor()
        {
            InitializeComponent();
            IsOk = false;
            txtLabel.Focus();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            IsOk = true;
            Window window = Parent as Window;
            window.Close();
        }
    }
}
