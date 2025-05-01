namespace Tavern_Rush
{
  internal enum Temperament
  {
    Meticulous,
    LaidBack,
    Balanced,
    HotHeaded,
    Picky
  }

  internal class Client
  {
    private readonly List<string> _names =
    [
      "Сир Седрик Железное Сердце",
      "Леди Элира Лунная Тень",
      "Бром Каменное Львиное Сердце",
      "Талия Гроза Путников",
      "Гаррик Странник",
      "Элдрин Легкая Нога",
      "Мэлис Тень Терновника",
      "Дарий Черный Камень",
      "Селена Ночная Шепотница",
      "Фергус Красный Клинок",
      "Лисандра Огненный Ручей",
      "Дрейк Железный Кулак",
      "Алтея Солнечный Цветок",
      "Родерик Стальной Ветер",
      "Изольда Звездный Кузнец",
      "Орин Темная Вода",
      "Гвенетта Углеродный Камень",
      "Тобиас Вороновый Шепот",
      "Кара Серебряное Пламя",
      "Финниан Штормовой Плащ"
    ];

    private readonly Dictionary<Temperament, string> _clientPhrases = new Dictionary<Temperament, string>
    {
      { Temperament.Meticulous, "🔍😐 Я не потерплю промедлений. Сделайте как надо — и быстро." },
      { Temperament.LaidBack, "🍃😎 Да не торопись ты, я тут отдыхаю." },
      { Temperament.Balanced, "🙂👌 Пожалуйста, обслужите меня, как полагается. Этого достаточно." },
      { Temperament.HotHeaded, "🔥😠 Если не получу заказ через пару секунд — ухожу!" },
      { Temperament.Picky, "🧐📋 Надеюсь, ваш повар знает, как подавать стейк нужной прожарки." }
    };

    public string Name { get; private set; }
    private readonly Temperament _temperament;
    public int TimeToServe { get; private set; }
    public int AllowedMistakes { get; private set; }

    public Client(Random random)
    {
      _temperament = GenerateRandomTemperament(random);
      SetServiceParameters(_temperament);
      Name = _names[random.Next(0, _names.Count)];
    }

    private static Temperament GenerateRandomTemperament(Random random)
    {
      Temperament[] temperaments = (Temperament[])Enum.GetValues(typeof(Temperament));
      int temperamentLength = temperaments.Length;
      int randomTemperamentIndex = random.Next(0, temperamentLength);
      return temperaments[randomTemperamentIndex];
    }

    private void SetServiceParameters(Temperament temperament)
    {
      switch (temperament)
      {
        case Temperament.Meticulous:
          TimeToServe = 7000;
          AllowedMistakes = 0;
          break;
        case Temperament.LaidBack:
          TimeToServe = 11000;
          AllowedMistakes = 3;
          break;
        case Temperament.Balanced:
          TimeToServe = 9000;
          AllowedMistakes = 2;
          break;
        case Temperament.HotHeaded:
          TimeToServe = 7000;
          AllowedMistakes = 2;
          break;
        case Temperament.Picky:
          TimeToServe = 9000;
          AllowedMistakes = 0;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(temperament), temperament, null);
      }
    }

    public void SayPhrase()
    {
      Console.WriteLine($"{_clientPhrases[_temperament]}");
    }

    public static Dictionary<Client, CancellationToken> GenerateClientWithToken(Client client,
      CancellationTokenSource cts)
    {
      Dictionary<Client, CancellationToken> clientWithToken = new Dictionary<Client, CancellationToken>();
      var token = cts.Token;
      clientWithToken.Add(client, token);
      return clientWithToken;
    }
  }
}