namespace Tavern_Rush
{
    internal class Tavern
    {
        private int _money = 20;
        private int _reputation = 50;
        private bool _isOpen = true;
        private int _tavernLevel = 1;


        public void OpenTavern()
        {
            Random random = new Random();
            Warehouse warehouse = new Warehouse(_tavernLevel);
            List<Product> productsInWarehouse = warehouse.GetProductsFromWarehouse();
            Order order = new Order(GenerateCountProducts(_tavernLevel), _tavernLevel, productsInWarehouse, random);
            UsefulAction usefulAction = new UsefulAction();
            HarmfulAction harmfulAction = new HarmfulAction();
            string[] harmfulActions = harmfulAction.CreateRandomHarmfulActions(_tavernLevel, random);
            string[] usefulActions = usefulAction.CreateActionNames();
            var actions = usefulActions.Concat(harmfulActions).ToArray();
            string[] mixedActionsArray = GameLogic.MixActions(actions, random, "Заказать продукты на склад");
            int[] validCode = GameLogic.ConvertArrayToValidCode(mixedActionsArray);

            while (_isOpen)
            {
                Client client = new Client(random);
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

                if (int.TryParse(orderCode, out int parsedOrderCode))
                {
                    if (parsedOrderCode == 7)
                    {
                        warehouse.RefillProducts(_tavernLevel);
                    }
                    else
                    {
                        if (GameLogic.IsValidCode(parsedOrderCode.ToString(), validCode, client))
                        {
                            Console.WriteLine("Спасибо, друг! Держи монету\n");
                            warehouse.UseProductForOrder(order.GetProductsFromOrder());
                            _money += order.OrderPrice;
                            _reputation += 2;
                        }
                        else
                        {
                            Console.WriteLine("Мда, даже обслужить нормально не могут. Всего хорошего!\n");
                            _reputation -= 10;
                            CheckReputation();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Неверный формат ввода! Будьте внимательны, можно растерять всю репутацию");
                    _reputation -= 10;
                    CheckReputation();
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

        private void CheckReputation()
        {
            if (_reputation <= 0)
            {
                _isOpen = false;
                Console.WriteLine("Игра окончена!");
            }
        }
    }
}
