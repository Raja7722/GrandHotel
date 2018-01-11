using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel
{
    class GrandHotelApp
    {
        private static GrandHotelApp _instance;
        private static IDataContext _dataContext;
        /// <summary>
        /// Obtient l'instance unique de l'application
        /// </summary>
        public static GrandHotelApp Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GrandHotelApp();

                return _instance;
            }
        }

        public static IDataContext DataContext // Singleton
        {
            get
            {
                if (_dataContext == null)
                    _dataContext = new Contexte();

                return _dataContext;
            }
        }

        // Constructeur
        public GrandHotelApp()
        {
            // Définition des options de menu à ajouter dans tous les menus de pages
            MenuPage.DefaultOptions.Add(
                new Option("a", "Accueil", () => _instance.NavigateHome()));

            //MenuPage.DefaultOptions.Add(
            //	new Option("p", "Page précédente", () => _instance.NavigateBack()));
        }
    }
}
