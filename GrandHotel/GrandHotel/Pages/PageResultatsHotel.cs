using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel.Pages
{
    class PageResultatsHotel : MenuPage
    {


        public PageResultatsHotel() : base("Hotel")
        {
            Menu.AddOption("1", "Liste des clients qui n'ont pas de numéro de portable", AfficherClientsSansMobile);
            Menu.AddOption("2", "Taux moyen de réservation de l'hôtel par mois-année", AfficherTauxMoyenRéservation);
            Menu.AddOption("3", "Nombre quotidien moyen de clients présents dans l'hôtel pour chaque mois de l'année 2017", FrequenceClients2017);
            Menu.AddOption("4", "Chiffre d'affaires de l'hôtel par trimestre de chaque année", CATrimestreAnnee);
            Menu.AddOption("5", "Nombre de clients par tranche du CA", NbClientsCA);
        }

        private void AfficherClientsSansMobile()
        {
            //--Requête pour récupérer les clients qui n'ont pas de numéro de portable
            //create view vIdClientSansMobile as (
            //select T.IdClient from Telephone T
            //except
            //select T.IdClient from Telephone T where CodeType = 'M')
            //GO
            
            //select IdClient, C.Civilite, C.Nom, C.Prenom
            //from vIdClientSansMobile vICSM
            //inner join Client C on C.Id = vICSM.IdClient

            Console.WriteLine("En construction... \n");
        }

        private void AfficherTauxMoyenRéservation()
        {
            Console.WriteLine("En construction... \n");
        }

        private void FrequenceClients2017()
        {
            Console.WriteLine("En construction... \n");
        }

        private void CATrimestreAnnee()
        {
            Console.WriteLine("En construction... \n");

            //-- CA par trimestre de chaque année
            //select year(DateFacture), DATEPART(quarter, F.DateFacture), Sum(Quantite * MontantHT * (1 - TauxTVA) * (1 - TauxReduction))
            //from Facture F
            //inner
            //join LigneFacture LF on F.Id = LF.IdFacture
            //group by year(DateFacture), DATEPART(quarter, F.DateFacture)
            //order by 1, 2
        }

        private void NbClientsCA()
        {
            Console.WriteLine("En construction... \n");
        }
    }
}
