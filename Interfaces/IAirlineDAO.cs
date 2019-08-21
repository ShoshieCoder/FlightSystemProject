using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public interface IAirlineDAO:IBasicDB<AirlineCompany>
    {
        AirlineCompany GetAirlineByUserame(string name);
        IList<AirlineCompany> GetAllAirlinesByCountry(int countryId);
    }
}
