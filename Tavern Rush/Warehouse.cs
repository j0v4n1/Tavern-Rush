namespace Tavern_Rush
{
    internal class Warehouse
    {
        private readonly Dictionary<ProductType, (Product product, int quantity)> _stock = new();

        public Warehouse(int tavernLevel)
        {
            GenerateWarehouse(tavernLevel);
        }
        public void ShowInfoStore()
        {
            Console.WriteLine("Ваш склад: ");
            foreach (var item in _stock)
            {
                if (item.Value.quantity == 0)
                {
                    continue;
                }
                Console.WriteLine($"{item.Value.product.Name} в количистве {item.Value.quantity}");
            }
            Console.WriteLine();
        }

        public List<Product> GetProductsFromWarehouse()
        {
            List<Product> products = new List<Product>();
            foreach (var item in _stock)
            {
                if (item.Value.quantity == 0)
                {
                    continue;
                }
                products.Add(item.Value.product);
            }
            return products;
        }

        private void GenerateWarehouse(int tavernLevel)
        {
            foreach (ProductType productType in Enum.GetValues(typeof(ProductType)))
            {
                Product productInstanse = new Product(productType);

                if (productInstanse.GetAvailabilityByLevel(productType) <= tavernLevel)
                {
                    _stock.Add(productType, (productInstanse, 5));
                    continue;
                }
                _stock.Add(productType, (productInstanse, 0));
            }
        }

        public void RefillProducts(int tavernLevel)
        {
            Console.WriteLine();
            ShowInfoStore();

            Console.WriteLine("Выберите какие продукты заказать и сколько:");

            foreach (var item in _stock)
            {
                if (item.Value.product.GetAvailabilityByLevel(item.Key) <= tavernLevel)
                {
                    Console.WriteLine($"{item.Value.product.Name} стоит {item.Value.product.Price} золотых 💰");

                }
            }
            Console.ReadKey();
        }

        public void UseProductForOrder(Product[] orderProducts)
        {
            foreach (var item in orderProducts)
            {
                var (product, quantity) = _stock[item.ProductType];
                quantity -= 1;
                _stock[item.ProductType] = (product, quantity);
            }
        }

    }
}

