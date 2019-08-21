using FlightSystemProject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TESTS
{
    static class HUB
    {
        public static LoginToken<Admin> admin = new LoginToken<Admin> { user = new Admin { UserName = "ADMIN", Password = "admin" } };
        public static LoggedInAdministratorFacade facade = new LoggedInAdministratorFacade();

        static public void ClearDB(out LoginToken<Admin> token, out LoggedInAdministratorFacade adminFacade)
        {
            using (SqlConnection conn = new SqlConnection(FlightSystemProject.FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Delete From Tickets;" +
                    "Delete From Flights;" +
                    "Delete From AirlineCompanies ;" +
                    "Delete From Customers;", conn))
                {
                    cmd.ExecuteNonQuery();
                }

                token = admin;
                adminFacade = facade;
            }
        }
    }
}
