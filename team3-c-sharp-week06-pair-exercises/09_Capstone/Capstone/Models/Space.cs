using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Space
    {
        public int Id {get;}
        public int VenueId { get; }
        public string Name { get; }
        public bool IsAccessible { get; }
        public int OpenFrom { get; }
        public int OpenTo { get; }
        public decimal DailyRate { get; }
        public int MaxOccupancy { get; }

        //public Space(int id, int venueId, string name, bool isAccessible, decimal dailyRate, int maxOccupany)
        //{
        //    Id = id;
        //    VenueId = venueId;
        //    Name = name;
        //    IsAccessible = isAccessible;
        //    DailyRate = dailyRate;
        //    MaxOccupancy = maxOccupany;
        //}

        public Space(int id, int venueId, string name, bool isAccessible, int openFrom, int openTo, decimal dailyRate, int maxOccupany)
        {
            Id = id;
            VenueId = venueId;
            Name = name;
            IsAccessible = isAccessible;
            OpenFrom = openFrom;
            OpenTo = openTo;
            DailyRate = dailyRate;
            MaxOccupancy = maxOccupany;
        }

        public override string ToString()
        {
            string result = $"{Id} - {VenueId} - {Name} - {IsAccessible} - {OpenFrom} - {OpenTo} - ${DailyRate} - {MaxOccupancy}";
            return result;
        }


    }
}
