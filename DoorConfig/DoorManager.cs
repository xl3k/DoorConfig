using DoorConfig.DoorControllerService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DoorConfig
{
    public class DoorManager: ViewModelBase, DoorControllerServiceCallback
    {
        InstanceContext instanceContext;
        public ObservableCollection<Door> Doors { get; private set; }
       
        public RelayCommand<object> UpdateDoorHandler { get; private set; }
        public RelayCommand<object> DeleteDoorHandler { get; private set; }
        public RelayCommand<object> AddNewDoorHandler { get; private set; }
        public RelayCommand CommandWindowsClosingHandler { get; private set; }
        DoorControllerServiceClient doorControllerClient;
        string clientId;
        
        public DoorManager()
        {
            instanceContext = new InstanceContext(this);
            doorControllerClient = new DoorControllerServiceClient(instanceContext);

            clientId = Guid.NewGuid().ToString();
            doorControllerClient.Subscribe(clientId); // todo: put it in a separate thread

            UpdateDoorHandler = new RelayCommand<object>(PerformUpdateDoor);
            DeleteDoorHandler = new RelayCommand<object>(PerformDeleteDoor);
            AddNewDoorHandler = new RelayCommand<object>(PerformAddNewDoor);
            CommandWindowsClosingHandler = new RelayCommand(WindowsClosing);
            Doors = new ObservableCollection<Door>();

            Task.Run(() => {
                doorControllerClient.GetDoors();
            });
            
            

        }

        
        private void PerformUpdateDoor(object commandParameter)
        {
            Task.Run(() => {
                Door door = commandParameter as Door;
                if (door != null)
                {
                    doorControllerClient.UpdateDoor(door.Id, door);
                }
            });
            
        }
        private void PerformDeleteDoor(object commandParameter)
        {
            Task.Run(() => {
                Door door = commandParameter as Door;
                if (door != null)
                {
                    doorControllerClient.DeleteDoor(door.Id);
                }
            });
            
        }
        private void PerformAddNewDoor(object commandParameter)
        {
            Task.Run(() => {
                Door door = commandParameter as Door;
                if (door != null)
                {
                    doorControllerClient.AddDoor(door);
                }
            });
            
        }

        // todo: remove it and use the command route
        public void AddNewDoor(object param)
        {
            Task.Run(() => {
                Door door = param as Door;
                if (door != null)
                {
                    doorControllerClient.AddDoor(door);
                }
            });

        }

        public void OnDoorUpdated(Door door)
        {
            if (door != null)
            {
                Application.Current.Dispatcher.Invoke(
               new Action(() =>
               {
                   var found = Doors.FirstOrDefault(x => x.Id == door.Id);
                   if (found != null)
                   {
                       found.Label = door.Label;
                       found.IsLocked = door.IsLocked;
                       found.IsOpen = door.IsOpen;
                   }
               })
               );
                
            }
        }

        public void OnDoorDeleted(string id, bool isDeleted)
        {
            
            if (isDeleted)
            {
                Application.Current.Dispatcher.Invoke(
               new Action(() =>
               {
                   var found = Doors.FirstOrDefault(x => x.Id == id);
                   if (found != null)
                   {
                       Doors.Remove(found);
                       return;
                   }
               })
               );

                
            }
        }

        public void OnDoorAdded(Door door)
        {
            if (door != null)
            {
                Application.Current.Dispatcher.Invoke(
               new Action(() =>
               {
                   var found = Doors.FirstOrDefault(x => x.Id == door.Id);
                   if (found == null)
                   {
                       Doors.Add(door);
                       return;
                   }
               })
               );


            }
        }

        public void OnDoorListReceived(Door[] doors)
        {
           Application.Current.Dispatcher.Invoke(
                new Action(() =>
                {
                    Doors.Clear();

                    foreach (var door in doors)
                    {
                        Doors.Add(door);
                    }
                })               
                );
           

        }

        

        public void WindowsClosing()
        {
            // any processing required when application is closing comes here
            // e.g. save app settings
            // close any connection to rpc, etc
            doorControllerClient.UnSubscribe(clientId);
            
        }
    }    
}
