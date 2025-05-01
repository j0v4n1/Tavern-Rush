namespace Tavern_Rush
{
  internal enum PerkType
  {
    QuickReflexes,
    TipMagnet,
    HireAssistant,
    MemoryTraining,
    ExperiencedBartender
  }

  internal class Perk
  {
    private Dictionary<PerkType, string> _perksNames = new()
    {
      { PerkType.QuickReflexes, "Быстрая реакция" },
      { PerkType.TipMagnet, "Магнит для чаевых" },
      { PerkType.HireAssistant, "Нанять помощника" },
      { PerkType.MemoryTraining, "Тренировка памяти" },
      { PerkType.ExperiencedBartender, "Опытный бармен" }
    };

    private readonly Dictionary<PerkType, string> _perksDescribe = new()
    {
      { PerkType.QuickReflexes, "Увеличивает время на выполнение каждого заказа на 2 секунды" },
      { PerkType.TipMagnet, "Увеличивает чаевые на 20%" },
      { PerkType.HireAssistant, "С вероятностью 30% заказ выполнит помощник." },
      { PerkType.MemoryTraining, "С вероятностью 40% убирается одно вредное действие из списка" },
      { PerkType.ExperiencedBartender, "С вероятностью 10% принимается любой порядок действий" }
    };

    public void ShowAllPerks()
    {
      Console.WriteLine("Выберите один из доступных улучшений таверны:");
      int i = 1;
      foreach (KeyValuePair<PerkType, string> item in _perksNames)
      {
        Console.WriteLine($"{i}. {item.Value} - {_perksDescribe[item.Key]}");
        i++;
      }
    }
  }
}