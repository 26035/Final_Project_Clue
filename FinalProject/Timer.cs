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

        public static int Time = 0;
        static Thread _thread = null;
        
        public static void Init()
        {
            if(_thread == null)
            {
                _thread = new Thread(CTimer);
                _thread.Start();
            }
        }

        private static void CTimer()
        {
            while (true)
            {
                Time++;
                Thread.Sleep(1000);
            }
        }

        public static void PrintTime()
        {
            Console.WriteLine(TimeConversion());
        }
        public static string TimeConversion()
        {
            string time = "";
            int hour = Time / 60;
            int rest = Time % 60;
            int min = rest / 60;
            rest = rest % 60;
            int second = rest;
            time = time + hour + ":" + min + ":" + second;
            return time;
        }

    }
}
