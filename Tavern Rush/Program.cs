namespace Tavern_Rush
{
  internal abstract class Program
  {
    public static async Task Main()
    {
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      Tavern tavern = new Tavern();
      await tavern.StartGame();
    }
  }
}