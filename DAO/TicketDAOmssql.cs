using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSystemProject
{
    class TicketDAOmssql : ITicketDAO
    {
        public void Add(Ticket t)
        {
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Insert Into Tickets(FLIGHT_ID,CUSTOMER_ID) Values ('{t.FlightId}','{t.CustomerId}')", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Ticket> GetAll()
        {
            List<Ticket> Tickets = new List<Ticket>();
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Tickets", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tickets.Add(new Ticket
                            {
                                Id = (Int64)reader["Id"],
                                FlightId = (Int64)reader["FLIGHT_ID"],
                                CustomerId = (Int64)reader["CUSTOMER_ID"],
                            });
                        }
                    }
                }
            }
            return Tickets;
        }

        public Ticket GetById(int id)
        {
            Ticket ById = null;
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"select * from Tickets Where ID={id}", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ById = new Ticket
                            {
                                Id = (long)reader["Id"],
                                FlightId = (long)reader["FLIGHT_ID"],
                                CustomerId = (long)reader["CUSTOMER_ID"],
                            };
                        }
                    }
                }
            }
            return ById;
        }

        public void Remove(Ticket t)
        {
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Delete from Tickets Where ID='{t.Id}'",conn))
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
            throw new NotFoundException("Ticket not found");
        }

        public void Update(Ticket t)
        {
            using (SqlConnection conn = new SqlConnection(FlightSystemCFG.CONNECTION_FLIGHT_DB))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand($"Update Tickets Set FLIGHT_ID = {t.FlightId}, CUSTOMER_ID = {t.CustomerId},", conn))
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
            throw new NotFoundException("Ticket not found");
        }
    }
}
