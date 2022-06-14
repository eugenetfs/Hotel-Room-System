using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Hotel_Room_System
{
    internal class HotelViewModel : INotifyPropertyChanged
    {
        #region Public Members
        public event PropertyChangedEventHandler PropertyChanged;
        public string selectedRoom;
        public string availRooms;
        #endregion

        #region Private Members
        private bool isAllRoomFull;
        private string convertText;
        private List<List<Room>> roomList = new List<List<Room>>();
        private RelayCommand assignCommand;
        private RelayCommand checkOutCommand;
        private RelayCommand cleanCommand;
        private RelayCommand outOfServiceCommand;
        private RelayCommand repairedCommand;
        private RelayCommand showCommand;
        #endregion

        #region Properties
        public string ConvertText
        {
            get { return convertText; }
            set
            {
                convertText = value;
                OnPropertyChanged("ConvertText");
            }
        }
        public List<List<Room>> RoomList
        {
            get { return roomList; }
            set
            {
                roomList = value;
                OnPropertyChanged("RoomList");
            }
        }
        public RelayCommand AssignCommand
        {
            get
            {
                if (assignCommand == null)
                    assignCommand = new RelayCommand(OnAssignCommand);
                return assignCommand;
            }
            set { assignCommand = value; }
        }
        public RelayCommand CheckOutCommand
        {
            get
            {
                if (checkOutCommand == null)
                    checkOutCommand = new RelayCommand(OnCheckOutCommand);
                return checkOutCommand;
            }
            set { checkOutCommand = value; }
        }
        public RelayCommand CleanCommand
        {
            get
            {
                if (cleanCommand == null)
                    cleanCommand = new RelayCommand(OnCleanCommand);
                return cleanCommand;
            }
            set { cleanCommand = value; }
        }
        public RelayCommand OutOfServiceCommand
        {
            get
            {
                if (outOfServiceCommand == null)
                    outOfServiceCommand = new RelayCommand(OnOutOfServiceCommand);
                return outOfServiceCommand;
            }
            set { outOfServiceCommand = value; }
        }
        public RelayCommand RepairedCommand
        {
            get
            {
                if (repairedCommand == null)
                    repairedCommand = new RelayCommand(OnRepairedCommand);
                return repairedCommand;
            }
            set { repairedCommand = value; }
        }
        public RelayCommand ShowCommand
        {
            get
            {
                if (showCommand == null)
                    showCommand = new RelayCommand(OnShowCommand);
                return showCommand;
            }
            set { showCommand = value; }
        }
        #endregion

        #region Constructor
        public HotelViewModel()
        {
            SetUpRoomList();
        }
        #endregion

        #region Private Methods
        private void SetUpRoomList()
        {
            for (int i = 0; i < 4; i++)
            {
                RoomList.Add(new List<Room>());

                for (int j = 0; j < 5; j++)
                {
                    switch (j)
                    {
                        case 0:
                            {
                                RoomList[i].Add(new Room { RoomNumber = (4 - i).ToString() + "E" });
                                break;
                            }

                        case 1:
                            {
                                RoomList[i].Add(new Room { RoomNumber = (4 - i).ToString() + "D" });
                                break;
                            }

                        case 2:
                            {
                                RoomList[i].Add(new Room { RoomNumber = (4 - i).ToString() + "C" });
                                break;
                            }

                        case 3:
                            {
                                RoomList[i].Add(new Room { RoomNumber = (4 - i).ToString() + "B" });
                                break;
                            }

                        case 4:
                            {
                                RoomList[i].Add(new Room { RoomNumber = (4 - i).ToString() + "A" });
                                break;
                            }
                    }
                }
            }
        }

        private void OnAssignCommand()
        {
            if (!isAllRoomFull)
            {
                ExecuteCommand();
            }        
        }

        private void OnCheckOutCommand()
        {
            if (!string.IsNullOrEmpty(selectedRoom))
            {
                ExecuteCommand(RoomStatus.Occupied, RoomStatus.Vacant);
            }     
            else
            {
                MessageBox.Show("No room selected");
            }   
        }

        private void OnCleanCommand()
        {
            if (!string.IsNullOrEmpty(selectedRoom))
            {
                ExecuteCommand(RoomStatus.Vacant, RoomStatus.Available);
            }
            else
            {
                MessageBox.Show("No room selected");
            }
        }

        private void OnOutOfServiceCommand()
        {
            if (!string.IsNullOrEmpty(selectedRoom))
            {
                ExecuteCommand(RoomStatus.Vacant, RoomStatus.Repaired);
            }
            else
            {
                MessageBox.Show("No room selected");
            }
        }

        private void OnRepairedCommand()
        {
            if (!string.IsNullOrEmpty(selectedRoom))
            {
                ExecuteCommand(RoomStatus.Repaired, RoomStatus.Vacant);
            }
            else
            {
                MessageBox.Show("No room selected");
            }
        }

        private void OnShowCommand()
        {
            availRooms = "";
            for (int i = RoomList.Count - 1; i >= 0; i--)
            {
                for (int j = RoomList[i].Count - 1; j >= 0; j--)
                {
                    if (RoomList[i][j].Status == RoomStatus.Available)
                    {
                        if (availRooms == "")
                        {
                            availRooms = RoomList[i][j].RoomNumber;
                        }
                        else
                        {
                            availRooms = availRooms + ", " + RoomList[i][j].RoomNumber;
                        }
                    }
                }
            }

            if (availRooms == "")
            {
                MessageBox.Show("No room available");
            }
            else
            {
                MessageBox.Show("Available rooms:" + availRooms);
            }
        }

        private void ExecuteCommand()
        {
            for (int i = RoomList.Count - 1; i >= 0; i--)
            {
                switch (i)
                {
                    case 3:  // level 1
                    case 1:  // level 3
                        {
                            for (int j = RoomList[i].Count - 1; j >= 0; j--)
                            {
                                if (RoomList[i][j].Status == RoomStatus.Available)
                                {
                                    if (RoomList[i][j].RoomNumber == "4A")
                                    {
                                        isAllRoomFull = true;
                                    }
                                    RoomList[i][j].Status = RoomStatus.Occupied;

                                    // Clear selection just in case
                                    RoomList[i][j].IsSelected = true;
                                    RoomList[i][j].IsSelected = false;

                                    MessageBox.Show("Room " + RoomList[i][j].RoomNumber + " assigned to guest");
                                    return;
                                }
                            }
                            break;
                        }

                    case 2:  // level 2
                    case 0:  // level 4
                        {
                            for (int j = 0; j < RoomList[i].Count; j++)
                            {
                                if (RoomList[i][j].Status == RoomStatus.Available)
                                {
                                    if (RoomList[i][j].RoomNumber == "4A")
                                    {
                                        isAllRoomFull = true;
                                    }
                                    RoomList[i][j].Status = RoomStatus.Occupied;

                                    // Clear selection just in case
                                    RoomList[i][j].IsSelected = true;
                                    RoomList[i][j].IsSelected = false;

                                    MessageBox.Show("Room " + RoomList[i][j].RoomNumber + " assigned to guest");
                                    return;
                                }
                            }
                            break;
                        }
                }
            }
        }

        private void ExecuteCommand(RoomStatus status, RoomStatus newStatus)
        {
            for (int i = RoomList.Count - 1; i >= 0; i--)
            {
                switch (i)
                {
                    case 3:  // level 1
                    case 1:  // level 3
                        {
                            for (int j = RoomList[i].Count - 1; j >= 0; j--)
                            {
                                if (RoomList[i][j].RoomNumber == selectedRoom)
                                {
                                    if (RoomList[i][j].Status == status)
                                    {
                                        RoomList[i][j].Status = newStatus;
                                        if (newStatus == RoomStatus.Available)
                                        {
                                            isAllRoomFull = false;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error, check room status");
                                    }
                                    selectedRoom = "";
                                    RoomList[i][j].IsSelected = false; // clear selection
                                    return;
                                }
                            }
                            break;
                        }

                    case 2:  // level 2
                    case 0:  // level 4
                        {
                            for (int j = 0; j < RoomList[i].Count; j++)
                            {
                                if (RoomList[i][j].RoomNumber == selectedRoom)
                                {
                                    if (RoomList[i][j].Status == status)
                                    {
                                        RoomList[i][j].Status = newStatus;
                                        if (newStatus == RoomStatus.Available)
                                        {
                                            isAllRoomFull = false;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error, check room status");
                                    }
                                    selectedRoom = "";
                                    RoomList[i][j].IsSelected = false; // clear selection
                                    return;
                                }
                            }
                            break;
                        }
                }
            }
        }
        #endregion

        #region Protected Methods
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
