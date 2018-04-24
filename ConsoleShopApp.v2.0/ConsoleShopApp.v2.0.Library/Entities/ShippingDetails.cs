using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleShopApp.v2._0.Library.Entities
{
    public class ShippingDetails
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public Enumerations.ShippingService ShippingService { get; set; }
        public Enumerations.City City { get; set; }

        public ShippingDetails(string street, string number, int city, int shipingService)
        {
            this.Street = street;
            this.Number = number;
            this.City = SetCity(city);
            this.ShippingService = SetShippingService(shipingService);
        }

        private Enumerations.ShippingService SetShippingService(int service)
        {
            if (service == 2)
            {
                return Enumerations.ShippingService.MakedonskiPosti;
            }
            else return Enumerations.ShippingService.DELCO;
        }

        private Enumerations.City SetCity(int city)
        {
            switch (city)
            {
                case 1:
                    return Enumerations.City.Skopje;
                case 2:
                    return Enumerations.City.Bitola;
                case 3:
                    return Enumerations.City.Ohrid;
                case 4:
                    return Enumerations.City.Stip;
                default:
                    return Enumerations.City.Undifined;
            }
        }

        public void ShippingDetailsInfo()
        {
            Console.WriteLine($"\tCity: {City} , Shipping service: {ShippingService}, Street: {Street} {Number}\n");
        }
    }
}
