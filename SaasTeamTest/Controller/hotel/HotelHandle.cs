using SaasTeamTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaasTeamTest.Controller.hotel
{
    sealed class HotelHandle : IHotelHandle
    {
        /// <summary>
        /// Default constructor,
        ///     Build hotel data as the assignment
        /// </summary>
        /// <param name="name">Hotel Name</param>
        public HotelHandle(String name)
        {
            Hotel = new Hotel(name);
            int floor = 4;
            int room = 5;
            Initialize(floor, room);
            SetupPath(floor, room);
            LoadRoomHandles();
        }

        /// <summary>
        /// Load Room Handles
        ///     For Demo App, this part does not load room status from persistent sources.
        /// </summary>
        private void LoadRoomHandles()
        {
            Console.WriteLine("Loading Room Hanldes...");
            Handles = new Dictionary<Room, RoomHandle>();
            foreach (var room in Path)
            {
                RoomHandle handle = null;
                switch (room.Status)
                {
                    case RoomStatus.Available:
                        handle = new AvailableRoomHandle(room);
                        break;
                    case RoomStatus.Occupied:
                        handle = new OccupiedRoomHandle(room);
                        break;
                    case RoomStatus.Repair:
                        handle = new RepairingRoomHandle(room);
                        break;
                    default:
                        handle = new VacantRoomHandle(room);
                        break;
                }
                Handles.Add(room, handle);
            }
            Console.WriteLine("Room Hanldes are loaded\n\n");
        }

        /// <summary>
        /// Setup Path from entrance, according to the given path
        ///     Note: all rooms in each floor are always same
        /// </summary>
        /// <param name="floors">floor count</param>
        /// <param name="roomsPerFloor">room count per floor</param>
        private void SetupPath(int floors, int roomsPerFloor)
        {
            Console.WriteLine("Setting up Path (" + floors + "x" + roomsPerFloor + ")");
            Path = new LinkedList<Room>();

            for (int f = 1; f <= floors; f++)
            {
                try
                {
                    var floor = Hotel.Find(flr => flr.Number == f);
                    for (int r = 0; r < roomsPerFloor; r++)
                    {
                        Char code = (char)('A' + r);
                        if (f % 2 == 0)
                        {
                            code = (char)('A' + (roomsPerFloor - 1) - r);
                        }
                        var room = floor.Find(r => r.Name == code);
                        if (room != null)
                            Path.AddLast(room);
                    }
                }
                catch (ArgumentNullException)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in setup Path ", ex);
                    break;
                }
            }
        }

        public void Initialize(int floors, int roomsPerFloor)
        {
            Console.WriteLine("Initializing Rooms (" + floors + "x" + roomsPerFloor + ")");
            for (int i = 1; i <= floors; i++)
            {
                Floor f = new Floor(i);
                Hotel.Add(f);

                for (int j = 0; j < roomsPerFloor; j++)
                {
                    Room r = new Room(f, (char)('A' + j));
                    f.Add(r);
                }
            }
        }

        public Room QuickCheckIn()
        {
            Room nextAvailableRoom = Path.FirstOrDefault(r => r.Status == RoomStatus.Available);
            if (nextAvailableRoom == null) throw new Exception("No Room is Available!");
            bool found = Handles.TryGetValue(nextAvailableRoom, out RoomHandle handle);
            if (handle == null || !found)
            {
                handle = new AvailableRoomHandle(nextAvailableRoom);
                Handles[nextAvailableRoom] = handle;
            }

            if (handle.CanAccess(UserRole.Staff))
            {
                Handles[nextAvailableRoom] = ((AvailableRoomHandle)handle).CheckIn();
            }

            return nextAvailableRoom;
        }

        public bool Checkout(Room room)
        {
            if (Handles.TryGetValue(room, out RoomHandle handle))
            {
                if (handle.CanAccess(UserRole.Guest))
                {
                    Handles[room] = ((OccupiedRoomHandle)handle).CheckOut();
                    return Handles[room] != null;
                }
            }
            return false;
        }

        public bool Clean(Room room)
        {
            if (Handles.TryGetValue(room, out RoomHandle handle))
            {
                if (handle.CanAccess(UserRole.HouseCleaner))
                {
                    Handles[room] = ((VacantRoomHandle)handle).Clean();
                    return Handles[room] != null;
                }
            }
            return false;
        }

        public bool RequestRepair(Room room)
        {
            if (Handles.TryGetValue(room, out RoomHandle handle))
            {
                if (handle.CanAccess(UserRole.Staff))
                {
                    Handles[room] = ((VacantRoomHandle)handle).RequestRepair();
                    return Handles[room] != null;
                }
            }
            return false;
        }

        public IList<Room> AvailableRooms()
        {
            return Path.Where(r => r.Status == RoomStatus.Available).ToList();
        }


        public bool CompleteRepairing(Room room)
        {
            if (Handles.TryGetValue(room, out RoomHandle handle))
            {
                if (handle.CanAccess(UserRole.Repairer))
                {
                    Handles[room] = ((RepairingRoomHandle)handle).Complete();
                    return Handles[room] != null;
                }
            }
            return false;
        }

        private readonly Hotel Hotel;

        private LinkedList<Room> Path;

        private IDictionary<Room, RoomHandle> Handles;
    }
}
