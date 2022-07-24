using System;
using System.Collections.Generic;
using System.Text;

namespace SaasTeamTest.Models
{
    public class Room
    {
        public Room(Floor level, Char code)
        {
            Floor = level;
            Name = code;
            Status = RoomStatus.Available;
        }

        public override string ToString() => Floor.ToString() + Name;

        public override bool Equals(object obj)
        {
            return ToString().Equals(obj.ToString());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public Char Name { get; protected set; }
        public RoomStatus Status { get; set; }

        private readonly Floor Floor;
    }
}
