using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleShopApp.v2._0.Library.Entities;

namespace ConsoleShopApp.v2._0.Library.Repository
{
    internal static class ProductRepository
    {
        private static readonly Dictionary<string, List<Product>> inventory = new Dictionary<string, List<Product>>()
        {
                {
                    "Nike", new List<Product>()
                {
                        new Product("Nike", "AIR MAX 2017", 102, 1200),
                        new Product("Nike", "LEBRON XV", 103, 2100),
                        new Product("Nike", "KOBE A.D. 1", 104, 1850),
                        new Product("Nike", "NIKE ZOOM KD10", 105, 1300),
                        new Product("Nike", "LEBRON SOLDIER XI SFG", 106, 1800),
                        new Product("Nike", "METCON 4", 107, 2200),
                        new Product("Nike", "AIR MAX PRIME SL", 108, 1000),
                        new Product("Nike", "W AIR ZOOM PEGASUS 34 SHIELD", 109, 2800),
                        new Product("Nike", "ZOOM TRAIN COMMAND", 110, 1750),
                        new Product("Nike", "REAX 8 TR", 111, 2400)
                }
                },
                {
                    "Adidas", new List<Product>()
                    {
                        new Product("Adidas", "TERREX SWIFT R GTX", 201, 1200),
                        new Product("Adidas", "CRAZYTRAIN ELITE M", 202, 1400),
                        new Product("Adidas", "TERREX VOYAGER CW C", 203, 2100),
                        new Product("Adidas", "CRAZY TIME II", 204, 900),
                        new Product("Adidas", "RESPONSE ST M", 205, 1800),
                        new Product("Adidas", "PUREBOOST XPOSE", 206, 1200),
                        new Product("Adidas", "TERREX TIVID MID CP", 207, 1500),
                        new Product("Adidas", "ALPHABOUNCE 1 M", 208, 2800),
                        new Product("Adidas", "CRAZY EXPLOSIVE 201", 209, 3100),
                        new Product("Adidas", "CRAZYTRAIN PRO 3.0 M", 210, 2000)
                    }
                },
                {
                    "Under Armour", new List<Product>()
                    {
                        new Product("Under Armour", "VERGE 2.0 MID GTX", 300, 2000),
                        new Product("Under Armour", "PERFORMANCE SNEAKERS-UA CURRY 4", 301, 2800),
                        new Product("Under Armour", "NEWELL RIDGE LOW", 302, 3100),
                        new Product("Under Armour", "SPEEDFORM GEMINI VENT", 303, 4000),
                        new Product("Under Armour", "HIGHLIGHT DELTA 2", 304, 2500)
                    }
                },
                {
                    "Converse", new List<Product>()
                    {
                        new Product("Converse", "CHUCK TAYLOR ALL STAR", 400, 1000),
                        new Product("Converse", "CHUCK TAYLOR ALL STAR HIGH STREET", 401, 1200),
                        new Product("Converse", "CUP", 402, 1200),
                        new Product("Converse", "CHUCK II", 403, 900),
                        new Product("Converse", "STARKI", 404, 1100),
                        new Product("Converse", "STAR PLAYER", 405, 1400)
                    }
                }
            };

        internal static List<string> GetVendorNames()
        {
            List<string> vendors = inventory.Keys.ToList();
            return vendors;
        }

        internal static List<Product> GetAllProducts()
        {
            List<Product> allProducts = new List<Product>();
            foreach (var item in inventory.Values)
            {
                foreach (var product in item)
                {
                    allProducts.Add(product);
                }
            }
            return allProducts;
        }

        internal static List<Product> GetProductsByVendorName(string input)
        {
            List<Product> allProducts = GetAllProducts();
            return allProducts.Where(product => product.VendorName.ToUpper().Contains(input.ToUpper())).ToList();
        }

        internal static List<Product> GetProductByProductName(string input)
        {
            List<Product> allProducts = GetAllProducts();
            return allProducts.Where(product => product.ProductName.ToUpper().Contains(input.ToUpper())).ToList();
        }
    }
}
