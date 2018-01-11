using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
	public class Contexte3 : DbContext, IDataContext
	{
		public DbSet<Supplier> Fournisseurs { get; set; }
		public DbSet<Customer> Clients { get; set; }
		public DbSet<Category> Catégories { get; set; }
		public DbSet<Product> Produits { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}

		public Contexte3() : base("name=Northwind2.Properties.Settings.Northwind2Connect")
		{	}

		public IList<string> GetPaysFournisseurs()
		{
            return Fournisseurs.AsNoTracking().Select(s => s.Address.Country).Distinct().ToList();
			// On pourrait aussi écrire :
            //return Fournisseurs.AsNoTracking().Select(s => s.Address)
			//			.Select(a => a.Country).Distinct().ToList();
        }

		public IList<Supplier> GetFournisseurs(string pays)
		{
			return Fournisseurs.AsNoTracking().Where(s => s.Address.Country == pays).ToList();
		}

        public int GetNbProduits(string pays)
		{
			var param = new System.Data.SqlClient.SqlParameter
			{
				SqlDbType = System.Data.SqlDbType.NVarChar,
				ParameterName = "@pays",
				Value = pays
			};

			return Database.SqlQuery<int>(@"select COUNT(*) NbProduits
				from Product p
				inner join Supplier s on (p.SupplierId = s.SupplierId)
				inner join Address a on s.AddressId = a.AddressId
				where a.Country = @pays", param).Single();
		}

		public IList<Customer> GetClientsCommandes()
		{
			return Clients.AsNoTracking().Include(c => c.Orders).ToList();
		}

		public IList<Category> GetCatégories()
		{
            Catégories.Load();
            return Catégories.Local;
		}

		public IList<Product> GetProduits(Guid idCategorie)
		{
            // Charge les produits depuis la base
            Produits.Where(p => p.CategoryId == idCategorie).Load();
            // Renvoie la vue locale, qui contient en plus les produits ajoutés
            // et pas encore enregistrés en base
            return Produits.Local.Where(p => p.CategoryId == idCategorie).ToList();
		}

		public void AjouterModifierProduit(Product produit, Operations op)
		{
			if (op == Operations.Ajout)
				Produits.Add(produit);
			else
			{
				Product prod = Produits.Find(produit.ProductId);
                if (prod != null)
                {
                    prod.CategoryId = produit.CategoryId;
                    prod.SupplierId = produit.SupplierId;
                    prod.Name = produit.Name;
                    prod.UnitPrice = produit.UnitPrice;
                    prod.UnitsInStock = produit.UnitsInStock;
                }
			}
		}

		public void SupprimerProduit(int id)
		{
			Product prod = Produits.Find(id);
			if (prod != null) Produits.Remove(prod);
		}

		public void AjouterProduitCategorie(Product produit)
		{
            if (produit.CategoryId == Guid.Empty)
            {
                // Recherche de la catégorie "Autres produits"
                Guid idCategorieAutres = new Guid("D77CBDA2-DD29-4CD6-B86D-B033A9ADC632");
                Category cate = Catégories.Find(idCategorieAutres);
            
                // Si elle n'existe pas on la crée, et on l'affecte au produit
                if (cate == null)
                {
                    cate = new Category { CategoryId = idCategorieAutres, Name = "Others", Description = "Other products" };
                    Catégories.Add(cate);
                }

                produit.CategoryId = idCategorieAutres;
            }

            // Ajout du produit
            Produits.Add(produit);
        }

        // Enregistrement des modifications des produits en base
        public int EnregistrerModifsProduits()
		{
            return SaveChanges();
		}
	}
}
