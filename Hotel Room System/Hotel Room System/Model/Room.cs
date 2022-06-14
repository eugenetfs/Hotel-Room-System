using System.ComponentModel;

namespace Hotel_Room_System
{
    public class Room : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string roomNumber;
        private bool isSelected;
        private RoomStatus status;

        public string RoomNumber
        {
            get { return roomNumber; }
            set
            {
                roomNumber = value;
                OnPropertyChanged("RoomNumber");
            }
        }
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        public RoomStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }
    }
}
