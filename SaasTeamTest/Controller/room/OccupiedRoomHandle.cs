using SaasTeamTest.Models;
using System;

namespace SaasTeamTest.Controller
{
    class OccupiedRoomHandle : RoomHandle
    {
        public OccupiedRoomHandle(Room room)
            : base(room)
        {
            Room.Status = RoomStatus.Occupied;
            Console.WriteLine("Room #" + room + " is now " + room.Status);
        }

        public RoomHandle CheckOut()
        {
            return new VacantRoomHandle(Room);
        }

        public override bool CanAccess(UserRole role)
        {
            return role == UserRole.Guest || 
                   role == UserRole.HouseCleaner;
        }
    }
}
