using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    class CountryDAOmssql : ICountryDAO
    {
        public void Add(Country t)
        {
            IList<Country> countries = GetAll();
            foreach (Country item in countries)
            {
                if (item.CountryName == t.CountryName)
                {
                    throw new AlreadyExistException("Country name taken");
                }
            }

            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Insert Into Countries(COUNTRY_NAME) Values ({t.CountryName}", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Country> GetAll()
        {
            List<Country> Countries = new List<Country>();
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Countries", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Countries.Add(new Country
                            {
                                CountryName = (string)reader["COUNTRY_NAME"],
                            });
                        }
                    }
                }
            }
            return Countries;
        }

        public Country GetById(int id)
        {
            Country ById = null;
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Countries Where ID={id}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ById = new Country
                            {
                                CountryName = (string)reader["COUNTRY_NAME"],
                            };
                        }
                    }
                }
            }
            return ById;
        }

        public void Remove(Country t)
        {
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Delete from Countries Where ID={t.Id}"))
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
            throw new NotFoundException("Country not found");
        }

        public void Update(Country t)
        {
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Update Countries Set COUNTRY_NAME = {t.CountryName}," +
                    $" Where ID={t.Id}", conn))
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
            throw new NotFoundException("Country not found");
        }
    }
}
