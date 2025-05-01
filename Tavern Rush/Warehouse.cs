namespace Tavern_Rush
{
  internal class Warehouse
  {
    private readonly Dictionary<ProductType, (Product product, int quantity)> _stock = new();
    public bool IsRefillStock { get; private set; }

    public Warehouse(int tavernLevel)
    {
      IsRefillStock = false;
      GenerateWarehouse(tavernLevel);
    }

    public Dictionary<Product, int> GetProductsNameAndQuantity()
    {
      return _stock.Select(item => item.Value).Where(item => item.quantity != 0).ToDictionary();
    }

    public List<Product> GetProductsFromWarehouse()
    {
      return (from item in _stock where item.Value.quantity != 0 select item.Value.product).ToList();
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

    public Dictionary<int, Product> GetAvailableProducts(int tavernLevel)
    {
      Dictionary<int, Product> product = [];
      int i = 1;
      foreach (KeyValuePair<ProductType, (Product product, int quantity)> item in _stock.Where(item =>
                 item.Value.product.GetAvailabilityByLevel(item.Key) <= tavernLevel))
      {
        product.Add(i, item.Value.product);
        i++;
      }

      return product;
    }

    public static bool IsEnoughMoney(int money, int totalSum)
    {
      return money >= totalSum;
    }

    public static int CalculateStockOrderTotal(Dictionary<int, Product> products, int product, int quantity)
    {
      return products.Where(item => item.Key == product).Sum(item => item.Value.Price * quantity);
    }

    public void UseProductForOrder(Product[] orderProducts)
    {
      foreach (Product item in orderProducts)
      {
        var (product, quantity) = _stock[item.ProductType];
        quantity -= 1;
        _stock[item.ProductType] = (product, quantity);
      }
    }

    public void RefillStock(Dictionary<int, Product> products, int productFill, int quantityFill)
    {
      Product item = products.Where(item => item.Key == productFill).Select(item => item.Value).First();
      var (product, quantity) = _stock[item.ProductType];
      quantity += quantityFill;
      _stock[item.ProductType] = (product, quantity);
    }

    public void ActivateRefillStock()
    {
      IsRefillStock = true;
    }

    public void DeactivateRefillStock()
    {
      IsRefillStock = false;
    }
  }
}