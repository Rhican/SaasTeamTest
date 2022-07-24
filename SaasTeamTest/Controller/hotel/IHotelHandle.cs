using SaasTeamTest.Models;
using System.Collections.Generic;

namespace SaasTeamTest.Controller.hotel
{
    public interface IHotelHandle
    {
        /// <summary>
        /// Quick CheckIn a guest with the next available room
        ///     item 1: method requesting for room assignment, which reply with the assigned room number upon success.
        ///     
        /// Exception will through if there is no more room available
        /// </summary>
        /// <returns>the Room checkined(assigned)</returns>
        public Room QuickCheckIn();

        /// <summary>
        /// Check out the room, only if it is Occupied currently.
        ///     item 2: method to check out of a room.
        /// </summary>
        /// <param name="room">the Room</param>
        /// <returns>true if it is successful</returns>
        public bool Checkout(Room room);

        /// <summary>
        /// Clean the room, only if it is Vacant currently.
        ///     item 3: method to mark a room cleaned (Available).
        /// </summary>
        /// <param name="room">the Room</param>
        /// <returns>true if it is successful</returns>
        public bool Clean(Room room);

        /// <summary>
        /// Request Repair for the room, only if it is Vacant currently.
        ///     item 4: method to mark a room for repair.
        /// </summary>
        /// <param name="room">the Room</param>
        /// <returns>true if it is successful</returns>
        public bool RequestRepair(Room room);

        /// <summary>
        /// List All the available rooms currently.
        ///     item 5: method to list all the available rooms.
        /// </summary>
        /// <returns>the list of rooms which are available</returns>
        public IList<Room> AvailableRooms();


        /// <summary>
        /// Complete Repairing to promote to Vacant room.
        /// </summary>
        /// <param name="room">the Room</param>
        /// <returns>true if it is successful</returns>
        public bool CompleteRepairing(Room room);
    }
}