namespace Tavern_Rush
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Tavern tavern = new Tavern();
            tavern.OpenTavern();
        }

    }

}