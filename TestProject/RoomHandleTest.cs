using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasTeamTest.Controller;
using SaasTeamTest.Models;

namespace TestProject
{
    [TestClass]
    public class RoomHandleTest
    {
        private Floor floor;
        private Room roomA, roomB;

        [TestInitialize]
        public void Initialize()
        {
            floor = new Floor(1);
            roomA = new Room(floor, 'A');
            roomB = new Room(floor, 'B');
        }

        [TestCleanup]
        public void Cleanup()
        {
            // No-op
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("RoomHandle")]
        public void StateAvailableToOccupied()
        {
            var roomHandle = new AvailableRoomHandle(roomA);
            RoomHandle newHandle = roomHandle.CheckIn();
            Assert.IsTrue(typeof(OccupiedRoomHandle).IsInstanceOfType(newHandle), "Failed, expected to be occupied, but it is " + roomA.Status);
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("RoomHandle")]
        public void StateAvailableToVacant()
        {
            var roomHandle = new OccupiedRoomHandle(roomA);
            RoomHandle newHandle = roomHandle.CheckOut();
            Assert.IsTrue(typeof(VacantRoomHandle).IsInstanceOfType(newHandle), "Failed, expected to be Vacant, but it is " + roomA.Status);
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("RoomHandle")]
        public void StateVacantToAvailable()
        {
            var roomHandle = new VacantRoomHandle(roomA);
            RoomHandle newHandle = roomHandle.Clean();
            Assert.IsTrue(typeof(AvailableRoomHandle).IsInstanceOfType(newHandle), "Failed, expected to be Available, but it is " + roomA.Status);
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("RoomHandle")]
        public void StateVacantToRepair()
        {
            var roomHandle = new VacantRoomHandle(roomA);
            RoomHandle newHandle = roomHandle.RequestRepair();
            Assert.IsTrue(typeof(RepairingRoomHandle).IsInstanceOfType(newHandle), "Failed, expected to be Repairing, but it is " + roomA.Status);
        }

        [TestMethod]
        [Owner("ZehuaRong")]
        [TestCategory("RoomHandle")]
        public void StateRepairToVacant()
        {
            var roomHandle = new RepairingRoomHandle(roomA);
            RoomHandle newHandle = roomHandle.Complete();
            Assert.IsTrue(typeof(VacantRoomHandle).IsInstanceOfType(newHandle), "Failed, expected to be Repairing, but it is " + roomA.Status);
        }
    }
}
