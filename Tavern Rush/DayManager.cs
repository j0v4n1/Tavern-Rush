namespace Tavern_Rush;

internal class DayManager(
  GameManager gameManager,
  Func<int> getMoney,
  Func<int> getReputation,
  Func<string[]> getAllPerksNames,
  Func<string[]> getAllPerksDescribe)
{
  private bool _isWorkingDay;
  private bool _isOpenTavernForClients;

  public void WorkingDay(Client client, Action increaseTavernLevelAndDay, int day,
    Action increaseBalance, Action increaseReputation, string[] mixedActions, Action checkReputation,
    Action decreaseReputation, int tavernLevel)
  {
    var clientService = new ClientService();
    var clientQueue = ClientService.InitializeClientQueue();
    while (_isWorkingDay)
    {
      var clientWithTokenSource = ClientService.InitializeClient(client);

      if (_isOpenTavernForClients && clientQueue.Count <= 0)
      {
        clientQueue.Enqueue(clientWithTokenSource[client].client);
      }

      if (clientQueue.Count > 0)
      {
        clientService.ServeClient(gameManager, getMoney, getReputation, client, clientQueue,
          clientWithTokenSource[client].cts.Token,
          increaseBalance, increaseReputation, checkReputation, decreaseReputation, mixedActions, tavernLevel,
          clientWithTokenSource);
      }
      else
      {
        EndWorkingDay(day, clientService.ServedClients, clientService.EarnedGold,
          clientService.LostClients, increaseTavernLevelAndDay);
      }
    }
  }

  public void TavernClosed(Warehouse warehouse, int tavernLevel)
  {
    while (!_isWorkingDay)
    {
      UiManager.ShowTavernInfo(getMoney(), getReputation());
      UiManager.ShowClosedTavernMenu();
      int menuChoice = ConsoleHelper.ConvertToInt(Console.ReadLine());
      switch (menuChoice)
      {
        case 1:
          gameManager.Warehouse.ActivateRefillStock();
          warehouse.RefillProducts(gameManager.Warehouse, tavernLevel);
          break;
        case 2:
          _isWorkingDay = true;
          _isOpenTavernForClients = true;
          _ = StartEndOfDayTimer();
          break;
        case 3:
          UiManager.ShowAllPerks(getAllPerksNames(), getAllPerksDescribe());
          break;
        default:
          UiManager.ShowShortMessage(ShortMessage.IncorrectInput);
          break;
      }
    }
  }

  private async Task StartEndOfDayTimer()
  {
    await Task.Delay(60000);
    _isOpenTavernForClients = false;
  }

  private void EndWorkingDay(int day, int servedClients, int earnedGold, int lostClients,
    Action increaseTavernLevelAndDay)
  {
    _isWorkingDay = false;
    UiManager.ShowDayReport(day, servedClients, earnedGold,
      lostClients);
    increaseTavernLevelAndDay();
  }
}