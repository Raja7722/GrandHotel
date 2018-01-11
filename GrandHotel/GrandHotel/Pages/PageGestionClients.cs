using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel.Pages
{
    public class PageGestionClients : MenuPage
    {
        //private IList<string> _clients;
        private List<Client> _clients;

        public PageGestionClients() : base("Clients")
        {
            Menu.AddOption("1", "Afficher la liste des Clients", AfficherClients);
            //Menu.AddOption("2", "Afficher les coordonnées d'un client", AfficherCoordonneesClients);
            //Menu.AddOption("3", "Saisir un nouveau client et son adresse", AfficherNouveauClientAdresse);
            //Menu.AddOption("4", "Ajouter un numéro de téléphone ou une adresse email à un client", AjouterTelEmail);
            //Menu.AddOption("5", "Supprimer un client et ses coordonnées", SupprimerClient);
            //Menu.AddOption("6", "Exporter la liste des clients au format XML", ExporterListeClientsXML);
        }

        private void AfficherClients()
        {

            _clients = GrandHotelApp.DataContext.GetListeClients();
            ConsoleTable.From(_clients).Display("clients");
        }

        //public override void Display()
        //{
        //    // Affichage de la liste des clients
        //    _clients = GrandHotelApp.DataContext.GetListeClients();
        //    ConsoleTable.From(_clients).Display("clients");

        //    // Affichage de la liste des commandes du client sélectionné
        //    //string id = Input.Read<string>("De quel client souhaitez-vous afficher la liste des commandes ? ");
        //    //var commandes = _clients.Where(c => c.CustomerId == id).Select(c => c.Orders).FirstOrDefault();
        //    //ConsoleTable.From(commandes).Display("commandes");
        //}
    }
}
