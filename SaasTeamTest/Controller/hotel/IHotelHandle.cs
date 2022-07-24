using SaasTeamTest.Models;
using System.Collections.Generic;

namespace SaasTeamTest.Controller.hotel
{
    public interface IHotelHandle
    {
        public Room QuickCheckIn();
        public bool Checkout(Room room);
        public bool Clean(Room room);
        public bool RequestRepair(Room room);
        public IList<Room> AvailableRooms();

        public bool CompleteRepairing(Room room);
    }
}
