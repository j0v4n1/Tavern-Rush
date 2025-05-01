namespace Tavern_Rush;

public static class ConsoleHelper
{
  public static int ConvertToInt(string? command)
  {
    if (command == null)
    {
      return 0;
    }

    return int.TryParse(command, out int parsedCommand) ? parsedCommand : 0;
  }

  public static void ShowMessageToContinue()
  {
    Console.WriteLine();
    Console.WriteLine("Нажмите любую клавишу для продолжения...");
    Console.ReadKey();
    Console.WriteLine();
  }
}