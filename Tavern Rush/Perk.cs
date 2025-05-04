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
    private readonly Dictionary<PerkType, string> _perksNames = new()
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

    public string[] GetAllPerksNames()
    {
      return _perksNames.Select(item => item.Value).ToArray();
    }
    
    public string[] GetAllPerksDescribe()
    {
      return _perksDescribe.Select(item => item.Value).ToArray();
    }
    
  }
}