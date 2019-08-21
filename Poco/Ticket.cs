using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public class Ticket : IPoco,IUser
    {
        public Ticket()
        {
        }

        public Ticket(long flightId, long customerId)
        {
            FlightId = flightId;
            CustomerId = customerId;
        }

        public Int64 Id { get; set; }
        public Int64 FlightId { get; set; }
        public Int64 CustomerId { get; set; }

        public override bool Equals(object obj)
        {
            Ticket other = obj as Ticket;
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int)this.Id;
        }

        public override string ToString()
        {
            return $"Ticket No:{Id}, Flight:{FlightId}, Customer{CustomerId}";
        }

        public static bool operator ==(Ticket p1, Ticket p2)
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

        public static bool operator !=(Ticket p1, Ticket p2)
        {
            return !(p1 == p2);
        }
    }
}
