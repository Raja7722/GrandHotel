using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind2.Tests
{
    [TestClass()]
    public class TestsContexte1
    {
        private static IDataContext _dataContext;

        [ClassInitialize]
        public static void InitClass(TestContext context)
        {
            _dataContext = new Contexte1();
        }

        [TestMethod()]
        public void TestGetPaysFournisseurs()
        {
            var pays = _dataContext.GetPaysFournisseurs();
            Assert.AreEqual(16, pays.Count);
            Assert.AreEqual("USA", pays[pays.Count - 1]);
        }

        [TestMethod()]
        public void TestGetFournisseurs()
        {
            var fournis = _dataContext.GetFournisseurs("Japan");
            Assert.AreEqual(4, fournis[0].SupplierId);
            Assert.AreEqual(6, fournis[1].SupplierId);
        }

        [TestMethod()]
        public void TestGetNbProduits()
        {
            Assert.AreEqual(7, _dataContext.GetNbProduits("UK"));
        }

        [TestMethod()]
        public void TestGetClientsCommandes()
        {
            var cliCom = _dataContext.GetClientsCommandes();
            Assert.AreEqual(91, cliCom.Count);
            var cli = cliCom.Where(c => c.CustomerId == "RANCH").FirstOrDefault();
            Assert.AreEqual(5, cli.Orders.Count);
        }

        [TestMethod()]
        public void TestGetCatégories()
        {
            var categories = _dataContext.GetCatégories();
            Assert.AreEqual(8, categories.Count);
            Assert.AreEqual("Seafood", categories.LastOrDefault().Name);
        }

        [TestMethod()]
        public void TestGetProduits()
        {
            Guid idCate = Guid.Parse("EB4E5F53-8711-4118-946E-F89E00615EC6");
            var produits = _dataContext.GetProduits(idCate);
            Assert.AreEqual(12, produits.Count);
            Assert.AreEqual(40, produits[6].ProductId);
        }

        [TestMethod()]
        public void TestAjouterModifierProduit()
        {
            Guid idCateLaitiers = Guid.Parse("323734F8-A4AC-4D92-B4E5-A4E896FC32A2");
            Product prod = new Product
            {
                CategoryId = idCateLaitiers,
                Name = "Essai",
                SupplierId = 1
            };
            _dataContext.AjouterModifierProduit(prod, Operations.Ajout);
            Assert.AreEqual(11, _dataContext.GetProduits(idCateLaitiers).Count);
        }

        [TestMethod()]
        public void TestSupprimerProduit()
        {
            Guid idCateLaitiers = Guid.Parse("323734F8-A4AC-4D92-B4E5-A4E896FC32A2");
            // On récupère le plus grand Id de produit parmis les produist de cette catégorie
            var idProd = _dataContext.GetProduits(idCateLaitiers).LastOrDefault().ProductId;
            // On supprime ce produit
            _dataContext.SupprimerProduit(idProd);
            // On vérifie à nouveau le nombre de produits
            Assert.AreEqual(10, _dataContext.GetProduits(idCateLaitiers).Count);
        }

        [TestMethod()]
        public void TestAjouterProduitCategorie()
        {
            Product prod = new Product
            {
                Name = "Produit sans catégorie",
                SupplierId = 1
            };
            _dataContext.AjouterProduitCategorie(prod);
            Assert.AreEqual(9, _dataContext.GetCatégories().Count);
        }

        [TestMethod()]
        public void TestEnregistrerModifsProduits()
        {
            Assert.AreEqual(0, _dataContext.EnregistrerModifsProduits());
        }
    }
}