namespace MessageConsumer
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Initializing: 1 min");
            Console.WriteLine("To exit write: \"stop\"");
            Facade facade = new();

            facade.StartLoop();

            string command = "";
            while (command != "stop")
            {
                command = Console.ReadLine()?.ToLower() ?? string.Empty;
            }
            facade.TokenSrc.Cancel();
            Console.WriteLine("Задача завершена");
        }
    }

}