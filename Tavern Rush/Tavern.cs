namespace Tavern_Rush
{
  internal class Tavern
  {
    private int _money = 20;
    private int _reputation = 50;
    private bool _isGameRunning = true;
    private int _tavernLevel = 1;
    private int _day = 1;
    private int GetMoney() => _money;
    private int GetReputation() => _reputation;
    private readonly Perk _perk = new();

    public void StartGame()
    {
      var gameManager = new GameManager(_tavernLevel, PayForGoods, GetMoney, GetReputation);
      string[] mixedActions = gameManager.InitializeActions(_tavernLevel);

      UiManager.InitializeTavernName();
      var dayManager = new DayManager(gameManager, GetMoney, GetReputation, _perk.GetAllPerksNames,
        _perk.GetAllPerksDescribe);

      while (_isGameRunning)
      {
        var client = new Client(gameManager.Random);
        dayManager.WorkingDay(client, IncreaseTavernLevelAndDay, _day,
          () => IncreaseBalance(gameManager), IncreaseReputation, mixedActions, CheckReputation,
          DecreaseReputation, _tavernLevel);
        dayManager.TavernClosed(gameManager.Warehouse, _tavernLevel);
      }
    }

    private void CheckReputation()
    {
      if (_reputation > 0) return;
      _isGameRunning = false;
      UiManager.ShowShortMessage(ShortMessage.GameOver);
    }

    private void IncreaseTavernLevelAndDay()
    {
      _tavernLevel++;
      _day++;
    }

    private void IncreaseReputation()
    {
      _reputation += 2;
    }

    private void DecreaseReputation()
    {
      _reputation -= 10;
    }

    private void IncreaseBalance(GameManager gameManager)
    {
      _money += gameManager.Order.OrderPrice;
    }

    private bool PayForGoods(int totalSum)
    {
      if (_money < totalSum) return false;
      _money -= totalSum;
      return true;
    }
  }
}