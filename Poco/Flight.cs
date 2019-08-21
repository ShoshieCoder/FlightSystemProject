using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public class Flight : IPoco,IUser
    {
        public Flight()
        {
        }

        public Flight(long airlineCompnayId, long originCountryCode, long destinationCountryCode, DateTime departureTime, DateTime landingTime, int remainingTickets)
        {
            AirlineCompnayId = airlineCompnayId;
            OriginCountryCode = originCountryCode;
            DestinationCountryCode = destinationCountryCode;
            DepartureTime = departureTime;
            LandingTime = landingTime;
            RemainingTickets = remainingTickets;
        }

        public Int64 Id { get; set; }
        public Int64 AirlineCompnayId { get; set; }
        public Int64 OriginCountryCode { get; set; }
        public Int64 DestinationCountryCode { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public int RemainingTickets { get; set; }

        public override bool Equals(object obj)
        {
            Flight other = obj as Flight;
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int)this.Id;
        }

        public override string ToString()
        {
            return $"Fligh: {Id}, Airline: {AirlineCompnayId}, From:{OriginCountryCode}, To:{DestinationCountryCode}, Departure{DepartureTime}, Arriving(aprox):{LandingTime}";
        }

        public static bool operator ==(Flight p1, Flight p2)
        {
            if (ReferenceEquals(p1, null) && ReferenceEquals(p2, null))
            {
                return true;
            }

            if (ReferenceEquals(p1, null) || ReferenceEquals(p2, null))
            {
                return false;
            }

            if (p1.Id == p2.Id)
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(Flight p1, Flight p2)
        {
            return !(p1 == p2);
        }
    }
}
