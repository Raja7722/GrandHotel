using GrandHotel.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel
{
    class Contexte : IDataContext
    {
        public IList<string> GetClients()
        {
            var list = new List<string>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select Nom, Prenom
                                from Client
                                order by 1";

            using (var conn = new SqlConnection(Settings.Default.GrandHotel))
            {
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add((string)reader["Client"]);
                    }
                }
            }

            return list;
        }

        public IList<string> GetFactures()
        {
            var list = new List<string>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select Nom, Prenom
                                from Client
                                order by 1";

            using (var conn = new SqlConnection(Settings.Default.GrandHotel))
            {
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add((string)reader["Client"]);
                    }
                }
            }

            return list;
        }
    }
}
