using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavern_Rush
{
    internal class Warehouse
    {
        private Dictionary<string, int> _stock = new();

        public Warehouse(int tavernLevel, Product product)
        {
            GenerateWarehouse(tavernLevel, products);
        }
        public void ShowInfoStore()
        {
            Console.WriteLine("Ваш склад: ");
            foreach (var item in _stock)
            {
                if (item.Value == 0)
                {
                    continue;
                }
                Console.WriteLine($"{item.Key} в количистве {item.Value}");
            }
            Console.WriteLine();
        }

        public List<string> GetProductsFromWarehouse()
        {
            List<string> products = new List<string>();
            foreach (var item in _stock)
            {
                products.Add(item.Key);
            }
            return products;
        }

        private void GenerateWarehouse(int tavernLevel, List<Product> products)
        {

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].GetAvailabilityByLevel(products[i].ProductType) <= tavernLevel)
                {
                    _stock.Add(products[i].GetName(products[i].ProductType), 10);
                }
            }
        }
    }
}

