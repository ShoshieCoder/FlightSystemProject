using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public abstract class FacadeBase
    {
        protected IAirlineDAO _airlineDAO = new AirlineDAOmssql();
        protected ICountryDAO _countryDAO = new CountryDAOmssql();
        protected ICustomerDAO _customerDAO = new CustomerDAOmssql();
        protected IFlightDAO _flightDAO = new FlightDAOmssql();
        protected ITicketDAO _ticketDAO = new TicketDAOmssql();
        protected IAdminDAO _adminDAO = new AdminDAOmssql();
    }
}
