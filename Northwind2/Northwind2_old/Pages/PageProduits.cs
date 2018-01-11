using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Northwind2.Pages
{
	public class PageProduits : MenuPage
	{
		public PageProduits() : base("Produits")
		{
			Menu.AddOption("1", "Liste des produits", AfficherProduits);
			Menu.AddOption("2", "Créer un nouveau produit", CréerProduit);
			Menu.AddOption("3", "Modifier un produit", ModifierProduit);
			Menu.AddOption("4", "Supprimer un produit", SupprimerProduit);
			Menu.AddOption("5", "Créer un nouveau produit sans catégorie", CréerProduitSansCatégorie);

			// Il ne faut pas ajouter le menu d'enregistrement si on utilise Contexte1
			if (!(Northwind2App.DataContext is Contexte1))
				Menu.AddOption("6", "Enregistrer les modifications", EnregistrerModifsProduits);

			// Code spécifique au Contexte2
			if (Northwind2App.DataContext is Contexte2)
				(Northwind2App.DataContext as Contexte2).InitDataSetProduits();
		}

        // Affichage des produits
        private void AfficherProduits()
		{
            Guid idCate;
            IList<Product> produits;
            AfficherProduitsDeCategorie(out idCate, out produits);
        }

        // Affiche et renvoie les produits d'une catégorie saisie par l'utilisateur
        private void AfficherProduitsDeCategorie(out Guid idCate, out IList<Product> produits)
        {
            // Affiche la liste des catégories et demande la saisie d'un id de catégorie
            var catégories = Northwind2App.DataContext.GetCatégories();
            ConsoleTable.From(catégories).Display("catégories");
            idCate = Input.Read<Guid>("Saisissez l'id de la catégorie dont vous souhaitez voir les produits :");

            // Récupération et affichage de la liste des produits de cette catégorie
            produits = Northwind2App.DataContext.GetProduits(idCate);
            ConsoleTable.From(produits).Display("produits");
        }

        private void CréerProduit()
		{
			// Affichage des catégories
			var catégories = Northwind2App.DataContext.GetCatégories();
			ConsoleTable.From(catégories).Display("catégories");

			// Saisie des infos du produit
			Output.WriteLine("Saisissez les informations du produit :");
			Product prod = new Product();
			prod.CategoryId = Input.Read<Guid>("Id de la catégorie :");
			prod.Name = Input.Read<String>("Nom :");
			prod.SupplierId = Input.Read<int>("Id du fournisseur :");
			prod.UnitPrice = Input.Read<decimal>("Prix unitaire :");
			prod.UnitsInStock = Input.Read<short>("Unités en stock (nombre entier) :");

			// Enregistrement dans la base
			Northwind2App.DataContext.AjouterModifierProduit(prod, Operations.Ajout);
			Output.WriteLine(ConsoleColor.Green, "Produit créé avec succès");
			Output.WriteLine("");
		}

		private void ModifierProduit()
		{
            // Affiche la liste des catégories puis des produits de la catégorie sélectionnée
            Guid idCate;
            IList<Product> produits;
            AfficherProduitsDeCategorie(out idCate, out produits);
			
			// Récupère le produit dont l'id a été saisi
			int id = Input.Read<int>("Id du produit à modifier :");
			Product prod = produits.Where(p => p.ProductId == id).FirstOrDefault();
            if (prod != null)
            {
                prod.CategoryId = idCate;

                // Ddemande les nouvelles valeurs des infos du produit, en proposant les valeurs actuelles par défaut
                Output.WriteLine("Modifiez chaque information du produit ou appuyez sur Entrée pour conserver la valeur actuelle :");
                prod.Name = Input.Read<String>("Nom :", prod.Name);
                prod.CategoryId = Input.Read<Guid>("Id de la catégorie :", prod.CategoryId);
                prod.SupplierId = Input.Read<int>("Id du fournisseur :", prod.SupplierId);
                prod.UnitPrice = Input.Read<decimal>("Prix unitaire : ", prod.UnitPrice);
                prod.UnitsInStock = Input.Read<short>("Unités en stock (nombre entier) :", prod.UnitsInStock);

                // Enregistrement dans la base
                Northwind2App.DataContext.AjouterModifierProduit(prod, Operations.Modification);
                Output.WriteLine(ConsoleColor.Green, "Produit modifié avec succès");
            }
            else
                Output.WriteLine(ConsoleColor.Red, "L'id du produit n'existe pas");

            Output.WriteLine("");
        }

		private void SupprimerProduit()
		{
			// Affiche la liste des catégories puis des produits de la catégorie sélectionnée
			AfficherProduits();

			int id = Input.Read<int>("Id du produit à supprimer :");
			try
			{
				Northwind2App.DataContext.SupprimerProduit(id);
			}
			catch (SqlException e)
			{
				GérerErreurSql(e);
			}
		}

		private void CréerProduitSansCatégorie()
		{
			// Saisie des infos du produit
			Output.WriteLine("Saisissez les informations du produit :");
			Product prod = new Product();
			prod.Name = Input.Read<String>("Nom :");
			prod.SupplierId = Input.Read<int>("Id du fournisseur :");
			prod.UnitPrice = Input.Read<decimal>("Prix unitaire :");
			prod.UnitsInStock = Input.Read<short>("Unités en stock (nombre entier) :");

			Northwind2App.DataContext.AjouterProduitCategorie(prod);
			Output.WriteLine(ConsoleColor.Green, "Produit créé avec succès");
			Output.WriteLine("");
		}

		private void EnregistrerModifsProduits()
		{
			try
			{
                Northwind2App.DataContext.EnregistrerModifsProduits();
			}
			catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
			{
				var innerEx = (ex.InnerException.InnerException as SqlException);
				if (innerEx != null & innerEx.Number == 547)
					GérerErreurSql(innerEx);
			}
			catch (SqlException ex)
			{
				GérerErreurSql(ex);
			}
		}

		private void GérerErreurSql(SqlException ex)
		{
			if (ex.Number == 547)
				Output.WriteLine(ConsoleColor.Red,
					"Le produit ne peut pas être supprimé car il est référencé par une commande");
			else
				throw ex;
		}
	}
}
