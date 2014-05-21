using System;
using System.Collections.Generic;
using System.Diagnostics;
using Afterthought;

namespace TimerAmendment
{
    public class TimerAmendment<T> : Amendment<T, T>
    {
        public TimerAmendment()
        {
            Methods
                .Where(m => m.MethodInfo.Name == "GetResult")
                .Before(Timer<T>.LogMethodBefore)
                .After(Timer<T>.LogMethodAfter);
        }
    }

    public static class Timer<T>
    {
        private static readonly Dictionary<T, Stopwatch> _timers = new Dictionary<T, Stopwatch>();

        public static void LogMethodBefore(T instance, string name, object[] ps)
        {
            _timers[instance] = Stopwatch.StartNew();
        }

        public static void LogMethodAfter(T instance, string name, object[] ps)
        {
            var stopwatch = _timers[instance];
            stopwatch.Stop();

            Console.WriteLine("Czas wykonania: " + stopwatch.Elapsed);
        }
    }
}
