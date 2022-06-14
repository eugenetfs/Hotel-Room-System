using Hotel_Room_System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UTest
{
    [TestClass]
    public class HotelTest
    {
        HotelViewModel viewModel = new HotelViewModel();

        [TestMethod]
        public void TestAssign()
        {
            // Arrange
            PrivateObject obj = new PrivateObject(viewModel);

            // Act
            obj.Invoke("OnAssignCommand");

            // Assert
            Assert.AreEqual(RoomStatus.Occupied, viewModel.RoomList[3][4].Status);
        }

        [TestMethod]
        public void TestCheckOut()
        {
            // Arrange
            PrivateObject obj = new PrivateObject(viewModel);
            viewModel.selectedRoom = "4E";
            viewModel.RoomList[0][0].Status = RoomStatus.Occupied;

            // Act
            obj.Invoke("OnCheckOutCommand");

            // Assert
            Assert.AreEqual(RoomStatus.Vacant, viewModel.RoomList[0][0].Status);
        }

        [TestMethod]
        public void TestClean()
        {
            // Arrange
            PrivateObject obj = new PrivateObject(viewModel);
            viewModel.selectedRoom = "4E";
            viewModel.RoomList[0][0].Status = RoomStatus.Vacant;

            // Act
            obj.Invoke("OnCleanCommand");

            // Assert
            Assert.AreEqual(RoomStatus.Available, viewModel.RoomList[0][0].Status);
        }

        [TestMethod]
        public void TestOutOfService()
        {
            // Arrange
            PrivateObject obj = new PrivateObject(viewModel);
            viewModel.selectedRoom = "4E";
            viewModel.RoomList[0][0].Status = RoomStatus.Vacant;

            // Act
            obj.Invoke("OnOutOfServiceCommand");

            // Assert
            Assert.AreEqual(RoomStatus.Repaired, viewModel.RoomList[0][0].Status);
        }

        [TestMethod]
        public void TestRepaired()
        {
            // Arrange
            PrivateObject obj = new PrivateObject(viewModel);
            viewModel.selectedRoom = "4E";
            viewModel.RoomList[0][0].Status = RoomStatus.Repaired;

            // Act
            obj.Invoke("OnRepairedCommand");

            // Assert
            Assert.AreEqual(RoomStatus.Vacant, viewModel.RoomList[0][0].Status);
        }

        [TestMethod]
        public void TestShowAvailableRoom()
        {
            // Arrange
            PrivateObject obj = new PrivateObject(viewModel);
            // Assign level 4 all with occupied rooms
            viewModel.RoomList[0][0].Status = RoomStatus.Occupied;
            viewModel.RoomList[0][1].Status = RoomStatus.Occupied;
            viewModel.RoomList[0][2].Status = RoomStatus.Occupied;
            viewModel.RoomList[0][3].Status = RoomStatus.Occupied;
            viewModel.RoomList[0][4].Status = RoomStatus.Occupied;
            // Assign level 3 all with occupied rooms
            viewModel.RoomList[1][0].Status = RoomStatus.Occupied;
            viewModel.RoomList[1][1].Status = RoomStatus.Occupied;
            viewModel.RoomList[1][2].Status = RoomStatus.Occupied;
            viewModel.RoomList[1][3].Status = RoomStatus.Occupied;
            viewModel.RoomList[1][4].Status = RoomStatus.Occupied;
            // Assign level 2 all with occupied rooms
            viewModel.RoomList[2][0].Status = RoomStatus.Occupied;
            viewModel.RoomList[2][1].Status = RoomStatus.Occupied;
            viewModel.RoomList[2][2].Status = RoomStatus.Occupied;
            viewModel.RoomList[2][3].Status = RoomStatus.Occupied;
            viewModel.RoomList[2][4].Status = RoomStatus.Occupied;

            // Act
            obj.Invoke("OnShowCommand");

            // Assert
            Assert.AreEqual("1A, 1B, 1C, 1D, 1E", viewModel.availRooms);
        }
    }
}
