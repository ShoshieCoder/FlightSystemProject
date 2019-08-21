using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public class LoggedInCustomerFacade : FacadeBase, ILoggedInCustomerFacade, IAnonymousUserFacade
    {
        public IList<Ticket> GetAllTickets(LoginToken<Customer> token)
        {
            IList<Ticket> tickets = null;
            if (token.user is Customer)
            {
                tickets = _ticketDAO.GetAll();
            }
            return tickets;
        }


        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            if(token.user.Id == ticket.CustomerId)
            {
                _ticketDAO.Remove(ticket);
            }
        }

        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            IList<Flight> flights = null;

            if(token.user is Customer)
            {
                flights = _flightDAO.GetFlightsByCustomer(token.user);
            }
            return flights;
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

        public Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            Ticket ticket = null;

            if (token.user is Customer)
            {
                
                if (flight.RemainingTickets > 0)
                {
                    _ticketDAO.Add(new Ticket { CustomerId = token.user.Id, FlightId = flight.Id });
                    flight.RemainingTickets--;
                    _flightDAO.Update(flight);
                }
                else
                {
                    throw new OutOfTicketsException("No tickets");
                }
            }

            return ticket;
        }
    }
}
