using System;

namespace Common
{
    public sealed class Guids
    {
        public static string NGuid => Guid.NewGuid().ToString("N");
        public static string DGuid => Guid.NewGuid().ToString("");

    }

}