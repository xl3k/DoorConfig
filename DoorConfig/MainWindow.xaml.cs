using DoorConfig.DoorControllerService;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void doorList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //if(doorList.SelectedValue != null)
            //{
            //    Door selectedDoor = doorList.SelectedValue as Door;
            //    DoorEditor de = new DoorEditor
            //    {
            //        Label = selectedDoor.Label,
            //        IsOpen = selectedDoor.IsOpen,
            //        IsLocked = selectedDoor.IsLocked
            //    };

            //    Window window = new Window
            //    {
            //        Title = "Edit Door",
            //        Content = de,
            //        SizeToContent = SizeToContent.WidthAndHeight

            //    };

            //    window.ShowDialog();
            //}

        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            DoorEditor de = new DoorEditor
            {
                Label = "",
                IsOpen = false,
                IsLocked = true
            };

            Window window = new Window
            {
                Title = "New Door",
                Content = de,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen

            };

            window.ShowDialog();

            de = window.Content as DoorEditor;

            if(de.IsOk)
            {
                DoorManager vm = DataContext as DoorManager;
                vm.AddNewDoor(new Door { IsLocked = de.IsLocked, IsOpen = de.IsOpen, Label = de.Label });
            }

            
        }
    }
}
