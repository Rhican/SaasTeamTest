using SaasTeamTest.Models;

namespace SaasTeamTest.Controller
{
    class RepairingRoomHandle : RoomHandle
    {
        public RepairingRoomHandle(Room room)
            : base(room)
        {
            Room.Status = RoomStatus.Repair;
        }

        public RoomHandle Complete()
        {
            return new VacantRoomHandle(Room);
        }

        public override bool CanAccess(UserRole role)
        {
            return role == UserRole.Repairer ||
                   role == UserRole.Staff;
        }
    }
}
