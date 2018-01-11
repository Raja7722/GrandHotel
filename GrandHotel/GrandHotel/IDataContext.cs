using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GrandHotel.Entites;

namespace GrandHotel
{

    public interface IDataContext
    {
        // Renvoie la liste des clients
        IList<string> GetClients();

        // Afficher les coordonnées d'un client
        IList<Client> GetCoordonneesClient(string ClientId);

        // Ajouter un nouveau client et son adresse
        void AjouterClientAdresse(Client client);


        // Ajouter un numéro de téléphone ou une adresse email à un client


        // Supprimer un client et ses informations liées (adresse, téléphone, email) si ce client n'est associé à 

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
