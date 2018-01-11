using Northwind2.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Outils.TConsole;

namespace Northwind2
{
	class Program
	{
		static void Main(string[] args)
		{
			Northwind2App app = Northwind2App.Instance;
			app.Title = "Northwind2";

			// Ajout des pages
			Page accueil = new PageAccueil();
			app.AddPage(accueil);
			app.AddPage(new PageFournisseurs());
			app.AddPage(new PageClientsCommandes());
			app.AddPage(new PageProduits());

			// Affichage de la page d'accueil
			app.NavigateTo(accueil);

			// Lancement de l'appli
			app.Run();
		}
	}

}
