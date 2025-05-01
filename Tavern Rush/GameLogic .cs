namespace Tavern_Rush
{
  internal abstract class GameLogic
  {
    public static int[] ConvertArrayToValidCode(string[] actions)
    {
      int[] validCode = new int[5];
      for (int i = 0; i < actions.Length; i++)
      {
        switch (actions[i])
        {
          case "Принять заказ":
            validCode[0] = i + 1;
            break;
          case "Взять еду":
            validCode[1] = i + 1;
            break;
          case "Налить напиток":
            validCode[2] = i + 1;
            break;
          case "Поставить на поднос":
            validCode[3] = i + 1;
            break;
          case "Отдать заказ":
            validCode[4] = i + 1;
            break;
        }
      }

      return validCode;
    }

    public static bool IsValidCode(string orderCode, int[] validCode, Client client)
    {
      int mistakes = 0;

      if (orderCode.Length != validCode.Length)
      {
        if (orderCode.Length > validCode.Length)
        {
          mistakes = orderCode.Length - validCode.Length;
        }
        else
        {
          int differenceLength = validCode.Length - orderCode.Length;
          for (int i = 0; i < differenceLength; i++)
          {
            orderCode += "0";
          }
        }
      }

      mistakes += validCode.Where((t, i) => t != Convert.ToInt32(orderCode[i].ToString())).Count();

      return mistakes <= client.AllowedMistakes;
    }

    public static string[] MixActions(string[] actions, Random random, string specialAction)
    {
      List<string> mixedActions = [];
      List<int> usedIndexes = [];
      string tempAction = "";
      for (int i = actions.Length - 1; i >= 0; i--)
      {
        int randomIndex = random.Next(0, actions.Length);
        if (usedIndexes.Contains(randomIndex))
        {
          i++;
          continue;
        }

        if (actions[randomIndex] == specialAction)
        {
          tempAction = actions[randomIndex];
          usedIndexes.Add(randomIndex);
          continue;
        }

        usedIndexes.Add(randomIndex);
        mixedActions.Add(actions[randomIndex]);
      }

      mixedActions.Add(tempAction);
      string[] mixedActionsArray = mixedActions.ToArray();
      return mixedActionsArray;
    }
  }
}