﻿using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrandHotel.Pages
{
    class PageGestionFactures : Page
    {
        private IList<string> _factures;

        public PageGestionFactures() : base("Factures")
        {
        }

        public override void Display()
        {
            // Affichage de la liste des factures
            _factures = GrandHotelApp.DataContext.GetFactures();
            ConsoleTable.From(_factures).Display("factures");

            // Affichage de la liste des commandes du client sélectionné
            //string id = Input.Read<string>("De quel client souhaitez-vous afficher la liste des commandes ? ");
            //var commandes = _clients.Where(c => c.CustomerId == id).Select(c => c.Orders).FirstOrDefault();
            //ConsoleTable.From(commandes).Display("commandes");
        }
    }
}
