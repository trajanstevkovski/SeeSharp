using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleShopApp.v2._0.Library.Entities
{
    public class ShippedOrder
    {
        public string OrderId { get; set; }
        public int ProductsCount { get; set; }
        public int Price { get; set; }

        public ShippedOrder(int productCount, int price)
        {
            this.OrderId = "#" + (DateTime.Now.Year + DateTime.Now.Second - DateTime.Now.Minute - DateTime.Now.Millisecond).ToString();
            this.ProductsCount = productCount;
            this.Price = price;
        }
    }
}
