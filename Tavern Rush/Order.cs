using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavern_Rush
{
    internal class Order
    {
        private List<string> _orderProducts = new List<string>();
        public int OrderPrice { get; private set; }

        public Order(int countProducts, int tavernLevel, List<ProductType> availableProductsInWarehouse, Random random, Product product)
        {
            List<int> usedIndexes = new List<int>();
            if (tavernLevel < 3)
            {
                for (int i = 0; i < countProducts; i++)
                {
                    _orderProducts.Add(product.GetName(availableProductsInWarehouse[i]));
                }
            }
            else
            {
                for (int i = 0; i < countProducts; i++)
                {
                    int randomIndex = random.Next(0, availableProductsInWarehouse.Count - 1);
                    if (usedIndexes.Contains(randomIndex))
                    {
                        i--;
                        continue;
                    }
                    usedIndexes.Add(randomIndex);
                    _orderProducts.Add(product.GetName(availableProductsInWarehouse[randomIndex]));
                    OrderPrice += product.GetPrice(availableProductsInWarehouse[randomIndex]);
                }
            }
        }

        public string[] CreateOrder()
        {
            return _orderProducts.ToArray();
        }
    }
}
