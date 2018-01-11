using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outils.TData;
using Northwind2.Properties;

namespace Northwind2
{
	public class Contexte2 : IDataContext
	{
		private DataSet _dsProduits;
		private SqlDataAdapter _adaptProduits;

		public IList<string> GetPaysFournisseurs()
		{
			var cmd = new SqlCommand();
			cmd.CommandText = @"select distinct A.Country
				from Address A
				inner join Supplier S on S.AddressId = A.AddressId
				order by 1";

			DataSet dsPays = new DataSet();
			SqlDataAdapter adapt = new SqlDataAdapter(cmd);

			using (var conn = new SqlConnection(Settings.Default.Northwind2Connect))
			{
				cmd.Connection = conn;
				//adapt.FillSchema(dsPays, SchemaType.Source);
				adapt.Fill(dsPays);
			}

			return dsPays.Tables[0].ToSimpleList<string>();
		}

		public IList<Supplier> GetFournisseurs(string pays)
		{
			var cmd = new SqlCommand();
			cmd.CommandText = @"select S.SupplierId, S.CompanyName
							from Supplier S
							inner join Address A on S.AddressId = A.AddressId
							where A.Country = @pays
							order by 1";

			cmd.Parameters.Add(new SqlParameter
			{
				SqlDbType = SqlDbType.NVarChar,
				ParameterName = "@pays",
				Value = pays
			});

			DataSet dsFournisseurs = new DataSet();
			SqlDataAdapter adapt = new SqlDataAdapter(cmd);

			using (var conn = new SqlConnection(Settings.Default.Northwind2Connect))
			{
				cmd.Connection = conn;
				//adapt.FillSchema(dsFournisseurs, SchemaType.Source);
				adapt.Fill(dsFournisseurs);
			}

			List<Supplier> list = dsFournisseurs.Tables[0].ToList<Supplier>();
			return list;
		}

		public int GetNbProduits(string pays)
		{
			var cmd = new SqlCommand();
			cmd.CommandText = @"select COUNT(*) NbProduits
				from Product p
				inner join Supplier s on (p.SupplierId = s.SupplierId)
				inner join Address a on s.AddressId = a.AddressId
				where a.Country = @pays";

			cmd.Parameters.Add(new SqlParameter
			{
				SqlDbType = SqlDbType.NVarChar,
				ParameterName = "@pays",
				Value = pays
			});

			using (var cnx = new SqlConnection(Settings.Default.Northwind2Connect))
			{
				cmd.Connection = cnx;
				cnx.Open();
				return (int)cmd.ExecuteScalar();
			}
		}

		// Récupération des clients et commandes en masse dans un DataSet contenant
		// 2 tables en relation, et transfert dans une arborescence d'objets
		public IList<Customer> GetClientsCommandes()
		{
			var cmd = new SqlCommand();
			cmd.CommandText = @"select CustomerId, CompanyName from Customer;
				select O.OrderId, CustomerId, OrderDate, ShippedDate, Freight,
					count(D.ProductId) ItemsCount,
					SUM(D.Quantity * D.UnitPrice) Total
				from Orders O
				inner join OrderDetail D on O.OrderId = D.OrderId
				group by CustomerId, O.OrderId, OrderDate, ShippedDate, Freight
				order by CustomerId, O.OrderId";

			DataSet dsClients = new DataSet();
			SqlDataAdapter adapt = new SqlDataAdapter(cmd);

			using (var conn = new SqlConnection(Settings.Default.Northwind2Connect))
			{
				cmd.Connection = conn;
				// La ligne suivante crée 2 tables, correspondant aux résultats
				// des 2 requêtes de la commande SQL
				adapt.FillSchema(dsClients, SchemaType.Source);

				// Création d'une relation entre les 2 tables, de façon
				// à retrouver facilement les commandes de chaque client
				dsClients.Relations.Add("OrdersOfCustomer",
					dsClients.Tables[0].Columns["Customerid"],
					dsClients.Tables[1].Columns["Customerid"]);

				adapt.Fill(dsClients);
			}

			// Remplissage d'une liste de clients à partir des tables en relation
			List<Customer> list = new List<Customer>();
			foreach (var ligneCli in dsClients.Tables[0].Rows.Cast<DataRow>())
			{
				// Création du client
				Customer cli = new Customer();
				cli.CustomerId = (string)ligneCli["CustomerId"];
				cli.CompanyName = (string)ligneCli["CompanyName"];
				cli.Orders = new List<Order>();

				// Création de ses commandes
				DataRow[] commandes = ligneCli.GetChildRows("OrdersOfCustomer");
				foreach (var ligneCom in commandes)
				{
					Order com = new Order();
					com.OrderId = (int)ligneCom["OrderId"];
					com.OrderDate = (DateTime)ligneCom["OrderDate"];
					if (ligneCom["ShippedDate"] != DBNull.Value)
						com.ShippedDate = (DateTime)ligneCom["ShippedDate"];
					com.Freight = (decimal)ligneCom["Freight"];
					com.ItemsCount = (int)ligneCom["ItemsCount"];
					com.Total = (decimal)ligneCom["Total"];
					cli.Orders.Add(com);
				}

				list.Add(cli);
			}
			return list;
		}

		public IList<Category> GetCatégories()
		{
			var cmd = new SqlCommand();
			cmd.CommandText = @"select CategoryId, Name, Description from Category";

			DataSet dsCategories = new DataSet();
			SqlDataAdapter adapt = new SqlDataAdapter(cmd);

			using (var conn = new SqlConnection(Settings.Default.Northwind2Connect))
			{
				cmd.Connection = conn;
				adapt.Fill(dsCategories);
			}

			var list = dsCategories.Tables[0].ToList<Category>();
			return list;
		}

		public void InitDataSetProduits()
		{
			_dsProduits = new DataSet();

			var cmd = new SqlCommand();
			cmd.CommandText = @"select ProductId, CategoryId, SupplierId, Name, UnitPrice, UnitsInStock
									from Product where CategoryId = @categorie order by 1";

			// Création du paramètre de la commande sans spécifier de valeur
			cmd.Parameters.Add("@categorie", SqlDbType.UniqueIdentifier);

			// Création de l'adaptateur et association à la requête de sélection
			_adaptProduits = new SqlDataAdapter(cmd);

			// Génération des requêtes Insert, Update, Delete avec un constructeur de requêtes
			using (var conn = new SqlConnection(Settings.Default.Northwind2Connect))
			{
				cmd.Connection = conn;
				SqlCommandBuilder scb = new SqlCommandBuilder(_adaptProduits);
				
				// Création de la table du DataSet
				_adaptProduits.FillSchema(_dsProduits, SchemaType.Source);

				// Génération des commandes d'insertion, modification et suppression
				_adaptProduits.InsertCommand = scb.GetInsertCommand();
				_adaptProduits.UpdateCommand = scb.GetUpdateCommand();
				_adaptProduits.DeleteCommand = scb.GetDeleteCommand();
			}
		}

		// Récupération de la liste des produits
		public IList<Product> GetProduits(Guid idCategorie)
		{
			// Recherche les lignes de la catégorie demandée, qui ont été supprimées ou modifiées
			string filtre = "CategoryId = '" + idCategorie + "'";
			DataRow[] lignes = _dsProduits.Tables[0].Select(filtre, "",
				DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent);

			// S'il n'y en a aucune, on peut recharger la table depuis la base
			// sans risque de perdre les suppressions et modifs
			if (!lignes.Any())
			{
				using (var conn = new SqlConnection(Settings.Default.Northwind2Connect))
				{
					// On renseigne la valeur du paramètre de la requête select
					_adaptProduits.SelectCommand.Parameters["@categorie"].Value = idCategorie;
					_adaptProduits.SelectCommand.Connection = conn;

					// Rafraichissement du DataSet à partir des données de la base
					_adaptProduits.Fill(_dsProduits);
				}
			}

			List<Product> list = _dsProduits.Tables[0].ToList<Product>();
			return list;
		}

		public void AjouterModifierProduit(Product produit, Operations op)
		{
			// Pour un ajout, on crée une nouvelle ligne, sinon on recherche la ligne à modifier
			DataRow ligne;
			if (op == Operations.Ajout)
				ligne = _dsProduits.Tables[0].NewRow();
			else
				ligne = _dsProduits.Tables[0].Rows.Find(produit.ProductId);

			// Affectation des valeurs des colonnes de la ligne
			ligne.BeginEdit();
			// L'Id du produit est généré automatiquement par la base
			ligne["CategoryId"] = produit.CategoryId;
			ligne["SupplierId"] = produit.SupplierId;
			ligne["Name"] = produit.Name;
			ligne["UnitPrice"] = produit.UnitPrice;
			ligne["UnitsInStock"] = produit.UnitsInStock;
			ligne.EndEdit();

			if (op == Operations.Ajout)
				_dsProduits.Tables[0].Rows.Add(ligne);
		}

		public void SupprimerProduit(int id)
		{
			DataRow ligne = _dsProduits.Tables[0].Rows.Find(id);
			if (ligne != null) ligne.Delete();
		}

		public void AjouterProduitCategorie(Product produit)
		{
            if (produit.CategoryId == Guid.Empty)
            {
                // Reherche de la catégorie "Autres produits" et création si elle n'existe pas
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"if not exists(
					select CategoryId from Category where CategoryId=@Cate)
					insert Category(CategoryId, Name, Description) values
					(@Cate, 'Others', 'Other products')";

                Guid idCategorieAutres = new Guid("D77CBDA2-DD29-4CD6-B86D-B033A9ADC632");
                cmd.Parameters.Add(new SqlParameter
                {
                    SqlDbType = SqlDbType.UniqueIdentifier,
                    ParameterName = "@Cate",
                    Value = idCategorieAutres
                });

                using (var conn = new SqlConnection(Settings.Default.Northwind2Connect))
                {
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                // Affectation de la catégorie au produit
                produit.CategoryId = idCategorieAutres;
            }

            // Ajout du produit
            AjouterModifierProduit(produit, Operations.Ajout);
        }

		// Enregistrement des modifications du DataSet des produits en base
		public int EnregistrerModifsProduits()
		{
			using (var conn = new SqlConnection(Settings.Default.Northwind2Connect))
			{
				// Affectation de la connexion aux commandes
				_adaptProduits.InsertCommand.Connection = conn;
				_adaptProduits.UpdateCommand.Connection = conn;
				_adaptProduits.DeleteCommand.Connection = conn;

				// Mise à jour de la base
				return _adaptProduits.Update(_dsProduits);
			}
		}
	}
}
