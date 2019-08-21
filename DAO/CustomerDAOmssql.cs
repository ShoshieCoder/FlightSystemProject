using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    class CustomerDAOmssql : ICustomerDAO
    {
        public void Add(Customer t)
        {
            IList<Customer> coutsomers = GetAll();
            foreach (Customer item in coutsomers)
            {
                if (item.UserName == t.UserName)
                {
                    throw new AlreadyExistException("User name taken");
                }
            }

            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Insert Into Customers(FIRST_NAME, LAST_NAME, USER_NAME, PASSWORD," +
                    $" ADDRESS, PHONE_NO, CREDITCARD_NO) Values ('{t.FirstName}','{t.LastName}','{t.UserName}','{t.Password}'," +
                    $"'{t.Address}','{t.PhoneNo}','{t.CreditNo}')", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Customer> GetAll()
        {
            List<Customer> Customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Customers", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customers.Add(new Customer
                            {
                                Id = (Int64)reader["ID"],
                                FirstName = (string)reader["FIRST_NAME"],
                                LastName = (string)reader["LAST_NAME"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                                Address = (string)reader["ADDRESS"],
                                PhoneNo = (string)reader["PHONE_NO"],
                                CreditNo = (string)reader["CREDITCARD_NO"]
                            });
                        }
                    }
                }
            }
            return Customers;
        }

        public Customer GetById(int id)
        {
            Customer ById = null;
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Customer where ID={id}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ById = new Customer
                            {
                                FirstName = (string)reader["FIRST_NAME"],
                                LastName = (string)reader["LAST_NAME"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                                Address = (string)reader["ADDRESS"],
                                PhoneNo = (string)reader["PHONE_NO"],
                                CreditNo = (string)reader["CREDITCARD_NO"]
                            };
                        }
                    }
                }
            }
            return ById;
        }

        public Customer GetCustomerByUserame(string name)
        {
            Customer ByUser = null;
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Customers where USER_NAME='{name}'", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ByUser = new Customer
                            {
                                FirstName = (string)reader["FIRST_NAME"],
                                LastName = (string)reader["LAST_NAME"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                                Address = (string)reader["ADDRESS"],
                                PhoneNo = (string)reader["PHONE_NO"],
                                CreditNo = (string)reader["CREDITCARD_NO"]
                            };
                        }
                    }
                }
            }
            return ByUser;
        }

        public void Remove(Customer t)
        {
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Delete from Customers Where ID='{t.Id}'",conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.RecordsAffected > 0)
                        {
                            return;
                        }
                    }
                }
            }
            throw new NotFoundException("Customer not found");
        }

        public void Update(Customer t)
        {
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Update Customers Set FIRST_NAME = '{t.FirstName}', LAST_NAME = '{t.LastName}'," +
                    $" USER_NAME = '{t.UserName}', PASSWORD = '{t.Password}', ADDRESS='{t.Address}', PHONE_NO='{t.PhoneNo}'," +
                    $" CREDITCARD_NO = '{t.CreditNo}' Where ID = '{t.Id}'", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.RecordsAffected > 0)
                        {
                            return;
                        }
                    }
                }
            }
            throw new NotFoundException("Customer not found");
        }
    }
}
