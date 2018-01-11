using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Northwind2;

namespace Northwind2.Tests
{
    [TestClass]
    public class TestNorthwind2
    {
        private TestContext _context;
        private static IDataContext _dataContext = new Contexte1();

        public TestContext TestContext
        {
            get { return _context; }
            set { _context = value; }
        }

        [ClassInitialize]
        public static void InitClass(TestContext context)
        {
            Debug.WriteLine("Init class 1");
        }

        [TestInitialize]
        public void InitTest()
        {
            Debug.WriteLine("Init test class 1");
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(5, _dataContext.GetNbProduits("France"));
        }

        [ExpectedException(typeof(FormatException))]
        [TestMethod]
        public void TestMethod2()
        {
            DateTime reveillon = Convert.ToDateTime("31-12--2017");
        }

        [TestCleanup]
        public void CleanTest()
        {
            Debug.WriteLine("Clean test class 1");
        }

        [ClassCleanup]
        public static void CleanClass()
        {
            Debug.WriteLine("Clean class 1");
        }

        [AssemblyCleanup()]
        public static void CleanAssembly()
        {
            Debug.WriteLine("Clean Assembly");
        }
    }

    [TestClass]
    public class TestClass2
    {
        [AssemblyInitialize()]
        public static void InitAssembly(TestContext context)
        {
            Debug.WriteLine("Init Assembly ");
        }

        [ClassInitialize]
        public static void InitClass(TestContext context)
        {
            Debug.WriteLine("Init Class 2");
        }

        [TestMethod]
        public void TestMethod()
        {
            Debug.WriteLine("Test Method of class 2");
        }
    }
}
