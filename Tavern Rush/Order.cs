namespace Tavern_Rush
{
    internal class Order
    {
        private readonly Dictionary<Product, string> _orderProducts = new Dictionary<Product, string>();
        public int OrderPrice { get; private set; }

        public Order(int countProducts, int tavernLevel, List<Product> availableProductsInWarehouse, Random random)
        {
            List<int> usedIndexes = new List<int>();
            if (tavernLevel < 3)
            {
                for (int i = 0; i < countProducts; i++)
                {
                    _orderProducts.Add(availableProductsInWarehouse[i], availableProductsInWarehouse[i].Name);
                    OrderPrice += availableProductsInWarehouse[i].Price;
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
                    _orderProducts.Add(availableProductsInWarehouse[randomIndex], availableProductsInWarehouse[randomIndex].Name);
                    OrderPrice += availableProductsInWarehouse[randomIndex].Price;
                }
            }
        }

        public string[] CreateOrder()
        {
            List<string> order = new List<string>();

            foreach (var item in _orderProducts)
            {
                order.Add(item.Value);
            }

            return order.ToArray();
        }

        public Product[] GetProductsFromOrder()
        {
            List<Product> order = new List<Product>();

            foreach (var item in _orderProducts)
            {
                order.Add(item.Key);
            }

            return order.ToArray();
        }
    }
}
