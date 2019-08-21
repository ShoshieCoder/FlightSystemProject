using System;
using FlightSystemProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TESTS
{
    [TestClass]
    public class LoginTest
    {
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordLoginAdmin()
        {
            FlyingCenterSystem system = FlyingCenterSystem.GetInstance();
            system.GetFacade("ADMIN", "asd", out ILoginToken token, out FacadeBase user);
        }

        [TestMethod]
        public void SuccessfulLoginAdmin()
        {
            FlyingCenterSystem system = FlyingCenterSystem.GetInstance();
            system.GetFacade("ADMIN", "admin", out ILoginToken token, out FacadeBase user);
            Assert.AreEqual(token.GetType(), typeof(LoginToken<Admin>));
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void AnonymousUser()
        {
            FlyingCenterSystem system = FlyingCenterSystem.GetInstance();
            system.GetFacade("user", "user", out ILoginToken token, out FacadeBase user);
            Assert.Equals(token, null);
            Assert.AreEqual(user, typeof(FlightSystemProject.AnonymousUserFacade));
        }

        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordLoginAirline()
        {
            HUB.ClearDB(out LoginToken<Admin> adminToken, out LoggedInAdministratorFacade adminFacade);
            FlyingCenterSystem system = FlyingCenterSystem.GetInstance();
            AirlineCompany newAirline = new AirlineCompany { AirlineName = "ELAL", UserName = "ELAL", Password = "amIShai", CountryCode = 1 };
            adminFacade.CreateNewAirline(adminToken, newAirline);
            system.GetFacade("ELAL", "asd", out ILoginToken token, out FacadeBase user);
        }

        [TestMethod]
        public void SuccessfulLoginAirline()
        {
            HUB.ClearDB(out LoginToken<Admin> adminToken, out LoggedInAdministratorFacade adminFacade);
            FlyingCenterSystem system = FlyingCenterSystem.GetInstance();
            AirlineCompany newAirline = new AirlineCompany { AirlineName = "ELAL", UserName = "ELAL", Password = "amIShai", CountryCode = 1 };
            adminFacade.CreateNewAirline(adminToken, newAirline);
            system.GetFacade("ELAL", "amIShai", out ILoginToken token, out FacadeBase user);
            Assert.AreEqual(token.GetType(), typeof(LoginToken<AirlineCompany>));
        }

        [TestMethod]
        public void SuccesfullLoginCustomer()
        {
            HUB.ClearDB(out LoginToken<Admin> adminToken, out LoggedInAdministratorFacade adminFacade);
            FlyingCenterSystem system = FlyingCenterSystem.GetInstance();
            Customer newCustomer = new Customer { FirstName = "Zrobavel", LastName = "zrob", UserName = "CustomerTest", Password = "asd", Address = "Somewhere 33", PhoneNo = "9720546488245", CreditNo = "1111 2222 3333 4444" };
            adminFacade.CreateNewCustomer(adminToken, newCustomer);
            system.GetFacade(newCustomer.UserName, newCustomer.Password, out ILoginToken token, out FacadeBase customerfacade);
            Assert.AreEqual(token.GetType(), typeof(LoginToken<Customer>));
        }

        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordLoginCustomer()
        {
            HUB.ClearDB(out LoginToken<Admin> adminToken, out LoggedInAdministratorFacade adminFacade);
            FlyingCenterSystem system = FlyingCenterSystem.GetInstance();
            Customer newCustomer = new Customer { FirstName = "Zrobavel", LastName = "zrob", UserName = "CustomerTest", Password = "asd", Address = "Somewhere 33", PhoneNo = "9720546488245", CreditNo = "1111 2222 3333 4444" };
            adminFacade.CreateNewCustomer(adminToken, newCustomer);
            system.GetFacade(newCustomer.UserName, "dsa", out ILoginToken token, out FacadeBase customerfacade);
        }
    }
}
