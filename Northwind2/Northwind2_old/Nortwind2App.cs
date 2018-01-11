﻿using Outils.TConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2
{
	/// <summary>
	/// Application console utilisant la base Northwind2
	/// </summary>
	public class Northwind2App : ConsoleApplication
	{
		private static Northwind2App _instance;
		private static IDataContext _dataContext;

		/// <summary>
		/// Obtient l'instance unique de l'application
		/// </summary>
		public static Northwind2App Instance
		{
			get
			{
				if (_instance == null)
					_instance = new Northwind2App();

				return _instance;
			}
		}

		/// <summary>
		/// Obtient le contexte de données associé à l'appli
		/// </summary>
		public static IDataContext DataContext
		{
			get
			{
				if (_dataContext == null)
					_dataContext = new Contexte3();

				return _dataContext;
			}
		}

		// Constructeur
		public Northwind2App()
		{
			// Définition des options de menu à ajouter dans tous les menus de pages
			MenuPage.DefaultOptions.Add(
				new Option("a", "Accueil", () => _instance.NavigateHome()));

			//MenuPage.DefaultOptions.Add(
			//	new Option("p", "Page précédente", () => _instance.NavigateBack()));
		}
	}
}