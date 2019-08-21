using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public class AirlineCompany : IPoco,IUser
    {
        public AirlineCompany()
        {
        }

        public AirlineCompany(string airlineName, string userNameName, string password, long countryCode)
        {
            AirlineName = airlineName;
            UserName = userNameName;
            Password = password;
            CountryCode = countryCode;
        }

        public Int64 Id { get; set; }
        public string AirlineName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Int64 CountryCode { get; set; }

        public override bool Equals(object obj)
        {
            AirlineCompany other = obj as AirlineCompany;
            return this == other;
        }

        public override int GetHashCode()
        {
            return (int)this.Id;
        }

        public static bool operator ==(AirlineCompany p1,AirlineCompany p2)
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

        public static bool operator !=(AirlineCompany p1, AirlineCompany p2)
        {
            return !(p1 == p2);
        }

        public override string ToString()
        {
            return $"Airline Company: {AirlineName}/n Country of origin: {CountryCode}";
        }
    }
}
