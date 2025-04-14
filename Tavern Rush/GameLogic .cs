using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavern_Rush
{
    internal class GameLogic
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
                    default:
                        break;
                }
            }
            return validCode;
        }

        public static bool isValidCode(string orderCode, int[] validCode, Client client)
        {
            int mistakes = 0;

            if (!int.TryParse(orderCode, out int result))
            {
                return false;
            }

            for (int i = 0; i < validCode.Length; i++)
            {
                if (validCode[i] != Convert.ToInt32(orderCode[i].ToString()))
                {
                    mistakes++;
                }
            }

            if (mistakes > client.AllowedMistakes)
            {
                return false;
            }
            return true;
        }

        public static string[] mixActions(string[] actions, Random random, string specialAction)
        {
            List<string> mixedActions = new List<string>();
            List<int> usedIndexes = new List<int>();
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
