using SaasTeamTest.Models;

namespace SaasTeamTest.Controller
{
    class VacantRoomHandle : RoomHandle
    {
        public VacantRoomHandle(Room room)
            : base(room)
        {
            Room.Status = RoomStatus.Vacant;
        }

        public RoomHandle Clean()
        {
            return new AvailableRoomHandle(Room);
        }

        public RoomHandle RequestRepair()
        {
            return new RepairingRoomHandle(Room);
        }

        public override bool CanAccess(UserRole role)
        {
            return role == UserRole.HouseCleaner ||
                   role == UserRole.Staff;
        }
    }
}
