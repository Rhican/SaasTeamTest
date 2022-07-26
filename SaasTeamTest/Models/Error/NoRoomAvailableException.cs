using System;

namespace SaasTeamTest.Models.Error
{
    public class NoRoomAvailableException : Exception
    {
        public NoRoomAvailableException()
            : base("No Room is Available!")
        {
        }
    }
}
