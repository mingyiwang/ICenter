using System;

namespace Core
{

    public sealed class UniqueIds
    {
        public static string NGuid => Guid.NewGuid().ToString("N");
        public static string DGuid => Guid.NewGuid().ToString("");

    }

}