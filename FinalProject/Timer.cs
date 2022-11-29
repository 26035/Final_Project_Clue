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

        

    }
}
