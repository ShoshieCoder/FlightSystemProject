using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    class AirlineDAOmssql : IAirlineDAO
    {
        public void Add(AirlineCompany t)
        {
            IList<AirlineCompany> companies = GetAll();
            foreach (AirlineCompany item in companies)
            {
                if (item.UserName == t.UserName)
                {
                    throw new AlreadyExistException("User name taken");
                }
                if(item.AirlineName == t.AirlineName)
                {
                    throw new AlreadyExistException("Company name taken");
                }
            }
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Insert Into AirlineCompanies(AIRLINE_NAME, USER_NAME, PASSWORD," +
                    $" COUNTRY_CODE) Values ('{t.AirlineName}','{t.UserName}','{t.Password}','{t.CountryCode}')", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public AirlineCompany GetAirlineByUserame(string name)
        {
            AirlineCompany ByUser = null;
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from AirlineCompanies where USER_NAME='{name}'", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ByUser = new AirlineCompany
                            {
                                Id = (Int64)reader["ID"],
                                AirlineName = (string)reader["AIRLINE_NAME"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                                CountryCode = (long)reader["COUNTRY_CODE"]
                            };
                        }
                    }
                }
            }
            return ByUser;
        }

        public IList<AirlineCompany> GetAll()
        {
            List<AirlineCompany> Companies = new List<AirlineCompany>();
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from AirlineCompanies", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Companies.Add(new AirlineCompany
                            {
                                Id = (Int64)reader["ID"],
                                AirlineName = (string)reader["AIRLINE_NAME"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                                CountryCode = (long)reader["COUNTRY_CODE"]
                            });
                        }
                    }
                }
            }
            return Companies;
        }

        public IList<AirlineCompany> GetAllAirlinesByCountry(int countryId)
        {
            List<AirlineCompany> Companies = new List<AirlineCompany>();
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from AirlineCompanies Where COUNTRY_CODE={countryId}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Companies.Add(new AirlineCompany
                            {
                                Id = (Int64)reader["ID"],
                                AirlineName = (string)reader["AIRLINE_NAME"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                                CountryCode = (long)reader["COUNTRY_CODE"]
                            });
                        }
                    }
                }
            }
            return Companies;
        }

        public AirlineCompany GetById(int id)
        {
            AirlineCompany ById = null;
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from AirlineCompanies Where ID={id}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ById = new AirlineCompany
                            {
                                AirlineName = (string)reader["AIRLINE_NAME"],
                                UserName = (string)reader["USER_NAME"],
                                Password = (string)reader["PASSWORD"],
                                CountryCode = (long)reader["COUNTRY_CODE"]
                            };
                        }
                    }
                }
            }
            return ById;
        }

        public void Remove(AirlineCompany t)
        {
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Delete from AirlineCompanies Where ID='{t.Id}'",conn))
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
            throw new NotFoundException("Airline Company not found");
        }

        public void Update(AirlineCompany t)
        {
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Update AirlineCompanies Set AIRLINE_NAME = '{t.AirlineName}', USER_NAME = '{t.UserName}'," +
                    $" PASSWORD = '{t.Password}', COUNTRY_CODE='{t.CountryCode}' Where ID='{t.Id}'" ,conn))
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
        }
    }
}
