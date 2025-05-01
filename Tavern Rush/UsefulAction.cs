namespace Tavern_Rush
{
  internal enum UsefulActionType
  {
    TakeOrder,
    DeliverOrder,
    PlaceOnTray,
    TakeProduct,
    PourDrink,
    RestockWarehouse
  }

  internal class UsefulAction
  {
    private readonly Dictionary<UsefulActionType, string> _actionNames = new()
    {
      { UsefulActionType.TakeOrder, "Принять заказ" },
      { UsefulActionType.DeliverOrder, "Отдать заказ" },
      { UsefulActionType.PlaceOnTray, "Поставить на поднос" },
      { UsefulActionType.TakeProduct, "Взять еду" },
      { UsefulActionType.PourDrink, "Налить напиток" },
      { UsefulActionType.RestockWarehouse, "Заказать продукты на склад" }
    };

    public string[] CreateActionNames()
    {
      List<string> actions = [];
      actions.AddRange(_actionNames.Select(action => action.Value));
      return actions.ToArray();
    }
  }
}