using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Serializer
{
    public class Order
    {
        [XmlAttribute]
        public int OrderId { get; set; }
        [XmlIgnore]
        public string CustomerId { get; set; }
        [XmlAttribute]
        public DateTime OrderDate { get; set; }
        [XmlAttribute]
        public int ShipperId { get; set; }
        [XmlAttribute]
        public DateTime ShippedDate { get; set; }
        [XmlAttribute]
        public decimal Freight { get; set; }

        public List<OrderDetail> Details { get; set; }
    }

    public class OrderDetail
    {
        [XmlIgnore]
        public int OrderId { get; set; }
        [XmlAttribute]
        public int ProductId { get; set; }
        [XmlText]
        public string ProductName { get; set; }
        [XmlAttribute]
        public decimal UnitPrice { get; set; }
        [XmlAttribute]
        public short Quantity { get; set; }
        [XmlAttribute]
        public double Discount { get; set; }
    }

}
