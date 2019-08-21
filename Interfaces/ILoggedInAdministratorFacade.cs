using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public interface ILoggedInAdministratorFacade
    {
        void CreateNewAirline(LoginToken<Admin> token, AirlineCompany airline);
        void UpdateAirlineDetails(LoginToken<Admin> token, AirlineCompany customer);
        void RemoveAirline(LoginToken<Admin> token, AirlineCompany airline);
        void CreateNewCustomer(LoginToken<Admin> token, Customer customer);
        void UpdateCustomerDetails(LoginToken<Admin> token, Customer customer);
        void RemoveCustomer(LoginToken<Admin> token, Customer customer);
    }
}
