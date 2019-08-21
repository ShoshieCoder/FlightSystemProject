using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public class LoggedInAdministratorFacade : FacadeBase, ILoggedInAdministratorFacade, IAnonymousUserFacade
    {
        public void CreateNewAirline(LoginToken<Admin> token, AirlineCompany airline)
        {
            if(token.user is Admin)
            {
                _airlineDAO.Add(airline);
            }
        }

        public void CreateNewCustomer(LoginToken<Admin> token, Customer customer)
        {
            if(token.user is Admin)
            {
                _customerDAO.Add(customer);
            }
        }

        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            return _airlineDAO.GetAll();
        }

        public IList<Customer> GetAllCustomers()
        {
            return _customerDAO.GetAll();
        }

        public IList<Flight> GetAllFlights()
        {
            return _flightDAO.GetAll();
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            return _flightDAO.GetAllFlightsVacancy();
        }

        public Flight GetFlightById(int id)
        {
            return _flightDAO.GetById(id);
        }

        public IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            return _flightDAO.GetFlightsByDepartureDate(departureDate);
        }

        public IList<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            return _flightDAO.GetFlightsByDestinationCountry(countryCode);
        }

        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            return _flightDAO.GetFlightsByLandingDate(landingDate);
        }

        public IList<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            return _flightDAO.GetFlightsByOriginCountry(countryCode);
        }

        public void RemoveAirline(LoginToken<Admin> token, AirlineCompany airline)
        {
            if (token.user is Admin)
            {
                _airlineDAO.Remove(airline);
            }
        }

        public void RemoveCustomer(LoginToken<Admin> token, Customer customer)
        {
            if (token.user is Admin)
            {
                _customerDAO.Remove(customer);
            }
        }

        public void UpdateAirlineDetails(LoginToken<Admin> token, AirlineCompany airline)
        {
            if (token.user is Admin)
            {
                _airlineDAO.Update(airline);
            }
        }

        public void UpdateCustomerDetails(LoginToken<Admin> token, Customer customer)
        {
            if (token.user is Admin)
            {
                _customerDAO.Update(customer);
            }
        }

        public void CreateAdmin(LoginToken<Admin> token, Admin admin)
        {
            if (token.user is Admin)
            {
                _adminDAO.Add(admin);
            }
        }
    }
}
