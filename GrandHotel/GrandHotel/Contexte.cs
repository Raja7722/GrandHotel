using GrandHotel.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel
{
    public class Contexte
    {
        public List<Client> GetListeClients()
        {
            var list = new List<Client>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select Id, Nom, Prenom, Civilite, CarteFidelite
                                from Client";

            using (var conn = new SqlConnection(Settings.Default.GrandHotelConnect))
            {
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new Client();
                        item.IdClient = (int)reader["Id"];
                        item.Nom = (string)reader["Nom"];
                        item.Prenom = (string)reader["Prenom"];
                        item.Civilite = (string)reader["Civilite"];
                        item.CarteFidelite = (Boolean)reader["CarteFidelite"];
                        //item.Societe = (string)reader["Societe"];
                        list.Add(item);
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

            using (var conn = new SqlConnection(Settings.Default.GrandHotelConnect))
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
