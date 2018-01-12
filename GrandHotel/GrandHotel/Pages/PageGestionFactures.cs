using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel.Pages
{
    class PageGestionFactures : MenuPage
    {
        private List<Facture> _factures;
        private List<LigneFacture> _ligneFactures;

        public PageGestionFactures() : base("Factures")
        {
            Menu.AddOption("1", "Afficher la liste des factures", AfficherListeFactures);
            Menu.AddOption("2", "Afficher les lignes d'une facture", AfficherLignesFacture);
            Menu.AddOption("3", "Saisir une facture", SaisirFacture);
            Menu.AddOption("4", "Saisir les lignes d'une facture", SaisirLignesFacture);
            Menu.AddOption("5", "Mettre à jour la date et le mode de paiement d'une facture", MAJFacture);
            Menu.AddOption("6", "Exporter les factures d'un client donné avec montant total au format XML", ExporterFactureXML);
        }

        //  Afficher la liste des factures d'un client à partir d'une date donnée
        private void AfficherListeFactures()
        {
            int IdClient = Input.Read<int>("Saisir l'identifiant du client : ");
            string date = Input.Read<string>("Saisir la date à partir de laquelle vous souhaitez voir la liste des factures (format:YYYY-MM-DD) : ");
            _factures = GrandHotelApp.DataContext.GetListFactures(IdClient, date);
            ConsoleTable.From(_factures).Display("Factures");
        }

        // Afficher les lignes d'une facture identifiée par son Id
        private void AfficherLignesFacture()
        {
            // Saisir l'id Id de la facture
            int IdFacture = Input.Read<int>("Saisir l'identifiant de la facture : ");
            _ligneFactures = GrandHotelApp.DataContext.GetLigneFacture(IdFacture);
            ConsoleTable.From(_ligneFactures).Display("Lignes factures");
        }

        // Création d'une nouvelle facture
        private void SaisirFacture()
        {
            // Saisie des infos de la facture
            Output.WriteLine("Saisissez les informations de la facture :");
            Facture fact = new Facture();

            fact.IdClient = Input.Read<int>("Id du client :");
            fact.DateFacture = Input.Read<DateTime>("Date d'émission de la facture (format:YYYY-MM-DD) :");
            fact.DatePaiement = Input.Read<DateTime>("Date de paiement de la facture (format:YYYY-MM-DD) :");
            fact.CodeModePaiement = Input.Read<string>("Moyen de paiement de la facture (CB, CHQ, ESP) :");

            // Enregistrement dans la base
            GrandHotelApp.DataContext.AjouterFacture(fact);
            Output.WriteLine(ConsoleColor.Green, "Facture créée avec succès");
            Output.WriteLine("");
        }

        // Saisie des lignes d'une facture donnée
        private void SaisirLignesFacture()
        {
            int idFac = Input.Read<int>("Saisir l'identifiant de la facture dont vous souhaitez renseigner les lignes:");
            LigneFacture lf = new LigneFacture();

            // Saisie des infos des lignes de facture
            lf.IdFacture = idFac;
            lf.NumLigne = Input.Read<int>("Numéro de la ligne :");
            lf.Quantite = Input.Read<Int16>("Quantité :");
            lf.MontantHT = Input.Read<decimal>("Montant HT de la facture :");
            lf.TauxTVA = Input.Read<decimal>("Taux de TVA :");
            lf.TauxReduction = Input.Read<decimal>("Taux de réduction :");

            // Enregistrement dans la base
            GrandHotelApp.DataContext.AjouterLigneFacture(lf);
            Output.WriteLine(ConsoleColor.Green, "Ligne(s) de facture créée(s) avec succès");
            Output.WriteLine("");
        }

        // Mettre a jour la date et le mode de paiement d'une facture
        private void MAJFacture()
        {
            Console.WriteLine("En construction... \n");
        }

        // Exporter fichier XML (Il faut corriger pour ajouter le montant total)
        private void ExporterFactureXML()
        {
            // Saisir l'id Id du client
            int id = Input.Read<int>("Id du client :");

            // Serialization de la liste des factures
            GrandHotelApp.DataContext.SerializationFactures(id);
            Output.WriteLine(ConsoleColor.Green, "La liste des factures d'un client a bien été exporté.");
        }
    }
}











