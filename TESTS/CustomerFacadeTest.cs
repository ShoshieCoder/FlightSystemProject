using System;
using System.Collections.Generic;
using FlightSystemProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TESTS
{
    [TestClass]
    public class CustomerFacadeTest
    {
        [TestMethod]
        public void CancelTicketTest()
        {
            HUB.ClearDB(out LoginToken<Admin> adminToken, out LoggedInAdministratorFacade adminFacade);
            Customer newCustomer = new Customer { FirstName = "Zrobavel", LastName = "zrob", UserName = "CustomerTest", Password = "asd", Address = "Somewhere 33", PhoneNo = "9720546488245", CreditNo = "1111 2222 3333 4444" };
            adminFacade.CreateNewCustomer(adminToken, newCustomer);
            LoggedInCustomerFacade customerFacade = new LoggedInCustomerFacade();
            

            LoginToken<AirlineCompany> airlineToken = new LoginToken<AirlineCompany> { user = new AirlineCompany { AirlineName = "ELAL", UserName = "test", Password = "asd", CountryCode = 1 } };
            LoggedinAirlineFacade airlineFacade = new LoggedinAirlineFacade();
            AirlineCompany airlineCompany = new AirlineCompany { AirlineName = "ELAL", UserName = "test", Password = "amIShai", CountryCode = 1 };
            adminFacade.CreateNewAirline(adminToken, airlineCompany);
            IList<AirlineCompany> companies = adminFacade.GetAllAirlineCompanies();

            Flight flight = new Flight { AirlineCompnayId = companies[0].Id, DepartureTime = DateTime.Now, LandingTime = DateTime.Now + TimeSpan.FromDays(1), OriginCountryCode = 1, DestinationCountryCode = 2, RemainingTickets = 9001 };
            airlineFacade.CreateFlight(airlineToken, flight);

            IList<Customer> customers = adminFacade.GetAllCustomers();
            LoginToken<Customer> customerToken = new LoginToken<Customer> { user = new Customer { Id = customers[0].Id } };
            IList<Flight> flights = customerFacade.GetAllFlights();
            flight.Id = flights[0].Id;

            customerFacade.PurchaseTicket(customerToken, flight);
            
            Assert.AreEqual(customerFacade.GetAllMyFlights(customerToken).Count, 1);

            IList<Ticket> tickets = customerFacade.GetAllTickets(customerToken);
            customerFacade.CancelTicket(customerToken, tickets[0]);
            Assert.AreEqual(customerFacade.GetAllMyFlights(customerToken).Count, 0);
        }
    }
}
