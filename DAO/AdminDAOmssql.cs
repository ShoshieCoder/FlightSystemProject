using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    class AdminDAOmssql : IAdminDAO
    {
        public void Add(Admin t)
        {
            IList<Admin> admins = GetAll();
            foreach(Admin item in admins)
            {
                if (item.UserName == t.UserName)
                {
                    throw new AlreadyExistException("User name taken");
                }
            }

            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Insert Into Administrators(USER_NAME, PASSWORD) Values" +
                    $"{t.UserName},{t.Password}", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Admin> GetAll()
        {
            List<Admin> Admins = new List<Admin>();
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Administratos", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Admins.Add(new Admin
                            {
                                UserName = (string)reader["[USER]"],
                                Password = (string)reader["PASSWORD"],
                            });
                        }
                    }
                }
            }
            return Admins;
        }

        public Admin GetAdminByUser(string name)
        {
            Admin ByUser = null;
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM Administrators WHERE USER_NAME='{name}'", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ByUser = new Admin
                            {
                                Id = (Int64)reader["ID"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                            };
                        }
                    }
                }
            }
            return ByUser;
        }

        public Admin GetById(int id)
        {
            Admin ById = null;
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Administratos Where ID='{id}'", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ById = new Admin
                            {
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                            };
                        }
                    }
                }
            }
            return ById;
        }

        public void Remove(Admin t)
        {
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Delete from Administratos Where ID='{t.Id}'"))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            throw new NotFoundException("Admin not found");
        }

        public void Update(Admin t)
        {
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Update Administratos Set USER = {t.UserName}, PASSWORD = {t.Password}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            throw new NotFoundException("Admin not found");
        }
    }
}
