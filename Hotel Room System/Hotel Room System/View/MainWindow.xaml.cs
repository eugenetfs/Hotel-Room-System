using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("UTest")]
namespace Hotel_Room_System
{
    public partial class MainWindow : Window
    {
        HotelViewModel viewModel = new HotelViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            viewModel.selectedRoom = button.Content.ToString();
        }
    }
}
