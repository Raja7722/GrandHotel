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

        public List<ClientCoordonnees> GetCoordonneesClient1(int i)
        {
            var list = new List<ClientCoordonnees>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select distinct C.Id, C.Civilite, C.Nom, C.Prenom, A.Rue, A.Complement, A.CodePostal, A.Ville, T.Numero, E.Adresse
                                from Client C
                                left outer join Adresse A on A.IdClient=C.Id
                                left outer join Telephone T on T.IdClient=C.Id
                                left outer join Email E on E.IdClient=C.Id
                                where C.Id = @id";


            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@id",
                Value = i
            });

            using (var conn = new SqlConnection(Settings.Default.GrandHotelConnect))
            {
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ClientCoordonnees();
                        item.IdClient = (int)reader["Id"];
                        item.Civilite = (string)reader["Civilite"];
                        item.Nom = (string)reader["Nom"];
                        item.Prenom = (string)reader["Prenom"];
                        item.Rue = (string)reader["Rue"];
                        if (reader["Complement"] != DBNull.Value)
                            item.Complement = (string)reader["Complement"];
                        item.CodePostal = (string)reader["CodePostal"];
                        item.Ville = (string)reader["Ville"];
                        item.Numero = (string)reader["Numero"];
                        item.Adresse = (string)reader["Adresse"];
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public List<ClientCoordonnees> GetCoordonneesClient(int i)
        {
            var list = new List<ClientCoordonnees>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select distinct C.Id, C.Civilite, C.Nom, C.Prenom, A.Rue, A.Complement, A.CodePostal, A.Ville, T.Numero, E.Adresse
                                from Client C
                                left outer join Adresse A on A.IdClient=C.Id
                                left outer join Telephone T on T.IdClient=C.Id
                                left outer join Email E on E.IdClient=C.Id
                                where C.Id = @id";


            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@id",
                Value = i
            });

            using (var conn = new SqlConnection(Settings.Default.GrandHotelConnect))
            {
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new ClientCoordonnees();
                        item.IdClient = (int)reader["Id"];
                        item.Civilite = (string)reader["Civilite"];
                        item.Nom = (string)reader["Nom"];
                        item.Prenom = (string)reader["Prenom"];
                        item.Rue = (string)reader["Rue"];
                        if (reader["Complement"] != DBNull.Value)
                            item.Complement = (string)reader["Complement"];
                        item.CodePostal = (string)reader["CodePostal"];
                        item.Ville = (string)reader["Ville"];
                        item.Numero = (string)reader["Numero"];
                        item.Adresse = (string)reader["Adresse"];
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public void AjouterClientAdresse(Client client, Adresse adresse)
        {
            // Trouver l'identifiant du dernier client
            int IdNouveauClient = 0;
            var cmd1 = new SqlCommand();
            cmd1.CommandText = @"select top(1) id
                                from client
                                order by id desc";

            using (var conn = new SqlConnection(Settings.Default.GrandHotelConnect))
            {
                cmd1.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IdNouveauClient = (int)reader["Id"] + 1;
                    }
                }
            }

            // Creer nouveau client
            var cmd2 = new SqlCommand();


            cmd2.CommandText = @"insert Client (Civilite, Nom, Prenom, CarteFidelite, Societe)
									values (@Civilite, @Nom, @Prenom, @CarteFidelite, @Societe)";


            //cmd2.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.VarChar, ParameterName = "@Civilite", Value = client.Civilite });
            //cmd2.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Nom", Value = client.Nom });
            //cmd2.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Prenom", Value = client.Prenom });
            //cmd2.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Bit, ParameterName = "@CarteFidelite", Value = client.CarteFidelite });
            //cmd2.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Societe", Value = client.Societe });

            cmd2.Parameters.AddWithValue("@Civilite", client.Civilite);
            cmd2.Parameters.AddWithValue("@Nom", client.Nom);
            cmd2.Parameters.AddWithValue("@Prenom", client.Prenom);
            cmd2.Parameters.AddWithValue("@CarteFidelite", client.CarteFidelite);
            cmd2.Parameters.AddWithValue("@Societe", client.Societe);

            using (var cnx = new SqlConnection(Settings.Default.GrandHotelConnect))
            {
                cmd2.Connection = cnx;
                cnx.Open();
                cmd2.ExecuteNonQuery();
            }

            // Creer une nouvelle adresse
            var cmd3 = new SqlCommand();


            cmd3.CommandText = @"insert Adresse (IdClient, Rue, Complement, CodePostal, Ville)
                                    values (@IdClient, @Rue, @Complement, @CodePostal, @Ville)";

            //cmd3.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.VarChar, ParameterName = "@IdClient", Value = IdNouveauClient });
            //cmd3.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Rue", Value = adresse.Rue });
            //cmd3.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Complement", Value = adresse.Complement });
            //cmd3.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.VarChar, ParameterName = "@CodePostal", Value = adresse.CodePostal });
            //cmd3.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.NVarChar, ParameterName = "@Ville", Value = adresse.Ville });

            cmd3.Parameters.AddWithValue("@IdClient", IdNouveauClient);
            cmd3.Parameters.AddWithValue("@Rue", adresse.Rue);
            cmd3.Parameters.AddWithValue("@Complement", adresse.Complement);
            cmd3.Parameters.AddWithValue("@CodePostal", adresse.CodePostal);
            cmd3.Parameters.AddWithValue("@Ville", adresse.Ville);

            using (var cnx = new SqlConnection(Settings.Default.GrandHotelConnect))
            {
                cmd3.Connection = cnx;
                cnx.Open();
                cmd3.ExecuteNonQuery();
            }
        }

        public void AjouterTel(Telephone t)
        {
            // Creer nouveau telephone
            var cmd = new SqlCommand();


            cmd.CommandText = @"insert Telephone (Numero, IdClient, CodeType, Pro)
									values (@Numero, @IdClient, @CodeType, @Pro)";

            //cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.VarChar, ParameterName = "@Numero", Value = t.Numero });
            //cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@IdClient", Value = t.IdClient });
            //cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Char, ParameterName = "@CodeType", Value = t.CodeType });
            //cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Bit, ParameterName = "@Pro", Value = t.Pro });

            cmd.Parameters.AddWithValue("@Numero", t.Numero);
            cmd.Parameters.AddWithValue("@IdClient", t.IdClient);
            cmd.Parameters.AddWithValue("@CodeType", t.CodeType);
            cmd.Parameters.AddWithValue("@Pro", t.Pro);

            using (var cnx = new SqlConnection(Settings.Default.GrandHotelConnect))
            {
                cmd.Connection = cnx;
                cnx.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AjouterMail(Email em)
        {
            // Creer nouvelle adresse email
            var cmd = new SqlCommand();


            cmd.CommandText = @"insert Email (Adresse, IdClient, Pro)
									values (@Adresse, @IdClient, @Pro)";

            cmd.Parameters.AddWithValue("@Adresse", em.Adresse);
            cmd.Parameters.AddWithValue("@IdClient", em.IdClient);
            cmd.Parameters.AddWithValue("@Pro", em.Pro);

            //cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.VarChar, ParameterName = "@Adresse", Value = em.Adresse });
            //cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@IdClient", Value = em.IdClient });
            //cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Bit, ParameterName = "@Pro", Value = em.Pro });

            using (var cnx = new SqlConnection(Settings.Default.GrandHotelConnect))
            {
                cmd.Connection = cnx;
                cnx.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SupprimerClient()
        {

        }

        public List<string> GetFactures()
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