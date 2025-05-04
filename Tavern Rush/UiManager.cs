namespace Tavern_Rush;

internal enum ShortMessage
{
  SuccessfulService,
  GameOver,
  IncorrectInput,
  ChooseProductToRefill,
  Quantity,
  PressAnyKeyToContinue,
  BadService
}

internal abstract class UiManager
{
  public static void InitializeTavernName()
  {
    Console.WriteLine("Назовите Вашу таверну: ");
    var tavernName = Console.ReadLine();
    Console.WriteLine($"Таверна {tavernName} открыта!\n");
  }

  public static void ShowShortMessage(ShortMessage message)
  {
    switch (message)
    {
      case ShortMessage.SuccessfulService:
        Console.WriteLine("Спасибо, друг! Держи монету\n");
        break;
      case ShortMessage.GameOver:
        Console.WriteLine("Игра окончена!");
        break;
      case ShortMessage.IncorrectInput:
        Console.WriteLine(
          "Неверный формат ввода!");
        break;
      case ShortMessage.ChooseProductToRefill:
        Console.WriteLine("Какие продукты хотите заказать:");
        Console.WriteLine();
        break;
      case ShortMessage.Quantity:
        Console.WriteLine();
        Console.Write("Выберите количество:");
        break;
      case ShortMessage.PressAnyKeyToContinue:
        Console.WriteLine();
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
        Console.WriteLine();
        break;
      case ShortMessage.BadService:
        Console.WriteLine("Мда, даже обслужить нормально не могут. Всего хорошего!\n");
        break;
    }
  }

  private static void ShowClientName(Client client)
  {
    Console.WriteLine($"🧍 Клиент: {client.Name}");
  }

  public static void ShowInfoStore(Warehouse warehouse)
  {
    Console.WriteLine("🏚️ Склад: ");
    Dictionary<Product, int> items = warehouse.GetProductsNameAndQuantity();
    foreach (KeyValuePair<Product, int> item in items)
    {
      Console.WriteLine($"{item.Key.Name} - {item.Value}");
    }

    Console.WriteLine();
  }

  public static void ShowTavernInfo(int money, int reputation)
  {
    Console.WriteLine("🍻 Таверна:");
    Console.WriteLine($"Золото - 💰 {money}");
    Console.WriteLine($"Репутация - 🌟 {reputation}\n");
  }

  public static void ShowClientInfo(Client client)
  {
    ShowClientName(client);
    ShowClientPhrase(client);
  }

  private static void ShowClientPhrase(Client client)
  {
    Console.WriteLine($"{client.GetPhrase(client.Temperament)}");
  }

  public static void ShowDayReport(int day, int servedClients, int earnedGold,
    int lostClients)
  {
    Console.WriteLine("Таверна на сегодня закрыта!");
    Console.WriteLine();
    Console.WriteLine($"День: {day}");
    Console.WriteLine($"Обслужено клиентов: {servedClients}");
    Console.WriteLine($"Заработано денег: {earnedGold}");
    Console.WriteLine($"Клиентов ушло: {lostClients}");
    Console.WriteLine();
  }

  public static void ShowOrderProducts(string[] orderProducts)
  {
    foreach (string product in orderProducts)
    {
      Console.Write($"{product} ");
    }

    Console.WriteLine("\n");
  }

  public static void ShowActions(string[] actions)
  {
    for (int i = 0; i < actions.Length; i++)
    {
      Console.WriteLine($"{i + 1}. {actions[i]}");
    }

    Console.WriteLine();
  }

  public static void ShowAvailableProducts(Dictionary<int, Product> availableProducts)
  {
    Console.Write("Выберите продукт:");
    Console.WriteLine();
    foreach (var item in availableProducts)
    {
      Console.WriteLine($"{item.Key}.{item.Value.Name} стоит {item.Value.Price} золотых 💰");
    }

    Console.WriteLine("0.Выйти");
  }

  public static void ShowMessageNotEnoughMoney()
  {
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("🚨 ### НЕДОСТАТОЧНО ДЕНЕГ! ### 🚨");
    Console.ResetColor();
    ShowShortMessage(ShortMessage.PressAnyKeyToContinue);
  }

  public static void ShowTotalSum(int totalSum)
  {
    Console.WriteLine($"С Вас спишется {totalSum} золотых 💰");
  }

  public static void ShowClosedTavernMenu()
  {
    Console.WriteLine("Что Вы хотите сделать?");
    Console.WriteLine("1. Пополнить склад");
    Console.WriteLine("2. Начать следующий день");
    Console.WriteLine("3. Выбрать улучшение для таверны");
  }

  public static void ShowAllPerks(string[] perksNames, string[] perksDescribe)
  {
    Console.WriteLine();
    Console.WriteLine("Выберите один из доступных улучшений таверны:");
    for (int i = 0; i < perksNames.Length; i++)
    {
      Console.WriteLine($"{i + 1}. {perksNames[i]} - {perksDescribe[i]}");
    }

    Console.WriteLine();
    Console.ReadLine();
  }
}