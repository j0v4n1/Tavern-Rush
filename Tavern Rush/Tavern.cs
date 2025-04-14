using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Tavern_Rush
{
    internal class Tavern
    {
        private int _money;
        private int _reputation;
        private bool IsOpen;
        private Perk[] Perks;
        private int TavernLevel;


        public Tavern()
        {
            _reputation = 50;
            _money = 20;
            TavernLevel = 1;
            IsOpen = true;
        }

        public void OpenTavern()
        {
            Random random = new Random();
            Product product new Product();
            Warehouse warehouse = new Warehouse(TavernLevel, productInstances);
            List<string> productsInWarehouse = warehouse.GetProductsFromWarehouse();
            Order order = new Order(GenerateCountProducts(TavernLevel), TavernLevel, productsInWarehouse, random);
            UsefulAction usefulAction = new UsefulAction();
            HarmfulAction harmfulAction = new HarmfulAction();
            string[] harmfulActions = harmfulAction.createRandomHarmfulActions(TavernLevel, random);
            string[] usefulActions = usefulAction.CreateActionNames();
            var actions = usefulActions.Concat(harmfulActions).ToArray();
            string[] mixedActionsArray = GameLogic.mixActions(actions, random, "Заказать продукты на склад");
            int[] validCode = GameLogic.ConvertArrayToValidCode(mixedActionsArray);

            while (IsOpen)
            {
                Client client = new Client();
                string[] products = order.CreateOrder();
                //Console.WriteLine("Назовите Вашу таверну: ");
                //string tavernName = Console.ReadLine();
                //Console.WriteLine($"Таверна {tavernName} открыта!\n");
                warehouse.ShowInfoStore();
                ShowTavernInfo();
                Console.WriteLine($"🧍 Клиент: {client.Name}");
                client.SayPhrase();
                Console.Write($"🧾 Заказ: ");

                foreach (var item in products)
                {
                    Console.Write($"{item} ");
                }

                Console.WriteLine("\n");

                for (int i = 0; i < mixedActionsArray.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {mixedActionsArray[i]}");
                }
                Console.WriteLine();
                string orderCode = Console.ReadLine();
                if (GameLogic.isValidCode(orderCode, validCode, client))
                {
                    Console.WriteLine("Спасибо, друг! Держи монету");
                    _money += order.OrderPrice;
                }
                else
                {
                    Console.WriteLine("Мда, даже обслужить нормально не могут. Всего хорошего!");
                    _reputation -= 10;
                }
            }
        }
        private int GenerateCountProducts(int tavernLevel)
        {
            return tavernLevel + 1;
        }

        private void ShowTavernInfo()
        {
            Console.WriteLine($"Ваше золото {_money}");
            Console.WriteLine($"Ваша репутация {_reputation}\n");
        }
    }
}
