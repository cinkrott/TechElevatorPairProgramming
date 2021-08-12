using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        public int ReservationId { get; }
        public int SpaceId { get; }
        public int NumberOfAttendees { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public string ReservedFor { get; }

        public Reservation(int reservationId, int spaceId, int numberOfAttendees, DateTime startDate, DateTime endDate, string reservedFor)
        {
            ReservationId = reservationId;
            SpaceId = spaceId;
            NumberOfAttendees = numberOfAttendees;
            StartDate = startDate;
            EndDate = endDate;
            ReservedFor = reservedFor;
        }

        public override string ToString()
        {
            string result = $"{ReservationId} -  {SpaceId} - {NumberOfAttendees} - {StartDate} - {EndDate} - {ReservationId}";
            return result;
        }
    }
}
