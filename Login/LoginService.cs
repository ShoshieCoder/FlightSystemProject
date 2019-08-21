using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public class LoginService : IloginService
    {
        private IAirlineDAO _airlineDAO = new AirlineDAOmssql();
        private ICustomerDAO _customerDAO = new CustomerDAOmssql();
        private IAdminDAO _adminDAO = new AdminDAOmssql();

        public bool TryAdminLogin(string username, string password, out LoginToken<Admin> token)
        {
            Admin Administrator = _adminDAO.GetAdminByUser(username);
            if (Administrator != null)
            {
                if (Administrator.Password.ToUpper() == password.ToUpper())
                {
                    token = new LoginToken<Admin> { user = Administrator };
                    return true;
                }
                else
                {
                    throw new WrongPasswordException("Wrong password");
                }
            }
            token = null;
            return false;
        }

        public bool TryAirlineLogin(string username, string password, out LoginToken<AirlineCompany> token)
        {
            AirlineCompany Company = _airlineDAO.GetAirlineByUserame(username);
            if (Company != null)
            {
                if (Company.Password.ToUpper() == password.ToUpper())
                {
                    token = new LoginToken<AirlineCompany> { user = Company };
                    return true;
                }
                else
                {
                    throw new WrongPasswordException("Wrong password");
                }
            }
            token = null;
            return false;
        }

        public bool TryCustomerLogin(string username, string password, out LoginToken<Customer> token)
        {
            Customer customer = _customerDAO.GetCustomerByUserame(username);
            if (customer != null)
            {
                if (customer.Password.ToUpper() == password.ToUpper())
                {
                    token = new LoginToken<Customer> { user = customer };
                    return true;
                }
                else
                {
                    throw new WrongPasswordException("Wrong password");
                }
            }
            token = null;
            return false;
        }
    }
}
