using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public class LoggedinAirlineFacade : FacadeBase, ILoggedinAirlineFacade, IAnonymousUserFacade
    {
        public void CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if(token.user.Id == flight.AirlineCompnayId)
            {
                _flightDAO.Remove(flight);
                foreach(Ticket t in _ticketDAO.GetAll())
                {
                    if (flight.Id == t.FlightId)
                    {
                        _ticketDAO.Remove(t);
                    }
                }
            }
        }

        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            if(token.user is AirlineCompany && token.user.Password == oldPassword)
            {
                AirlineCompany company = new AirlineCompany
                {
                    Id = token.user.Id,
                    AirlineName = token.user.AirlineName,
                    UserName = token.user.UserName,
                    Password = newPassword,
                    CountryCode = token.user.CountryCode
                };
                _airlineDAO.Update(company);
            }
        }

        public void CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if(token.user is AirlineCompany)
            {
                _flightDAO.Add(flight);
            }
        }

        public IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            IList<Flight> flights = _flightDAO.GetAll();
            return flights;
        }

        public IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token)
        {
            IList<Ticket> tickets = _ticketDAO.GetAll().Where(t => t.Id == token.user.Id).ToList();
            return tickets;
        }

        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            return _airlineDAO.GetAll();
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

        public void MofidyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        {
            if(token.user.Id == airline.Id)
            {
                _airlineDAO.Update(airline);
            }
        }

        public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if(token.user.Id == flight.AirlineCompnayId)
            {
                _flightDAO.Update(flight);
            }
        }
    }
}
