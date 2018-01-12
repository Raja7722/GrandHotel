using GrandHotel.Properties;
using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GrandHotel
{
    public class Contexte
    {

        #region Méthodes pour la PageGestionClients
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

        public void SupprimerClient(int id)
        {
            // Liste de clients qui ne sont pas associé à aucune facture ni occupation de chambre
            var listeNonAssocie = new List<int>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select C.Id
                                from Client C
                                left outer join Facture F on F.IdClient=C.Id
                                left outer join Reservation R on R.IdClient=C.Id
                                where F.IdClient is NULL and R.IdClient is NULL";

            using (var conn = new SqlConnection(Settings.Default.GrandHotelConnect))
            {
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listeNonAssocie.Add((int)reader["Id"]);
                    }
                }
            }

            // Supprimer le client seulement s'il est dans la listeNonAssocie
            if (listeNonAssocie.Contains(id))
            {
                Console.WriteLine("Le client n'est pas associé à une facture ni à une chambre.\n");
                Console.ReadLine();

                // Supprimer l'adresse
                var cmd2 = new SqlCommand();
                cmd2.CommandText = @"delete from Adresse where IdClient = @id";

                cmd2.Parameters.Add(new SqlParameter
                {
                    SqlDbType = SqlDbType.Int,
                    ParameterName = "@id",
                    Value = id
                });
                using (var conn = new SqlConnection(Settings.Default.GrandHotelConnect))
                {
                    cmd2.Connection = conn;
                    conn.Open();
                    cmd2.ExecuteNonQuery();
                }

                // Supprimer le téléphone
                var cmd3 = new SqlCommand();
                cmd3.CommandText = @"delete from Telephone where IdClient = @id";

                cmd3.Parameters.Add(new SqlParameter
                {
                    SqlDbType = SqlDbType.Int,
                    ParameterName = "@id",
                    Value = id
                });
                using (var conn = new SqlConnection(Settings.Default.GrandHotelConnect))
                {
                    cmd3.Connection = conn;
                    conn.Open();
                    cmd3.ExecuteNonQuery();
                }

                // Supprimer l'adresse email
                var cmd4 = new SqlCommand();
                cmd4.CommandText = @"delete from Email where IdClient = @id";

                cmd4.Parameters.Add(new SqlParameter
                {
                    SqlDbType = SqlDbType.Int,
                    ParameterName = "@id",
                    Value = id
                });
                using (var conn = new SqlConnection(Settings.Default.GrandHotelConnect))
                {
                    cmd4.Connection = conn;
                    conn.Open();
                    cmd4.ExecuteNonQuery();
                }

                // Supprimer le client
                var cmd1 = new SqlCommand();
                cmd1.CommandText = @"delete from Client where Id = @id";

                cmd1.Parameters.Add(new SqlParameter
                {
                    SqlDbType = SqlDbType.Int,
                    ParameterName = "@id",
                    Value = id
                });
                using (var conn = new SqlConnection(Settings.Default.GrandHotelConnect))
                {
                    cmd1.Connection = conn;
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                }


                Console.WriteLine("Le client a été supprimé.");
                Console.ReadLine();
            }
            else
            {
                Output.WriteLine(ConsoleColor.Red, "Le client ne peut pas être supprimé car il est associé à une facture ou une réservation.");
            }

        }

        public void SerializationClient()
        {
            List<Client> ListeClients = GrandHotelApp.DataContext.GetListeClients();

            XmlSerializer serializer = new XmlSerializer(typeof(List<Client>),
                                           new XmlRootAttribute("Clients"));

            using (var sw = new StreamWriter(@"..\..\clients.xml"))
            {
                serializer.Serialize(sw, ListeClients);
            }
        }
        #endregion


        #region Méthodes pour la PageGestionFactures

        public List<Facture> GetListFactures(int id, string d)
        {
            var list = new List<Facture>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select Id, IdClient, DateFacture, DatePaiement, CodeModePaiement 
                                from Facture
                                where IdClient=@Id and DateFacture>=@date
                                order by DateFacture";

            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@Id", Value = id });
            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Date, ParameterName = "@date", Value = d });



            using (var conn = new SqlConnection(Settings.Default.GrandHotelConnect))
            {
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new Facture();
                        item.IdFacture = (int)reader["Id"];
                        item.IdClient = (int)reader["IdClient"];
                        item.DateFacture = (DateTime)reader["DateFacture"];
                        item.DatePaiement = (DateTime)reader["DatePaiement"];
                        item.CodeModePaiement = (string)reader["CodeModePaiement"];
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public List<LigneFacture> GetLigneFacture(int id)
        {
            var list = new List<LigneFacture>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select IdFacture, NumLigne, Quantite, MontantHT, TauxTVA, TauxReduction
                                from LigneFacture
                                where IdFacture = @Id";

            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@Id", Value = id });

            using (var conn = new SqlConnection(Settings.Default.GrandHotelConnect))
            {
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var item = new LigneFacture();
                        item.IdFacture = (int)reader["IdFacture"];
                        item.NumLigne = (int)reader["NumLigne"];
                        item.Quantite = (Int16)reader["Quantite"];
                        item.MontantHT = (decimal)reader["MontantHT"];
                        item.TauxTVA = (decimal)reader["TauxTVA"];
                        item.TauxReduction = (decimal)reader["TauxReduction"];
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public void AjouterFacture(Facture facture)
        {

            // Trouver l'identifiant de la dernière facture
            int IdNouvelleFacture = 0;
            var cmd1 = new SqlCommand();
            cmd1.CommandText = @"select top(1) Id
                                from Facture
                                order by Id desc";

            using (var conn = new SqlConnection(Settings.Default.GrandHotelConnect))
            {
                cmd1.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IdNouvelleFacture = (int)reader["Id"] + 1;
                    }
                }
            }

            // Creer nouvelle facture
            var cmd2 = new SqlCommand();


            cmd2.CommandText = @"insert Facture (IdClient, DateFacture, DatePaiement, CodeModePaiement)
									values (@IdClient, @DateFacture, @DatePaiement, @CodeModePaiement)";


            cmd2.Parameters.AddWithValue("@IdClient", facture.IdClient);
            cmd2.Parameters.AddWithValue("@DateFacture", facture.DateFacture);
            cmd2.Parameters.AddWithValue("@DatePaiement", facture.DatePaiement);
            cmd2.Parameters.AddWithValue("@CodeModePaiement", facture.CodeModePaiement);

            using (var cnx = new SqlConnection(Settings.Default.GrandHotelConnect))
            {
                cmd2.Connection = cnx;
                cnx.Open();
                cmd2.ExecuteNonQuery();
            }
        }
   
        public void AjouterLigneFacture(LigneFacture lf)
        {
            var cmd = new SqlCommand();


            cmd.CommandText = @"insert LigneFacture (IdFacture, NumLigne, Quantite, MontantHT, TauxTVA, TauxReduction)
									values (@IdFacture, @NumLigne, @Quantite, @MontantHT, @TauxTVA, @TauxReduction)";

            cmd.Parameters.AddWithValue("@IdFacture", lf.IdFacture);
            cmd.Parameters.AddWithValue("@NumLigne", lf.NumLigne);
            cmd.Parameters.AddWithValue("@Quantite", lf.Quantite);
            cmd.Parameters.AddWithValue("@MontantHT", lf.MontantHT);
            cmd.Parameters.AddWithValue("@TauxTVA", lf.TauxTVA);
            cmd.Parameters.AddWithValue("@TauxReduction", lf.TauxReduction);

            using (var cnx = new SqlConnection(Settings.Default.GrandHotelConnect))
            {
                cmd.Connection = cnx;
                cnx.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SerializationFactures(int id)
        {
            List<Facture> ListeFactures = new List<Facture>();
            var cmd = new SqlCommand();

            // Requête pour exporter les factures
            cmd.CommandText = @"  select distinct IdFacture, F.IdClient, F.DateFacture, F.DatePaiement, F.CodeModePaiement, sum(Quantite*MontantHT*(1-TauxTVA)*(1-TauxReduction))
                                  from Facture F
                                  inner join LigneFacture LF on LF.IdFacture=F.Id
                                  group by IdFacture, IdClient, F.DateFacture, F.DatePaiement, F.CodeModePaiement
                                  having F.IdClient=@Id
                                  order by IdFacture";

            cmd.Parameters.Add(new SqlParameter { SqlDbType = SqlDbType.Int, ParameterName = "@Id", Value = id });


            using (var conn = new SqlConnection(Settings.Default.GrandHotelConnect))
            {
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new Facture();
                        item.IdFacture = (int)reader["IdFacture"];
                        item.IdClient = (int)reader["IdClient"];
                        item.DateFacture = (DateTime)reader["DateFacture"];
                        item.DatePaiement = (DateTime)reader["DatePaiement"];
                        item.CodeModePaiement = (string)reader["CodeModePaiement"];
                        ListeFactures.Add(item);
                    }
                }
            }


            XmlSerializer serializer = new XmlSerializer(typeof(List<Facture>),
                                           new XmlRootAttribute("Factures"));

            using (var sw = new StreamWriter(@"..\..\factures.xml"))
            {
                serializer.Serialize(sw, ListeFactures);
            }
        }

        public void UpdateFacture(Facture fac)
        {

        }

        #endregion

        #region Méthodes pour la PageResultatsHotel

        #endregion

    }
}