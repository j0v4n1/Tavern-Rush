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


}