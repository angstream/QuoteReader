using QuoteService;
using System;

namespace QuoteClient
{


    class MyQuoteClient
    {
        private IQuoteGenerator generator;
        public MyQuoteClient(IQuoteGenerator generator)
        {
            this.generator = generator;
        }

        public void Run(int min, int max)
        {
            generator.CreateSequence(min, max);            
        }
    }

    class Program
    {

        public static IQuoteGenerator CreateQuoteGenerator()
        {
            return new QuoteGenerator();
        }

        private static void ShowUsageAndQuit()
        {
            Console.WriteLine("usage: QuoteClient 1  for range 240 to 270 or QuoteClient 2 for range 180 to 210");
            Environment.Exit(0);
        }
        private static void Main(String[] args)
        {
            if (args.Length == 0)
            {
                ShowUsageAndQuit();
            }          


            String option = args[0];

            if (option != "1" && option != "2")
            {
                ShowUsageAndQuit();
            }
            
            var client = new MyQuoteClient(CreateQuoteGenerator());
            
            int min = 0;
            int max = 0;

            if (option == "1")
            {
                min = 240;
                max = 270;
            }
            else 
            {
                min = 180;
                max = 210;
            }
            
            client.Run(min, max);
            
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        
    }
}
