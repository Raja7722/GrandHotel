using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
	public interface IDataContext
	{
		// Renvoie la liste des pays des fournisseurs
		IList<string> GetPaysFournisseurs();

		// Renvoie la liste de tous les fournisseurs d'un pays donné
		IList<Supplier> GetFournisseurs(string pays);

		// Renvoie le nombre de produits fournis par l’ensemble des fournisseurs d'un pays
		int GetNbProduits(string pays);

		// Récupère les clients et leur commandes et stocke les données sous forme arborescente
		IList<Customer> GetClientsCommandes();

		// Renvoie la liste des catégories
		IList<Category> GetCatégories();

		// Renvoie la liste des produits d'une catégorie donnée
		IList<Product> GetProduits(Guid idCategorie);

		// Ajoute / modifie un produit
		void AjouterModifierProduit(Product produit, Operations op);

		// Supprime un produit
		void SupprimerProduit(int id);

		// Crée un produit et de la catégories Others
		void AjouterProduitCategorie(Product produit);

		// Enregistrement des modifications des produits en base
		int EnregistrerModifsProduits();
	}
}
