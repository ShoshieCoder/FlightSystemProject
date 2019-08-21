using System;
using System.Collections.Generic;
using FlightSystemProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TESTS
{
    [TestClass]
    public class AdminFacadeTest
    {
        [TestMethod]
        public void RemoveAirlineTest()
        {
            HUB.ClearDB(out LoginToken<Admin> adminToken, out LoggedInAdministratorFacade adminFacade);
            AirlineCompany company = new AirlineCompany { AirlineName = "test", UserName = "test", Password = "asd", CountryCode = 1 };
            adminFacade.CreateNewAirline(adminToken, company);
            IList<AirlineCompany> companies = adminFacade.GetAllAirlineCompanies();
            company.Id = companies[0].Id;
            adminFacade.RemoveAirline(adminToken, company);
            IList<AirlineCompany> Nocompanies = adminFacade.GetAllAirlineCompanies();
            Assert.AreEqual(Nocompanies.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void RemoveAirlineExceptionTest()
        {
            HUB.ClearDB(out LoginToken<Admin> adminToken, out LoggedInAdministratorFacade adminFacade);
            AirlineCompany company = new AirlineCompany { AirlineName = "test", UserName = "test", Password = "asd", CountryCode = 1 };
            adminFacade.CreateNewAirline(adminToken, company);
            adminFacade.RemoveAirline(adminToken, company);
        }

        [TestMethod]
        public void RemoveCustomerTest()
        {
            HUB.ClearDB(out LoginToken<Admin> adminToken, out LoggedInAdministratorFacade adminFacade);
            Customer customer = new Customer { FirstName = "Zrobavel", LastName = "zrob", UserName = "test", Password = "asd", Address = "middle of nowhere", PhoneNo = "0517283946", CreditNo = "1111 2222 3333 4444" };
            adminFacade.CreateNewCustomer(adminToken, customer);
            IList<Customer> customers = adminFacade.GetAllCustomers();
            customer.Id = customers[0].Id;
            adminFacade.RemoveCustomer(adminToken, customer);
            IList<Customer> Nocustomers = adminFacade.GetAllCustomers();
            Assert.AreEqual(Nocustomers.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void RemoveCustomerExeptionTest()
        {
            HUB.ClearDB(out LoginToken<Admin> adminToken, out LoggedInAdministratorFacade adminFacade);
            Customer customer = new Customer { FirstName = "Zrobavel", LastName = "zrob", UserName = "test", Password = "asd", Address = "middle of nowhere", PhoneNo = "0517283946", CreditNo = "1111 2222 3333 4444" };
            adminFacade.CreateNewCustomer(adminToken, customer);
            adminFacade.RemoveCustomer(adminToken, customer);
        }

        [TestMethod]
        public void UpdateCustomerTest()
        {
            HUB.ClearDB(out LoginToken<Admin> adminToken, out LoggedInAdministratorFacade adminFacade);
            Customer customer = new Customer { FirstName = "Zrobavel", LastName = "zrob", UserName = "test", Password = "asd", Address = "middle of nowhere", PhoneNo = "0517283946", CreditNo = "1111 2222 3333 4444" };
            adminFacade.CreateNewCustomer(adminToken, customer);
            IList<Customer> customers = adminFacade.GetAllCustomers();
            customer.Id = customers[0].Id;
            customer.LastName = "Zvolon";
            adminFacade.UpdateCustomerDetails(adminToken, customer);
            IList<Customer> Updatecustomers = adminFacade.GetAllCustomers();
            Assert.AreEqual(Updatecustomers[0].LastName, "Zvolon");
        }

        [TestMethod]
        public void UpdateAirlineTest()
        {
            HUB.ClearDB(out LoginToken<Admin> adminToken, out LoggedInAdministratorFacade adminFacade);
            AirlineCompany company = new AirlineCompany { AirlineName = "ELAL", UserName = "test", Password = "asd", CountryCode = 1 };
            adminFacade.CreateNewAirline(adminToken, company);
            IList<AirlineCompany> companies = adminFacade.GetAllAirlineCompanies();
            company.Id = companies[0].Id;
            company.AirlineName = "ARKIA";
            adminFacade.UpdateAirlineDetails(adminToken, company);
            IList<AirlineCompany> UpdateAirline = adminFacade.GetAllAirlineCompanies();
            Assert.AreEqual(UpdateAirline[0].AirlineName, "ARKIA");
        }
    }
}
