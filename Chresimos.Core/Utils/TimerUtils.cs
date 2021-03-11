using System;
using System.Timers;

namespace Chresimos.Core.Utils
{
    public static class TimerUtils
    {
        /// <param name="delay">Time until the function get called, in milliseconds.</param>
        public static Timer SetTimeout (Action function, int delay)
        {
            if (delay <= 0)
            {
                function();
                return null;
            }

            var timer = new Timer();
            timer.Interval = delay;
            timer.Elapsed += (sender, args) =>
            {
                function();
                timer.Dispose();
            };

            timer.Start();

            return timer;
        }

        public static Timer SetTimeout <T> (Func<T> function, int delay)
        {
            return SetTimeout((Action) (() => function()), delay);
        }

        /// <param name="interval"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public static Timer SetInterval (Action function, int interval, int delay = 0)
        {
            var timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += (sender, args) => function();

            SetTimeout(timer.Start, delay);
            return timer;
        }

        public static Timer SetInterval <T> (Func<T> function, int interval, int delay = 0)
        {
            return SetInterval((Action) (() => function()), interval, delay);
        }
    }
}