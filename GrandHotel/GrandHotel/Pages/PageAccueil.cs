using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel.Pages
{

    public class PageAccueil : MenuPage
    {
        public PageAccueil() : base("Accueil", false)
        {
            Menu.AddOption("0", "Quitter l'application",
                () => Environment.Exit(0));
            Menu.AddOption("1", "Gestion des clients",
                () => GrandHotelApp.Instance.NavigateTo(typeof(PageGestionClients)));
            Menu.AddOption("2", "Gestion des factures",
                () => GrandHotelApp.Instance.NavigateTo(typeof(PageGestionFactures)));
            Menu.AddOption("3", "Résultats de l'hôtel",
                () => GrandHotelApp.Instance.NavigateTo(typeof(PageResultatsHotel)));
        }
    }

}
