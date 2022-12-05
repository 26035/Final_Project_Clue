using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Timers;

namespace FinalProject
{
    static class Timer
    {
        //attributes
        public static int Time = 0;
        static Thread _thread = null;
        //methods
        /// <summary>
        /// used to initialize the timer
        /// </summary>
        public static void Init()
        {
            if(_thread == null)
            {
                _thread = new Thread(CTimer);
                _thread.Start();
            }
        }
        /// <summary>
        /// used to increment the Timer each second
        /// </summary>
        private static void CTimer()
        {
            while (true)
            {
                Time++;
                Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// used to print the timer on the console
        /// </summary>
        public static void PrintTime()
        {
            Console.WriteLine(TimeConversion());
        }
        /// <summary>
        /// used to do the conversion to second in hour, min and second
        /// </summary>
        /// <returns></returns>
        public static string TimeConversion()
        {
            string time = "";
            int hour = Time / 3600;
            int rest = Time % 3600;
            int min = rest / 60;
            rest = rest % 60;
            int second = rest;
            time = time + hour + ":" + min + ":" + second;
            return time;
        }

    }
}
