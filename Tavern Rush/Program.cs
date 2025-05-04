namespace Tavern_Rush
{
  internal abstract class Program
  {
    public static void Main()
    {
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      Tavern tavern = new Tavern();
      tavern.StartGame();
    }
  }
}