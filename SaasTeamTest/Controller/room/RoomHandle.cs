﻿using SaasTeamTest.Models;

namespace SaasTeamTest.Controller
{
    public abstract class RoomHandle
    {
        public RoomHandle(Room room)
        {
            Room = room;
        }
        public Room Room { get; private set; }

        public virtual bool CanAccess(UserRole role)
        {
            return false;
        }
    }
}
