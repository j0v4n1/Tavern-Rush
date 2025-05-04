namespace Tavern_Rush
{
  internal class Order
  {
    private readonly Dictionary<Product, string> _orderProducts = new();
    public int OrderPrice { get; }

    public Order(int tavernLevel, List<Product> availableProductsInWarehouse, Random random)
    {
      int countProducts = tavernLevel + 1;
      List<int> usedIndexes = [];
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
          _orderProducts.Add(availableProductsInWarehouse[randomIndex],
            availableProductsInWarehouse[randomIndex].Name);
          OrderPrice += availableProductsInWarehouse[randomIndex].Price;
        }
      }
    }

    public string[] CreateOrder()
    {
      return _orderProducts.Select(item => item.Value).ToArray();
    }

    public Product[] GetProductsFromOrder()
    {
      return _orderProducts.Select(item => item.Key).ToArray();
    }

    public static int GenerateCountProducts(int tavernLevel)
    {
      return tavernLevel + 1;
    }
  }
}