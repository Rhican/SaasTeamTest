using SaasTeamTest.Models;
using System;

namespace SaasTeamTest.Controller
{
    public class RepairingRoomHandle : RoomHandle
    {
        public RepairingRoomHandle(Room room)
            : base(room)
        {
            Room.Status = RoomStatus.Repair;
            Console.WriteLine("Room #" + room + " is now " + room.Status);
        }

        public RoomHandle Complete()
        {
            return new VacantRoomHandle(Room);
        }

        public override bool CanAccess(UserRole role)
        {
            return role == UserRole.Repairer ||
                   role == UserRole.Staff;
        }
    }
}
