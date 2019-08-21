using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    public class FlyingCenterSystem
    {
        public static FlyingCenterSystem _instance;
        private static object key = new object();
        public static LoginService servise = new LoginService();

        public FlyingCenterSystem()
        {
            Thread t = new Thread(Update);
            t.Start();
        }

        public static FlyingCenterSystem GetInstance()
        {
            lock (key)
            {
                if (_instance == null)
                {
                    _instance = new FlyingCenterSystem();
                }
            }
            return _instance;
        }

        public void GetFacade(string userName, string password, out ILoginToken token, out FacadeBase facade)
        {
            if (servise.TryAdminLogin(userName, password, out LoginToken<Admin> admin))
            {
                token = admin;
                facade = new LoggedInAdministratorFacade();
            }
            else if (servise.TryAirlineLogin(userName, password, out LoginToken<AirlineCompany> company))
            {
                token = company;
                facade = new LoggedinAirlineFacade();
            }
            else if (servise.TryCustomerLogin(userName, password, out LoginToken<Customer> customer))
            {
                token = customer;
                facade = new LoggedInCustomerFacade();
            }
            else
            {
                token = null;
                facade = new AnonymousUserFacade();
                throw new NotFoundException("User not found");
            }
        }

        void Update()
        {
            string num = ConfigurationManager.AppSettings["3HourWait"];
            Thread.Sleep(System.Convert.ToInt32(num));

            using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=FlightSystemProject;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("UPDATES", conn);
                cmd.Connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }

            Update();
        }
    }
}
