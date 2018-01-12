using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Serializer
{
    public class DAL
    {
        // Récupère les commandes d'un client et leur détail,
        // et stocke les données sous forme arborescente
        public static List<Order> GetOrders(string customerId)
        {
            var list = new List<Order>();
            var cmd = new SqlCommand();
            cmd.CommandText = @"select o.OrderId, o.OrderDate, o.ShippedDate, o.ShipperId, o.Freight,
                    od.ProductId, p.Name, od.UnitPrice, od.Quantity, od.Discount
                    from Orders o
                    left outer join OrderDetail od on o.OrderId = od.OrderId
                    left outer join Product p on od.ProductId = p.ProductId
                    where CustomerId = @id";

            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.NVarChar,
                ParameterName = "@id",
                Value = customerId
            });

            using (var conn = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Northwind2; Integrated Security = True;"))
            {
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int orderId = (int)reader["OrderId"];

                        // Si l'id de la commande courante est différent de celui de la dernière
                        // commande de la liste, on crée un nouvel objet Order
                        Order ord = null;
                        if (list.Count == 0 || list[list.Count - 1].OrderId != orderId)
                        {
                            ord = new Order();
                            ord.OrderId = orderId;
                            ord.CustomerId = customerId;
                            ord.OrderDate = (DateTime)reader["OrderDate"];
                            if (reader["ShippedDate"] != DBNull.Value)
                                ord.ShippedDate = (DateTime)reader["ShippedDate"];
                            ord.ShipperId = (int)reader["ShipperId"];
                            ord.Freight = (decimal)reader["Freight"];
                            ord.Details = new List<OrderDetail>();
                            list.Add(ord);
                        }
                        else
                            ord = list[list.Count - 1];

                        // Création de la ligne de commande s'il en existe au moins une
                        if (reader["OrderId"] != DBNull.Value)
                        {
                            OrderDetail od = new OrderDetail();
                            od.OrderId = (int)reader["OrderId"];
                            od.ProductId = (int)reader["ProductId"];
                            od.ProductName = (string)reader["Name"];
                            od.UnitPrice = (decimal)reader["UnitPrice"];
                            od.Quantity = (short)reader["Quantity"];
                            od.Discount = (float)reader["Discount"];

                            ord.Details.Add(od);
                        }
                    }
                }
            }

            return list;
        }

        public static List<Order> ImporterXml()
        {
            List<Order> listCol = null;

            XmlSerializer deserializer = new XmlSerializer(typeof(List<Order>),
               new XmlRootAttribute("Orders"));

            using (var sr = new StreamReader(@"..\..\Orders_XmlSerializer.xml"))
            {
                listCol = (List<Order>)deserializer.Deserialize(sr);
            }

            return listCol;
        }

        public static void ExporterXml(List<Order> listCol)
        {
            // On crée un sérialiseur, en spécifiant le type de l'objet à sérialiser
            // et le nom de l'élément xml racine
            XmlSerializer serializer = new XmlSerializer(typeof(List<Order>),
                                       new XmlRootAttribute("Orders"));

            using (var sw = new StreamWriter(@"..\..\Orders_XMLSerializer.xml"))
            {
                serializer.Serialize(sw, listCol);
            }
        }

        // Génération d'un fichier xml avec une structure différente de celle des entités
        public static void ExporterXml_XmlWriter(IEnumerable<Order> listCom)
        {
            // Définit les paramètres pour l'indentation du flux xml généré
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";

            using (XmlWriter writer = XmlWriter.Create(@"..\..\Orders_XMLWriter.xml", settings))
            {
                // Ecriture du prologue
                writer.WriteStartDocument();

                // Ecriture de l'élément racine
                writer.WriteStartElement("DateCommandes");

                // Variables permettant de repérer les changements de mois et années
                int année = 0;
                int mois = 0;
                // On parcourt la liste des commandes triées par date
                foreach (Order com in listCom.OrderBy(c => c.OrderDate))
                {
                    // Ecritures des éléments DateCommande
                    bool nouveauGroupe = (com.OrderDate.Year != année || com.OrderDate.Month != mois);
                    if (année != 0 && nouveauGroupe)
                        writer.WriteEndElement();

                    if (nouveauGroupe)
                    {
                        writer.WriteStartElement("DateCommande");
                        writer.WriteAttributeString("annee", com.OrderDate.Year.ToString());
                        writer.WriteAttributeString("mois", com.OrderDate.Month.ToString());
                        année = com.OrderDate.Year;
                        mois = com.OrderDate.Month;
                    }

                    // Ecriture des commandes
                    writer.WriteStartElement("Commande"); 
                    writer.WriteAttributeString("Id", com.OrderId.ToString());
                    decimal montant = com.Details.Sum(d => d.UnitPrice * d.Quantity * (1 - (decimal)d.Discount));
                    montant = Math.Round(montant, 2);
                    writer.WriteAttributeString("Montant", montant.ToString());
                    writer.WriteEndElement();
                }

                // Ecriture de la balise fermante de l'élément racine et fin du document
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        #region Linq To XML sur fichier Bandes dessinées

        private static readonly string _chemin = @"..\..\Exemples\CollectionsBD.xml";
        private static readonly XDocument _doc = XDocument.Load(_chemin);

        // titres des albums qui sont sortis dans les années 60
        public static IEnumerable<string> AlbumsAnnées60()
        {
            return _doc.Descendants("Album").Where(a => a.Attribute("Année").Value.Substring(0, 3) == "196")
                    .Attributes("Titre").Select(t => t.Value);
        }

        // Ajout de l'auteur Pascal Dabère dans la liste des auteurs de Lucky Luke
        public static void AjouterAuteur()
        {
            // Récupère l'élément Auteurs
            var auteurs = _doc.Descendants("CollectionBD").Where(c => c.Attribute("Nom").Value == "Lucky Luke")
                    .Descendants("Auteurs").FirstOrDefault();
            // Ajoute un élément Auteur
            auteurs.Add(new XElement("Auteur", "Pascal Dabère"));
        }

        // Ajout d'un album dans la collection des Lucky Luke 
        public static void AjouterAlbum()
        {
            // Récupère l'élément Albums
            var albums = _doc.Descendants("CollectionBD").Where(c => c.Attribute("Nom").Value == "Lucky Luke")
                    .Descendants("Albums").FirstOrDefault();
            
            // Crée un élément Album avec ses attributs
            XElement album = new XElement("Album");
            album.Add(new XAttribute("Id", albums.Elements("Album").Count()+1));
            album.Add(new XAttribute("Titre", "Le pont sur le Mississippi"));
            album.Add(new XAttribute("Année", "1994"));

            // Ajoute cet élément à l'élément Albums
            albums.Add(album);
        }

        // Mise en majuscules du titre de l’album N° 15 d’Astérix
        public static void TitreEnMajuscules()
        {
            var album = _doc.Descendants("CollectionBD").Where(c => c.Attribute("Nom").Value == "Astérix")
                    .Descendants("Album").Where(a => a.Attribute("Id").Value == "15").FirstOrDefault();

            album.Attribute("Titre").Value = album.Attribute("Titre").Value.ToUpper();
        }

        public static void EnregistrerModifs()
        {
            _doc.Save(_chemin);
        }

        #endregion
    }
}
