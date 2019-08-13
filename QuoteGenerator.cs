using System;
using System.Threading.Tasks;
using System.Threading;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Reactive;


namespace QuoteService
{

    public class MyQuote
    {
        private readonly DateTime timeStamp;
        private readonly string quote;

        public String Quote
        {
            get { return quote; }
        }

        public DateTime TimeStamp
        {
            get { return timeStamp; }
        }

        public MyQuote(string tid)
        {
            quote = tid;
            timeStamp = DateTime.Now;
        }

     

        public override string ToString()
        {
            return String.Format("Quote:     {0}\nTimeStamp: {1}\n", quote, timeStamp.ToString());
        }
    }

    public interface IQuoteGenerator
    {
        void CreateSequence(int min, int max);        
    }

    public class QuoteGenerator: IQuoteGenerator
    {
        static IObserver<MyQuote> observer = Observer.Create<MyQuote>(x => Console.WriteLine(x.ToString()));
        
        private static readonly Random random = new Random();
        private static double GetRandomNumber(double minValue, double maxValue)
        {
            var next = random.NextDouble();

            var rn = minValue + (next * (maxValue - minValue));

            return rn;
        }

        public void CreateSequence(int min, int max)
        {

            var selector = Observable.Interval(TimeSpan.FromSeconds(1))
                    .Select(x => new MyQuote(GetRandomNumber(min, max).ToString("0.00")));


            using (selector.Subscribe(observer))
            {
                Console.WriteLine("Press any key to unsubscribe");
                Console.ReadKey();
            }
        }

    }    
}
