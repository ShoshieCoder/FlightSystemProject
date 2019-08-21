using System;
using System.Collections.Generic;
using FlightSystemProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TESTS
{
    [TestClass]
    public class AirlineFacadeTest
    {
        [TestMethod]
        public void CancelFlight()
        {
            HUB.ClearDB(out LoginToken<Admin> adminToken, out LoggedInAdministratorFacade adminFacade);
            LoginToken<AirlineCompany> airlineToken = new LoginToken<AirlineCompany> { user = new AirlineCompany { AirlineName = "TestAirline", UserName = "test", Password = "asd", CountryCode = 1 } };
            LoggedinAirlineFacade airlineFacade = new LoggedinAirlineFacade();
            AirlineCompany airlineCompany = new AirlineCompany { AirlineName = "ELAL", UserName = "ELAL", Password = "amIShai", CountryCode = 1 };
            adminFacade.CreateNewAirline(adminToken, airlineCompany);
            IList<AirlineCompany> companies = adminFacade.GetAllAirlineCompanies();
            Flight flight = new Flight { AirlineCompnayId = companies[0].Id, DepartureTime = DateTime.Now, LandingTime = DateTime.Now + TimeSpan.FromDays(1), OriginCountryCode = 1, DestinationCountryCode = 2, RemainingTickets = 9001 };
            airlineFacade.CreateFlight(airlineToken, flight);
            IList<Flight> flights = airlineFacade.GetAllFlights();
            Assert.AreEqual(1, flights.Count);
            airlineToken.user.Id = companies[0].Id;
            airlineFacade.CancelFlight(airlineToken, flights[0]);
            flights = airlineFacade.GetAllFlights(airlineToken);
            Assert.AreEqual(0, flights.Count);
        }

        [TestMethod]
        public void ChangePasswordTest()
        {
            HUB.ClearDB(out LoginToken<Admin> adminToken, out LoggedInAdministratorFacade adminFacade);
            AirlineCompany airlineCompany = new AirlineCompany { AirlineName = "ELAL", UserName = "ELAL", Password = "amIShai", CountryCode = 1 };
            adminFacade.CreateNewAirline(adminToken, airlineCompany);
            IList<AirlineCompany> companies = adminFacade.GetAllAirlineCompanies();
            LoggedinAirlineFacade airlineFacade = new LoggedinAirlineFacade();
            LoginToken<AirlineCompany> airlineToken = new LoginToken<AirlineCompany> { user = new AirlineCompany { Id=companies[0].Id, AirlineName = "ELAL", UserName = "ELAL", Password = "amIShai", CountryCode = 1 } };
            airlineFacade.ChangeMyPassword(airlineToken, "amIShai", "new");
            companies = adminFacade.GetAllAirlineCompanies();
            Assert.AreEqual(companies[0].Password, "new");
        }

        [TestMethod]
        public void UpdateTest()
        {
            HUB.ClearDB(out LoginToken<Admin> adminToken, out LoggedInAdministratorFacade adminFacade);
            AirlineCompany airlineCompany = new AirlineCompany { AirlineName = "ELAL", UserName = "ELAL", Password = "amIShai", CountryCode = 1 };
            adminFacade.CreateNewAirline(adminToken, airlineCompany);
            IList<AirlineCompany> companies = adminFacade.GetAllAirlineCompanies();
            Flight flight = new Flight { AirlineCompnayId = companies[0].Id, DepartureTime = DateTime.Now, LandingTime = DateTime.Now + TimeSpan.FromDays(1), OriginCountryCode = 1, DestinationCountryCode = 2, RemainingTickets = 9001 };
            LoggedinAirlineFacade airlineFacade = new LoggedinAirlineFacade();
            LoginToken<AirlineCompany> airlineToken = new LoginToken<AirlineCompany> { user = new AirlineCompany { Id=companies[0].Id, AirlineName = "TestAirline", UserName = "test", Password = "asd", CountryCode = 1 } };
            airlineFacade.CreateFlight(airlineToken, flight);
            flight.RemainingTickets = 9000;
            IList<Flight> flights = airlineFacade.GetAllFlights(airlineToken);
            airlineToken.user.Id = flights[0].AirlineCompnayId;
            flight.Id = flights[0].Id;
            airlineFacade.UpdateFlight(airlineToken, flight);
            IList<Flight> flightsUpdated = airlineFacade.GetAllFlights(airlineToken);
            Assert.AreEqual(flightsUpdated[0].RemainingTickets, flight.RemainingTickets);
        }
    }
}
