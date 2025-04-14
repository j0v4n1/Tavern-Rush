using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavern_Rush
{
    internal class Perk
    {
        private Dictionary<string, string> _perks = new Dictionary<string, string>()
        {
            {"Быстрая реакция", "Уменьшает время на выполнение каждого заказа на 1 секунду"},
            {"Опытный бармен", "Уменьшает количество ошибок при заказах на 10%" },
            {"Магнит для чаевых","Увеличивает чаевые на 10%" },
            {"Тренировка памяти", "Меньше случайных вредных действий появляется"},
            {"Нанять помощника", "С вероятностью 20% заказ выполнит помощник." }
        };

        public void ShowAllPerks()
        {
            Console.WriteLine("Выберите один из доступных улучшений таверны");
            int dictionaryLength = _perks.Count;
            int i = 1;
            foreach (var item in _perks)
            {
                Console.WriteLine($"{i}. {item.Key} - {item.Value}");
                i++;
            }
        }
    }
}
