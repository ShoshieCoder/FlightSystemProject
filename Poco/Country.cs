using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public class Country : IPoco,IUser
    {
        public Country()
        {
        }

        public Country(string countryName)
        {
            CountryName = countryName;
        }

        public Int64 Id { get; set; }
        public string CountryName { get; set; }

        public override bool Equals(object obj)
        {
            Country other = obj as Country;
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int)this.Id;
        }

        public override string ToString()
        {
            return $"{CountryName}";
        }

        public static bool operator ==(Country p1, Country p2)
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

        public static bool operator !=(Country p1, Country p2)
        {
            return !(p1 == p2);
        }
    }
}
