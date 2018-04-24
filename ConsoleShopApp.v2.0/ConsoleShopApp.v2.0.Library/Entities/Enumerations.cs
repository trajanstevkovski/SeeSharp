using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleShopApp.v2._0.Library.Entities
{
    public static class Enumerations
    {
        public enum PaymentMethod
        {
            Undifined = 0,
            PayPall = 1,
            CreditCard = 2
        }

        public enum ShippingService
        {
            DELCO = 1,
            MakedonskiPosti = 2
        }

        public enum City
        {
            Undifined = 0,
            Skopje = 1,
            Bitola = 2,
            Ohrid = 3,
            Stip = 4
        }
    }
}
