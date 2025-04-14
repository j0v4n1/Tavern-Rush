using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavern_Rush
{
    enum Temperament
    {
        Meticulous,
        LaidBack,
        Balanced,
        HotHeaded,
        Picky
    }
    internal class Client
    {
        private List<string> _names = new List<string>()
        {
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
        };
        private Dictionary<Temperament, string> clientPhrases = new Dictionary<Temperament, string>
        {
            { Temperament.Meticulous, "🔍😐 Я не потерплю промедлений. Сделайте как надо — и быстро." },
            { Temperament.LaidBack, "🍃😎 Да не торопись ты, я тут отдыхаю." },
            { Temperament.Balanced, "🙂👌 Пожалуйста, обслужите меня, как полагается. Этого достаточно." },
            { Temperament.HotHeaded, "🔥😠 Если не получу заказ через пару секунд — ухожу!" },
            { Temperament.Picky, "🧐📋 Надеюсь, ваш повар знает, как подавать стейк нужной прожарки." }
        };

        public string Name { get; private set; }
        private Temperament _temperament;
        private int _timeToServe;
        public int AllowedMistakes { get; private set; }

        public Client()
        {
            Random random = new Random();
            _temperament = GenerateRandomTemperament(random);
            SetServiceParameters(_temperament);
            Name = _names[random.Next(0, _names.Count)];
        }

        private Temperament GenerateRandomTemperament(Random random)
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
                    _timeToServe = 5;
                    AllowedMistakes = 0;
                    break;
                case Temperament.LaidBack:
                    _timeToServe = 9;
                    AllowedMistakes = 3;
                    break;
                case Temperament.Balanced:
                    _timeToServe = 7;
                    AllowedMistakes = 2;
                    break;
                case Temperament.HotHeaded:
                    _timeToServe = 5;
                    AllowedMistakes = 2;
                    break;
                case Temperament.Picky:
                    _timeToServe = 7;
                    AllowedMistakes = 0;
                    break;
                default:
                    break;
            }
        }
        public void SayPhrase()
        {
            Console.WriteLine($"{clientPhrases[_temperament]}");
        }
    }
}
