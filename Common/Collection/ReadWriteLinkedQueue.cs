using System.Collections.Generic;
using Common.Concurrent;

namespace Common.Collection
{

    public class ReadWriteLinkedQueue<T> : Queue<T>
    {

         private readonly ReadWrite _lock  = new ReadWrite();
         private readonly LinkedList<T> _list;

    }

}