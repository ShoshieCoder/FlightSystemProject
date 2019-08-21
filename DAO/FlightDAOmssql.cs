using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FlightSystemProject
{
    public class FlightDAOmssql : IFlightDAO
    {
        public void Add(Flight t)
        {
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Insert Into Flights(AIRLINECOUNPMANY_ID, ORIGIN_COUNTRY_CODE, DESTINATION_COUNTY_CODE, DEPARTURE_TIME, LANDING_TIME, REMAINING_TICKETS)" +
                    $"Values ('{t.AirlineCompnayId}', '{t.OriginCountryCode}', '{t.DestinationCountryCode}', '{t.DepartureTime.ToString()}', '{t.LandingTime.ToString()}', '{t.RemainingTickets}')", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Flight> GetAll()
        {
            List<Flight> Flights = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Flights", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Flights.Add(new Flight
                            {
                                Id = (Int64)reader["ID"],
                                AirlineCompnayId = (Int64)reader["AIRLINECOUNPMANY_ID"],
                                OriginCountryCode = (Int64)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (Int64)reader["DESTINATION_COUNTY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"]
                            });
                        }
                    }
                }
            }
            return Flights;
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> Vacancy = new Dictionary<Flight, int>();
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Flights", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Vacancy.Add(new Flight
                            {
                                Id = (Int64)reader["Id"],
                                AirlineCompnayId = (Int64)reader["AirLineCompany_Id"],
                                OriginCountryCode = (Int64)reader["Origin_Country_Code"],
                                DestinationCountryCode = (Int64)reader["Destination_Country_Code"],
                                DepartureTime = (DateTime)reader["Departure_Time"],
                                LandingTime = (DateTime)reader["Landing_Time"],
                                RemainingTickets = (int)reader["Remaining_Tickets"],
                            }, (int)reader["Remaining_Tickets"]);
                        }
                    }
                }
            }
            return Vacancy;
        }

        public Flight GetById(int id)
        {
            Flight ById = null;
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Flights where ID='{id}'", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ById = new Flight
                            {
                                Id = (long)reader["Id"],
                                AirlineCompnayId = (long)reader["AirLineCompany_Id"],
                                OriginCountryCode = (long)reader["Origin_Country_Code"],
                                DestinationCountryCode = (long)reader["Destination_Country_Code"],
                                DepartureTime = (DateTime)reader["Departure_Time"],
                                LandingTime = (DateTime)reader["Landing_Time"],
                                RemainingTickets = (int)reader["Remaining_Tickets"]
                            };
                        }
                    }
                }
            }
            return ById;
        }

        public IList<Flight> GetFlightsByCustomer(Customer customer)
        {
            List<Flight> FlightsByCustomer = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Flights f join Tickets t on f.ID=t.FLIGHT_ID where t.CUSTOMER_ID='{customer.Id}'", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FlightsByCustomer.Add(new Flight
                            {
                                Id = (Int64)reader["ID"],
                                AirlineCompnayId = (Int64)reader["AIRLINECOUNPMANY_ID"],
                                OriginCountryCode = (Int64)reader["ORIGIN_COUNTRY_CODE"],
                                DestinationCountryCode = (Int64)reader["DESTINATION_COUNTY_CODE"],
                                DepartureTime = (DateTime)reader["DEPARTURE_TIME"],
                                LandingTime = (DateTime)reader["LANDING_TIME"],
                                RemainingTickets = (int)reader["REMAINING_TICKETS"]
                            });
                        }
                    }
                }
            }
            return FlightsByCustomer;
        }

        public IList<Flight> GetFlightsByDepartureDate(DateTime departureDate)
        {
            List<Flight> FlightsByDeparture = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Flights f where f.DEPARTURE_TIME={departureDate}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FlightsByDeparture.Add(new Flight
                            {
                                Id = (long)reader["Id"],
                                AirlineCompnayId = (long)reader["AirLineCompany_Id"],
                                OriginCountryCode = (long)reader["Origin_Country_Code"],
                                DestinationCountryCode = (long)reader["Destination_Country_Code"],
                                DepartureTime = (DateTime)reader["Departure_Time"],
                                LandingTime = (DateTime)reader["Landing_Time"],
                                RemainingTickets = (int)reader["Remaining_Tickets"]
                            });
                        }
                    }
                }
            }
            return FlightsByDeparture;
        }

        public IList<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            List<Flight> FlightsByDestination = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Flights f where f.DESTINATION_COUNTY_CODE={countryCode}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FlightsByDestination.Add(new Flight
                            {
                                Id = (long)reader["Id"],
                                AirlineCompnayId = (long)reader["AirLineCompany_Id"],
                                OriginCountryCode = (long)reader["Origin_Country_Code"],
                                DestinationCountryCode = (long)reader["Destination_Country_Code"],
                                DepartureTime = (DateTime)reader["Departure_Time"],
                                LandingTime = (DateTime)reader["Landing_Time"],
                                RemainingTickets = (int)reader["Remaining_Tickets"]
                            });
                        }
                    }
                }
            }
            return FlightsByDestination;
        }

        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            List<Flight> FlightsByLanding = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Flights f where f.LANDING_TIME={landingDate}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FlightsByLanding.Add(new Flight
                            {
                                Id = (long)reader["Id"],
                                AirlineCompnayId = (long)reader["AirLineCompany_Id"],
                                OriginCountryCode = (long)reader["Origin_Country_Code"],
                                DestinationCountryCode = (long)reader["Destination_Country_Code"],
                                DepartureTime = (DateTime)reader["Departure_Time"],
                                LandingTime = (DateTime)reader["Landing_Time"],
                                RemainingTickets = (int)reader["Remaining_Tickets"]
                            });
                        }
                    }
                }
            }
            return FlightsByLanding;
        }

        public IList<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            List<Flight> FlightsByOrigin = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Flights f where f.ORIGIN_COUNTRY_CODE={countryCode}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FlightsByOrigin.Add(new Flight
                            {
                                Id = (long)reader["Id"],
                                AirlineCompnayId = (long)reader["AirLineCompany_Id"],
                                OriginCountryCode = (long)reader["Origin_Country_Code"],
                                DestinationCountryCode = (long)reader["Destination_Country_Code"],
                                DepartureTime = (DateTime)reader["Departure_Time"],
                                LandingTime = (DateTime)reader["Landing_Time"],
                                RemainingTickets = (int)reader["Remaining_Tickets"]
                            });
                        }
                    }
                }
            }
            return FlightsByOrigin;
        }

        public void Remove(Flight t)
        {
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Delete from Flights Where ID='{t.Id}'",conn))
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
            //throw new NotFoundException("Flight not found");
        }

        public void Update(Flight t)
        {
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Update Flights Set AIRLINECOUNPMANY_ID = '{t.AirlineCompnayId}', ORIGIN_COUNTRY_CODE = '{t.OriginCountryCode}'," +
                    $" DESTINATION_COUNTY_CODE = '{t.DestinationCountryCode}', DEPARTURE_TIME = '{t.DepartureTime}'," +
                    $" LANDING_TIME = '{t.LandingTime}',REMAINING_TICKETS = '{t.RemainingTickets}' where ID = '{t.Id}'", conn))
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
