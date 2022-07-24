﻿using SaasTeamTest.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaasTeamTest.Controller
{
    class AvailableRoomHandle : RoomHandle
    {
        public AvailableRoomHandle(Room room)
            : base(room)
        {            
            Room.Status = RoomStatus.Available;
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