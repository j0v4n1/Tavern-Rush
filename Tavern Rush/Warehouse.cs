namespace Tavern_Rush
{
  internal class Warehouse
  {
    private readonly Dictionary<ProductType, (Product product, int quantity)> _stock = new();
    private bool _isRefillStock;
    private readonly Func<int, bool> _payForGoods;
    private readonly Func<int> _getMoney;
    private readonly Func<int> _getReputation;

    public Warehouse(int tavernLevel, Func<int, bool> payForGoods, Func<int> getMoney, Func<int> getReputation)
    {
      _isRefillStock = false;
      GenerateWarehouse(tavernLevel);
      _payForGoods = payForGoods;
      _getMoney = getMoney;
      _getReputation = getReputation;
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

    private Dictionary<int, Product> GetAvailableProducts(int tavernLevel)
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

    private static bool IsEnoughMoney(int money, int totalSum)
    {
      return money >= totalSum;
    }

    private static int CalculateStockOrderTotal(Dictionary<int, Product> products, int product, int quantity)
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

    private void RefillStock(Dictionary<int, Product> products, int productFill, int quantityFill)
    {
      Product item = products.Where(item => item.Key == productFill).Select(item => item.Value).First();
      var (product, quantity) = _stock[item.ProductType];
      quantity += quantityFill;
      _stock[item.ProductType] = (product, quantity);
    }

    public void ActivateRefillStock()
    {
      _isRefillStock = true;
    }

    private void DeactivateRefillStock()
    {
      _isRefillStock = false;
    }

    public void RefillProducts(Warehouse warehouse, int tavernLevel)
    {
      while (warehouse._isRefillStock)
      {
        UiManager.ShowInfoStore(warehouse);
        UiManager.ShowShortMessage(ShortMessage.ChooseProductToRefill);
        Dictionary<int, Product> availableProducts = warehouse.GetAvailableProducts(tavernLevel);
        UiManager.ShowAvailableProducts(availableProducts);
        int product = Convert.ToInt32(Console.ReadLine());
        if (product == 0)
        {
          DeactivateRefillStock();
          break;
        }

        UiManager.ShowShortMessage(ShortMessage.Quantity);
        int quantity = Convert.ToInt32(Console.ReadLine());
        int totalSum = CalculateStockOrderTotal(availableProducts, product, quantity);
        
        if (IsEnoughMoney(_getMoney(), totalSum))
        {
          UiManager.ShowTotalSum(totalSum);
          UiManager.ShowShortMessage(ShortMessage.PressAnyKeyToContinue);
          warehouse.RefillStock(availableProducts, product, quantity);
          _payForGoods(totalSum);
          UiManager.ShowTavernInfo(_getMoney(), _getReputation());
        }
        else
        {
          UiManager.ShowMessageNotEnoughMoney();
        }
      }
    }
  }
}