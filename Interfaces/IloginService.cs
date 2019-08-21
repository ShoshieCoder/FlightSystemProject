using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    interface IloginService
    {
        bool TryAdminLogin(string username, string password, out LoginToken<Admin> token);
        bool TryCustomerLogin(string username, string password, out LoginToken<Customer> token);
        bool TryAirlineLogin(string username, string password, out LoginToken<AirlineCompany> token);
    }
}
