using System;
using System.Threading;

namespace Core.Concurrent
{

    public static class Threads
    {

        public static void Start(string name, ThreadStart action)
        {
            new Thread(action)
            {
                Name = name,
                IsBackground = false
            }.Start();
        }

        public static void Sleep(int milliseconds)
        {
            try
            {
                Thread.Sleep(milliseconds);
            }
            catch (Exception)
            {
                // ignored
            }
        }

    }

}