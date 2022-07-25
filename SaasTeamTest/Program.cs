using SaasTeamTest.Controller.hotel;
using SaasTeamTest.Models;
using System;
using System.Collections.Generic;

namespace SaasTeamTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IHotelHandle hotelHandle = new HotelHandle("Saas");

            Room room1 = hotelHandle.QuickCheckIn();
            hotelHandle.Checkout(room1);
            hotelHandle.Clean(room1);

            Room room2 = hotelHandle.QuickCheckIn();
            if (room2 == room1)
                Console.WriteLine("Room#" + room1 + " has been re-checkin via quick checkin.");
            else
                Console.WriteLine("Room#" + room1 + " has failed to re-checkin via quick checkin.");

            Random rand = new Random();
            int n = 5 * 4 - 1;
            List<Room> roomsToClean = new List<Room>();
            for (int i = 0; i < n; i++)
            {
                Room room = hotelHandle.QuickCheckIn();
                int targetStatus = rand.Next(0, (int)RoomStatus.Repair + 1);
                switch ((RoomStatus)targetStatus)
                {
                    case RoomStatus.Available:
                        hotelHandle.Checkout(room);
                        //hotelHandle.Clean(room); // clean later so random test can continue
                        roomsToClean.Add(room);
                        break;
                    case RoomStatus.Occupied:
                        break;
                    case RoomStatus.Repair:
                        hotelHandle.Checkout(room);
                        hotelHandle.RequestRepair(room);
                        if (rand.Next(0, 2) == 1)
                            hotelHandle.CompleteRepairing(room);
                        break;
                    case RoomStatus.Vacant:
                        hotelHandle.Checkout(room);
                        break;
                }
            }

            var noAvailableRooms = hotelHandle.AvailableRooms();
            if (noAvailableRooms.Count == 0)
                Console.WriteLine("No Room is Available!");

            try
            {
                hotelHandle.QuickCheckIn();
            }
            catch(Models.Error.NoRoomAvailableException)
            {
                Console.WriteLine("QuickCheckIn can not work when there is no room available!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            foreach (var room in roomsToClean)
            {
                hotelHandle.Clean(room);
            }

            var availableRooms = hotelHandle.AvailableRooms();

            foreach (var room in availableRooms)
            {
                Console.WriteLine("Room #" + room + " is " + room.Status.ToString());
            }
        }
    }
}
