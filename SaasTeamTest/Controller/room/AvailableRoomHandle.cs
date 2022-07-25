using SaasTeamTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaasTeamTest.Controller
{
    public class AvailableRoomHandle : RoomHandle
    {
        public AvailableRoomHandle(Room room)
            : base(room)
        {            
            Room.Status = RoomStatus.Available;
            Console.WriteLine("Room #" + room + " is now " + room.Status);
        }

        public RoomHandle CheckIn()
        {
            return new OccupiedRoomHandle(Room);
        }

        public override bool CanAccess(UserRole role)
        {
            return role != UserRole.Repairer;
        }
    }
}
