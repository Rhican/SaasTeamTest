using System;
using System.Collections.Generic;
using System.Linq;

namespace SaasTeamTest.Models
{
    class Hotel
    {
        public Hotel(string name)
        {
            Name = name;
        }

        public int FloorCount => mFloors.Count;
        public ICollection<Floor> Floors() => mFloors;

        public bool Add(Floor Floor)
        {
            return mFloors.Add(Floor);
        }

        public bool Remove(Floor Floor)
        {
            return mFloors.Remove(Floor);
        }
        public Floor Find(Func<Floor, bool> predicate)
        {
            return mFloors.First(predicate);
        }

        public string Name { get; private set; }
        private HashSet<Floor> mFloors = new HashSet<Floor>();
    }
}
