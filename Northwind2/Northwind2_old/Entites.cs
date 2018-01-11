using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Northwind2
{

	public class Supplier
	{
		[Display(ShortName="Id")]
		public int SupplierId { get; set; }
		[Display(ShortName = "Nom")]
		public string CompanyName { get; set; }
		[Display(ShortName="None")]
		public Guid AddressId { get; set; }
		[Display(ShortName="None")]
		public virtual Address Address { get; set; }
	}

	public class Address
	{
		public Guid AddressId { get; set; }
		public string Country { get; set; }
	}

	public class Customer
	{
		[Display(ShortName="Id")]
		public string CustomerId { get; set; }
		[Display(ShortName="Nom")]
		public string CompanyName { get; set; }
		[Display(ShortName="None")]
		public virtual List<Order> Orders { get; set; }
	}

	[Table("Orders")]
	public class Order
	{
		[Display(ShortName="Id")]
		public int OrderId { get; set; }
		[Display(ShortName="None")]
		public string CustomerId { get; set; }
		[Display(ShortName="Date commande")]
		public DateTime OrderDate { get; set; }
		[Display(ShortName="Date envoi")]
		public DateTime? ShippedDate { get; set; }
		[Display(ShortName="Frais")]
		public decimal Freight { get; set; }
		// L'attribut NotMapped indique que la propriété
		// ne sera pas mappée par EF sur un champ de la table
		[NotMapped]
		[Display(ShortName="Articles")]
		public int ItemsCount { get; set; }
		[NotMapped]
		[Display(ShortName="Montant")]
		public decimal Total { get; set; }
	}

	public class Product
	{
		[Display(ShortName="Id")]
		public int ProductId { get; set; }
		[Display(ShortName="Nom")]
		public string Name { get; set; }
		[Display(ShortName="None")]
		public Guid CategoryId { get; set; }
		[Display(ShortName="Fournisseur")]
		public int SupplierId { get; set; }
		[Display(ShortName="PU")]
		public decimal UnitPrice { get; set; }
		[Display(ShortName="Stock")]
		public short UnitsInStock { get; set; }
	}

	public class Category
	{
		[Display(ShortName="Id")]
		public Guid CategoryId { get; set; }
		[Display(ShortName="Nom")]
		public string Name { get; set; }
		[Display(ShortName="Description")]
		public string Description { get; set; }
	}
}
