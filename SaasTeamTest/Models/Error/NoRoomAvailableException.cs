using System;
using System.Collections.Generic;
using System.Text;

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
