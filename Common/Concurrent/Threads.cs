﻿using System;
using System.Threading;

namespace Common.Concurrent
{

    public static class Threads
    {

        public static void StartNew(string name, ThreadStart action)
        {
            var thread = new Thread(action)
            {
                Name = name,
                IsBackground = false,
            };
            thread.Start();
        }

        public static void Sleep(int ms)
        {
            try
            {
                Thread.Sleep(ms);
            }
            catch (Exception)
            {
                
            }
        }

    }

}