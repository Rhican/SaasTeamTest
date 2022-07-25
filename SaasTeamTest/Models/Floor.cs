using System;
using System.Collections.Generic;
using System.Linq;

namespace SaasTeamTest.Models
{
    public class Floor
    {
        public Floor(int number)
        {
            Number = number;
            mRooms = new HashSet<Room>();
        }

        public int Number { get; private set; }
        public int RoomCount => mRooms.Count;
        public ICollection<Room> Rooms() => mRooms;

        public bool Add(Room room)
        {
            return mRooms.Add(room);
        }
        public bool Remove(Room room)
        {
            return mRooms.Remove(room);
        }
        public Room Find(Func<Room, bool> predicate)
        {
            return mRooms.First(predicate);
        }

        public override string ToString() => ("" + Number);
        public override bool Equals(object obj)
        {
            return ToString().Equals(obj.ToString());
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        private HashSet<Room> mRooms;
    }
}