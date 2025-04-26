namespace Tavern_Rush
{
    public enum HarmfulActionType
    {
        BeerSpilled,
        Stumble,
        DropTheTray,
        KickTheCauldron,
        SneakADrink,
        FumbleTheOrder,
        ArgueWithAClient,
    }

    internal class HarmfulAction
    {
        private readonly Dictionary<HarmfulActionType, string> _actionsName = new Dictionary<HarmfulActionType, string>()
        {
            { HarmfulActionType.BeerSpilled, "Пролить пиво" },
            { HarmfulActionType.Stumble, "Споткнуться" },
            { HarmfulActionType.DropTheTray, "Уронить поднос" },
            { HarmfulActionType.KickTheCauldron, "Пнуть котёл" },
            { HarmfulActionType.SneakADrink, "Выпить самому" },
            { HarmfulActionType.FumbleTheOrder, "Уронить заказ" },
            { HarmfulActionType.ArgueWithAClient, "Ругаться с посетителем" }
        };

        public string[] CreateRandomHarmfulActions(int tavernLevel, Random random)
        {
            List<string> harmfulActions = new List<string>();
            HarmfulActionType randomAction;
            switch (tavernLevel)
            {
                case 1:
                    for (int i = 1; i <= tavernLevel; i++)
                    {
                        randomAction = RandomizeHarmfulActionType(random);
                        while (harmfulActions.Contains(_actionsName[randomAction]))
                        {
                            randomAction = RandomizeHarmfulActionType(random);
                        }
                        harmfulActions.Add(_actionsName[randomAction]);
                    }
                    return harmfulActions.ToArray();
                case 2:
                    for (int i = 1; i <= tavernLevel; i++)
                    {
                        randomAction = RandomizeHarmfulActionType(random);
                        while (harmfulActions.Contains(_actionsName[randomAction]))
                        {
                            randomAction = RandomizeHarmfulActionType(random);
                        }
                        harmfulActions.Add(_actionsName[randomAction]);
                    }
                    return harmfulActions.ToArray();
                case 3:
                    for (int i = 1; i <= tavernLevel; i++)
                    {
                        randomAction = RandomizeHarmfulActionType(random);
                        while (harmfulActions.Contains(_actionsName[randomAction]))
                        {
                            randomAction = RandomizeHarmfulActionType(random);
                        }
                        harmfulActions.Add(_actionsName[randomAction]);
                    }
                    return harmfulActions.ToArray();
                case 4:
                    for (int i = 1; i <= tavernLevel; i++)
                    {
                        randomAction = RandomizeHarmfulActionType(random);
                        while (harmfulActions.Contains(_actionsName[randomAction]))
                        {
                            randomAction = RandomizeHarmfulActionType(random);
                        }
                        harmfulActions.Add(_actionsName[randomAction]);
                    }
                    return harmfulActions.ToArray();
                case 5:
                    for (int i = 1; i <= tavernLevel; i++)
                    {
                        randomAction = RandomizeHarmfulActionType(random);
                        while (harmfulActions.Contains(_actionsName[randomAction]))
                        {
                            randomAction = RandomizeHarmfulActionType(random);
                        }
                        harmfulActions.Add(_actionsName[randomAction]);
                    }
                    return harmfulActions.ToArray();
                default:
                    return harmfulActions.ToArray();
            }
        }

        private HarmfulActionType RandomizeHarmfulActionType(Random random)
        {
            int maxValue = Enum.GetValues(typeof(HarmfulActionType)).Length;
            int randomIndex = random.Next(0, maxValue);
            return (HarmfulActionType)randomIndex;

        }
    }
}