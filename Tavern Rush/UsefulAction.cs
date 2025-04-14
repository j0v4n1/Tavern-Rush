using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavern_Rush
{
    enum UsefulActionType
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
            List<string> actionNames = new List<string>();

            foreach (var actionName in _actionNames)
            {
                actionNames.Add(_actionNames[actionName.Key]);
            }

            return actionNames.ToArray();
        }
    }
}
