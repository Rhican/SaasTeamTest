using SaasTeamTest.Models;

namespace SaasTeamTest.Controller
{
    class OccupiedRoomHandle : RoomHandle
    {
        public OccupiedRoomHandle(Room room)
            : base(room)
        {
            Room.Status = RoomStatus.Occupied;
        }

        public RoomHandle CheckOut()
        {
            return new VacantRoomHandle(Room);
        }

        public override bool CanAccess(UserRole role)
        {
            return role == UserRole.Guest || 
                   role == UserRole.HouseCleaner;
        }
    }
}
