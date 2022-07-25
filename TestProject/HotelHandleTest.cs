using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasTeamTest.Controller.hotel;
using SaasTeamTest.Models;

namespace TestProject
{
    [TestClass]
    public class HotelHandleTest
    {
        private IHotelHandle hotelHandle;
        private Room room;

        [TestInitialize]
        public void Initialize()
        {
            hotelHandle = new HotelHandle("Saas");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // No-op
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("Hotel")]
        [Timeout(100)]
        public void Test_1_ListAvailableRooms()
        {
            var rooms = hotelHandle.AvailableRooms();
            Assert.IsTrue(rooms.Count > 0, "Could not find any avaialble room!");
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("Hotel")]
        [Timeout(100)]
        public void Test_2_QuickCheckIn()
        {
            room = hotelHandle.QuickCheckIn();
            Assert.IsTrue(room != null && room.Status == RoomStatus.Occupied, "Failed to quick checkin.");
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("Hotel")]
        public void Test_3_CheckOut()
        {
            Test_2_QuickCheckIn();

            Assert.IsTrue(room != null && room.Status == RoomStatus.Occupied, "Room is expected to be Occupied! but it is " + room.Status);
            hotelHandle.Checkout(room);
            Assert.IsTrue(room != null && room.Status == RoomStatus.Vacant, "Failed to Check out.");
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("Hotel")]
        public void Test_4_RequestRepair()
        {
            Test_3_CheckOut();

            Assert.IsTrue(room != null && room.Status == RoomStatus.Vacant, "Room is expected to be Vacant! but it is " + room.Status);
            hotelHandle.RequestRepair(room);
            Assert.IsTrue(room != null && room.Status == RoomStatus.Repair, "Failed to set for reparing.");
            hotelHandle.CompleteRepairing(room);
            Assert.IsTrue(room != null && room.Status == RoomStatus.Vacant, "Failed to recover to vacant.");
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("Hotel")]
        public void Test_4_Clean()
        {
            Test_3_CheckOut();

            Assert.IsTrue(room != null && room.Status == RoomStatus.Vacant, "Room is expected to be Vacant! but it is " + room.Status);
            hotelHandle.Clean(room);
            Assert.IsTrue(room != null && room.Status == RoomStatus.Available, "Failed to set for Available after cleaning.");
        }
    }
}
