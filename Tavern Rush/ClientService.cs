namespace Tavern_Rush;

internal class ClientService
{
  public int ServedClients { get; private set; }
  public int EarnedGold { get; private set; }
  public int LostClients { get; private set; }


  public static Dictionary<Client, (Client client, CancellationTokenSource cts)> InitializeClient(Client client)
  {
    var clientWithTokenSource = new Dictionary<Client, (Client, CancellationTokenSource)>();
    var cts = new CancellationTokenSource();
    clientWithTokenSource.Add(client, (client, cts));
    return clientWithTokenSource;
  }

  public static Queue<Client> InitializeClientQueue()
  {
    return new Queue<Client>();
  }

  private async Task StartClientServeTimer(int timeToServe, Queue<Client> clients, CancellationToken token,
    Warehouse warehouse, Order order, Action increaseBalance, Action increaseReputation,
    Action checkReputation, Action decreaseReputation
  )
  {
    try
    {
      await Task.Delay(timeToServe, token);
      HandleBadService(clients, checkReputation, decreaseReputation);
    }
    catch (TaskCanceledException)
    {
      UiManager.ShowShortMessage(ShortMessage.SuccessfulService);
      warehouse.UseProductForOrder(order.GetProductsFromOrder());
      increaseBalance();
      increaseReputation();
      ServedClients++;
      EarnedGold += order.OrderPrice;
      clients.Dequeue();
    }
  }

  private void HandleBadService(Queue<Client> clients, Action checkReputation, Action decreaseReputation)
  {
    UiManager.ShowShortMessage(ShortMessage.BadService);
    clients.Dequeue();
    decreaseReputation();
    LostClients++;
    checkReputation();
  }

  public void ServeClient(GameManager gameManager, Func<int> getMoney, Func<int> getReputation, Client client, Queue<Client> clientQueue,
    CancellationToken token, Action increaseBalance, Action increaseReputation, Action checkReputation,
    Action decreaseReputation, string[] mixedActions, int tavernLevel,
    Dictionary<Client, (Client client, CancellationTokenSource cts)> clientWithTokenSource)
  {
    int[] validCode = GameManager.ConvertArrayToValidCode(mixedActions);
    string[] orderProducts = gameManager.Order.CreateOrder();
    UiManager.ShowInfoStore(gameManager.Warehouse);
    UiManager.ShowTavernInfo(getMoney(), getReputation());
    UiManager.ShowClientInfo(client);
    _ = StartClientServeTimer(client.TimeToServe, clientQueue, token,
      gameManager.Warehouse,
      gameManager.Order, increaseBalance, increaseReputation, checkReputation, decreaseReputation);
    UiManager.ShowOrderProducts(orderProducts);

    UiManager.ShowActions(mixedActions);
    string? orderCode = Console.ReadLine();

    if (int.TryParse(orderCode, out int parsedOrderCode))
    {
      HandleMenuSelection(gameManager, parsedOrderCode, validCode, client, tavernLevel, clientQueue,
        checkReputation, decreaseReputation, clientWithTokenSource);
    }
    else
    {
      UiManager.ShowShortMessage(ShortMessage.IncorrectInput);
      decreaseReputation();
      checkReputation();
    }
  }

  private void CheckClientOrder(GameManager gameManager, int parsedOrderCode, int[] validCode, Client client,
    Queue<Client> clientQueue, Action checkReputation, Action decreaseReputation,
    Dictionary<Client, (Client client, CancellationTokenSource cts)> clientWithTokenSource)
  {
    if (GameManager.IsValidCode(parsedOrderCode.ToString(), validCode, client))
    {
      clientWithTokenSource[client].cts.Cancel();
    }
    else
    {
      HandleBadService(clientQueue, checkReputation, decreaseReputation);
    }
  }

  private void HandleMenuSelection(GameManager gameManager, int parsedOrderCode, int[] validCode, Client client,
    int tavernLevel, Queue<Client> clientQueue, Action checkReputation,
    Action decreaseReputation, Dictionary<Client, (Client client, CancellationTokenSource cts)> clientWithTokenSource)
  {
    if (parsedOrderCode == 7)
    {
      gameManager.Warehouse.ActivateRefillStock();
      gameManager.Warehouse.RefillProducts(gameManager.Warehouse, tavernLevel);
    }
    else
    {
      CheckClientOrder(gameManager, parsedOrderCode, validCode, client, clientQueue, checkReputation,
        decreaseReputation, clientWithTokenSource);
    }
  }
}