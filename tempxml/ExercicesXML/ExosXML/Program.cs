using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Serializer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Chargement des données de la base");
            List<Order> liste = DAL.GetOrders("QUICK");
            Console.WriteLine("{0} commandes chargées", liste.Count);

            Console.WriteLine("Sérialisation en XML avec XMLSerializer");
            DAL.ExporterXml(liste);

            Console.WriteLine("Sérialisation en XML avec XMLWriter");
            DAL.ExporterXml_XmlWriter(liste);

            Console.WriteLine("Désérialisation du fichier XML créé précédemment");
            Console.WriteLine("Exécuter l'application en debug pour voir la liste générée");
            var listeImport = DAL.ImporterXml();
            
            Console.WriteLine("Requêtes Linq To XML sur le fichier des bandes dessinées");
            var albums = DAL.AlbumsAnnées60();
            foreach (var a in albums)
                Console.WriteLine(a);

            DAL.AjouterAuteur();
            DAL.AjouterAlbum();
            DAL.TitreEnMajuscules();
            //DAL.EnregistrerModifs();

            Console.ReadKey();
        }
    }
}
