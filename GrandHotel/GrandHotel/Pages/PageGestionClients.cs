using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel.Pages
{
    public class PageGestionClients : MenuPage
    {
        //private IList<string> _clients;
        private List<Client> _clients;
        private List<ClientCoordonnees> _coordonnees;

        private List<Telephone> _telephones;
        private List<Adresse> _adresses;
        private List<Email> _emails;

        public PageGestionClients() : base("Clients")
        {
            Menu.AddOption("1", "Afficher la liste des Clients", AfficherClients);
            Menu.AddOption("2", "Afficher les coordonnées d'un client", AfficherCoordonneesClients);
            Menu.AddOption("3", "Saisir un nouveau client et son adresse", CreerClient);
            Menu.AddOption("4", "Ajouter un numéro de téléphone ou une adresse email à un client", AjouterTelephoneEmail);
            Menu.AddOption("5", "Supprimer un client et ses coordonnées", SuppClient);
            Menu.AddOption("6", "Exporter la liste des clients au format XML", ExporterListeClientsXML);
        }

        // Afficher la liste des clients
        private void AfficherClients()
        {

            _clients = GrandHotelApp.DataContext.GetListeClients();
            ConsoleTable.From(_clients).Display("clients");
        }

        // Affichage des coordonnees du client sélectionné
        private void AfficherCoordonneesClients()
        {
            int id = Input.Read<int>("Saisir l'identifiant du client dont vous voulez voir les coordonnées : ");
            _coordonnees = GrandHotelApp.DataContext.GetCoordonneesClient(id);
            ConsoleTable.From(_coordonnees).Display("coordonnees");

        }

        // Création d'un nouveau client et de son adresse
        private void CreerClient()
        {


            // Saisie des infos du client
            Output.WriteLine("Saisissez les informations du client :");
            Client clt = new Client();

            clt.Civilite = Input.Read<string>("Civilité :");
            clt.Nom = Input.Read<string>("Nom du client :");
            clt.Prenom = Input.Read<string>("Prénom du client :");
            //string temp;
            //temp = Input.Read<string>("Carte de fidélité : ");
            //clt.CarteFidelite = Convert.ToBoolean(temp);
            clt.CarteFidelite = Input.Read<bool>("Carte de fidélité (true/false): ");
            clt.Societe = Input.Read<string>("Société:");

            // Saisie de l'adresse du client
            Output.WriteLine("Saisissez les informations concernant l'adresse du client :");
            Adresse ad = new Adresse();

            ad.Rue = Input.Read<string>("Rue :");
            ad.Complement = Input.Read<string>("Complément :");
            ad.CodePostal = Input.Read<string>("Code Postal :");
            ad.Ville = Input.Read<string>("Ville : ");


            // Enregistrement dans la base
            GrandHotelApp.DataContext.AjouterClientAdresse(clt, ad);
            Output.WriteLine(ConsoleColor.Green, "Client créé avec succès");
            Output.WriteLine("");
        }

        // Ajout d'un nouveau numéro ou adresse email
        private void AjouterTelephoneEmail()
        {
            Telephone tel = new Telephone();
            Email email = new Email();


            bool test = false;
            while (!test)
            {
                Console.WriteLine("Souhaitez-vous ajouter: \n1.un numéro de téléphone \nou \n2.un email? \nTaper 1 ou 2");
                string reponse = Console.ReadLine();
                // Affichage de la liste des clients
                AfficherClients();
                //var client = GrandHotelApp.DataContext.GetListeClients();
                //ConsoleTable.From(client).Display("clients");

                // Saisie de l'id du client
                int id = Input.Read<int>("Saisissez l'identifiant du client :");

                switch (reponse)
                {
                    // Ajouter un numéro de téléphone
                    case "1":
                        // Saisie du numéro de téléphone
                        tel.IdClient = id;
                        tel.Numero = Input.Read<string>("Saisissez le numéro de téléphone : ");
                        tel.CodeType = Input.Read<string>("Saisissez le type de code (F/M): ");
                        tel.Pro = Input.Read<bool>("Est-ce un téléphone professionnel? (true/false) ");
                        test = true;
                        GrandHotelApp.DataContext.AjouterTel(tel);
                        break;

                    // Ajouter un email
                    case "2":
                        // Saisie d'un email
                        email.IdClient = id;
                        email.Adresse = Input.Read<string>("Saisissez l'adresse email : ");
                        email.Pro = Input.Read<bool>("Est-ce un email professionnel? (true/false) ");
                        test = true;
                        GrandHotelApp.DataContext.AjouterMail(email);
                        break;

                    default:
                        Console.WriteLine("Mauvaise saisie");
                        test = false;
                        break;
                }
                // Enregistrement dans la base

                Output.WriteLine(ConsoleColor.Green, "Modifications validées");
            }
        }

        // Supprimer un client
        private void SuppClient()
        {
            // Affiche la liste des clients avec leur id
            AfficherClients();

            // Saisir l'id Id du client à supprimer
            int id = Input.Read<int>("Id du client à supprimer :");

            // Supprimer le client si il n'est pas associé à aucune facture ni occupation de chambre
            GrandHotelApp.DataContext.SupprimerClient(id);
        }

        // Exporter fichier XML
        private void ExporterListeClientsXML()
        {
            // Serialization de la liste des clients
            GrandHotelApp.DataContext.SerializationClient();
            Output.WriteLine(ConsoleColor.Green, "La liste des clients a bien été exporté.");
            Console.ReadLine();
            //Console.WriteLine("En construction\n");
        }
    }
}
