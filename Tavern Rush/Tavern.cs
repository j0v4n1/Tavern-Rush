namespace Tavern_Rush
{
  internal class Tavern
  {
    private int _money = 20;
    private int _reputation = 50;
    private bool _isGameRunning = true;
    private bool _isWorkingDay;
    private bool _isOpenTavernForClients;
    private int _tavernLevel = 1;
    private int _day = 1;
    private int _servedClients;
    private int _earnedGold;
    private int _lostClients;


    public async Task StartGame()
    {
      var random = new Random();
      var warehouse = new Warehouse(_tavernLevel);
      List<Product> productsInWarehouse = warehouse.GetProductsFromWarehouse();
      var order = new Order(GenerateCountProducts(_tavernLevel), _tavernLevel, productsInWarehouse, random);
      var usefulAction = new UsefulAction();
      var harmfulAction = new HarmfulAction();
      string[] harmfulActions = harmfulAction.CreateRandomHarmfulActions(_tavernLevel, random);
      string[] usefulActions = usefulAction.CreateActionNames();
      string[] actions = usefulActions.Concat(harmfulActions).ToArray();
      string[] mixedActionsArray = GameLogic.MixActions(actions, random, "Заказать продукты на склад");
      int[] validCode = GameLogic.ConvertArrayToValidCode(mixedActionsArray);
      Console.WriteLine("Назовите Вашу таверну: ");
      var tavernName = Console.ReadLine();
      Console.WriteLine($"Таверна {tavernName} открыта!\n");

      while (_isGameRunning)
      {
        Queue<Client> clients = new Queue<Client>();
        while (_isWorkingDay)
        {
          var client = new Client(random);
          var cts = new CancellationTokenSource();
          var clientWithToken = Client.GenerateClientWithToken(client, cts);
          if (_isOpenTavernForClients && clients.Count <= 0)
          {
            clients.Enqueue(client);
          }

          if (clients.Count > 0)
          {
            string[] products = order.CreateOrder();
            Console.WriteLine("Создан заказ");
            ShowInfoStore(warehouse);
            ShowTavernInfo();
            Console.WriteLine($"🧍 Клиент: {client.Name}");
            client.SayPhrase();
            Console.Write($"🧾 Заказ: ");
            _ = StartClientServeTimer(client.TimeToServe, clients, clientWithToken[client], warehouse,
              order);
            foreach (string item in products)
            {
              Console.Write($"{item} ");
            }

            Console.WriteLine("\n");

            for (int i = 0; i < mixedActionsArray.Length; i++)
            {
              Console.WriteLine($"{i + 1}. {mixedActionsArray[i]}");
            }

            Console.WriteLine();
            string? orderCode = Console.ReadLine();
            

            if (int.TryParse(orderCode, out int parsedOrderCode))
            {
              if (parsedOrderCode == 7)
              {
                warehouse.ActivateRefillStock();
                RefillProducts(warehouse);
              }
              else
              {
                if (GameLogic.IsValidCode(parsedOrderCode.ToString(), validCode, client))
                {
                  cts.Cancel();
                }
                else
                {
                  HandleBadService(clients);
                }
              }
            }
            else
            {
              Console.WriteLine(
                "Неверный формат ввода! Будьте внимательны, можно растерять всю репутацию");
              _reputation -= 10;
              CheckReputation();
            }
          }
          else
          {
            _isWorkingDay = false;
            ShowDayReport();
          }
        }

        while (!_isWorkingDay)
        {
          ShowTavernInfo();
          Console.WriteLine("Что Вы хотите сделать?");
          Console.WriteLine("1. Пополнить склад");
          Console.WriteLine("2. Начать следующий день");
          Console.WriteLine("3. Выбрать улучшение для таверны");
          int menuChoice = ConsoleHelper.ConvertToInt(Console.ReadLine());
          switch (menuChoice)
          {
            case 1:
              warehouse.ActivateRefillStock();
              RefillProducts(warehouse);
              break;
            case 2:
              _isWorkingDay = true;
              _isOpenTavernForClients = true;
              _ = StartEndOfDayTimer();
              break;
            case 3:
              Console.WriteLine();
              Console.WriteLine("Улучшения будут в слудующем патче");
              Console.WriteLine();
              break;
            case 0:
              Console.WriteLine("Неверный ввод!");
              Console.WriteLine();
              break;
          }
        }
      }
    }

    private static int GenerateCountProducts(int tavernLevel)
    {
      return tavernLevel + 1;
    }

    private void ShowTavernInfo()
    {
      Console.WriteLine("🍻 Таверна:");
      Console.WriteLine($"Золото - 💰 {_money}");
      Console.WriteLine($"Репутация - 🌟 {_reputation}\n");
    }

    private void CheckReputation()
    {
      if (_reputation > 0) return;
      _isGameRunning = false;
      Console.WriteLine("Игра окончена!");
    }

    private void RefillProducts(Warehouse warehouse)
    {
      while (warehouse.IsRefillStock)
      {
        ShowInfoStore(warehouse);
        Console.WriteLine("Какие продукты хотите заказать:");
        Console.WriteLine();
        Dictionary<int, Product> availableProducts = warehouse.GetAvailableProducts(_tavernLevel);
        foreach (KeyValuePair<int, Product> item in availableProducts)
        {
          Console.WriteLine($"{item.Key}.{item.Value.Name} стоит {item.Value.Price} золотых 💰");
        }

        Console.WriteLine("0.Выйти");
        Console.WriteLine();
        Console.Write("Выберите продукт:");
        int product = Convert.ToInt32(Console.ReadLine());
        if (product == 0)
        {
          warehouse.DeactivateRefillStock();
          break;
        }

        Console.WriteLine();
        Console.Write("Выберите количество:");
        int quantity = Convert.ToInt32(Console.ReadLine());
        int totalSum = Warehouse.CalculateStockOrderTotal(availableProducts, product, quantity);
        if (Warehouse.IsEnoughMoney(_money, totalSum))
        {
          Console.WriteLine($"С Вас спишется {totalSum} золотых 💰");
          ConsoleHelper.ShowMessageToContinue();
          warehouse.RefillStock(availableProducts, product, quantity);
          PayForGoods(totalSum);
          ShowTavernInfo();
        }
        else
        {
          Console.WriteLine();
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine("🚨 ### НЕДОСТАТОЧНО ДЕНЕГ! ### 🚨");
          Console.ResetColor();
          ConsoleHelper.ShowMessageToContinue();
        }
      }
    }

    private static void ShowInfoStore(Warehouse warehouse)
    {
      Console.WriteLine("🏚️ Склад: ");
      Dictionary<Product, int> items = warehouse.GetProductsNameAndQuantity();
      foreach (KeyValuePair<Product, int> item in items)
      {
        Console.WriteLine($"{item.Key.Name} - {item.Value}");
      }

      Console.WriteLine();
    }

    private async Task StartEndOfDayTimer()
    {
      await Task.Delay(60000);
      _isOpenTavernForClients = false;
    }

    private async Task StartClientServeTimer(int timeToServe, Queue<Client> clients, CancellationToken token,
      Warehouse warehouse, Order order)
    {
      try
      {
        await Task.Delay(timeToServe, token);
        HandleBadService(clients);
      }
      catch (TaskCanceledException)
      {
        Console.WriteLine("Спасибо, друг! Держи монету\n");
        warehouse.UseProductForOrder(order.GetProductsFromOrder());
        _money += order.OrderPrice;
        _reputation += 2;
        _servedClients++;
        _earnedGold += order.OrderPrice;
        clients.Dequeue();
      }
    }

    private void ShowDayReport()
    {
      Console.WriteLine("Таверна на сегодня закрыта!");
      Console.WriteLine();
      Console.WriteLine($"День: {_day}");
      Console.WriteLine($"Обслужено клиентов: {_servedClients}");
      Console.WriteLine($"Заработано денег: {_earnedGold}");
      Console.WriteLine($"Клиентов ушло: {_lostClients}");
      Console.WriteLine();
      _day += 1;
      _tavernLevel += 1;
    }

    private void PayForGoods(int totalSum)
    {
      _money -= totalSum;
    }

    private void HandleBadService(Queue<Client> clients)
    {
      Console.WriteLine("Мда, даже обслужить нормально не могут. Всего хорошего!\n");
      clients.Dequeue();
      _reputation -= 10;
      _lostClients++;
      CheckReputation();
    }
  }
}