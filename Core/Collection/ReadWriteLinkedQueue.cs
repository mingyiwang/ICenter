using System.Collections.Generic;
using ReadWrite = Core.Concurrent.ReadWrite;

namespace Core.Collection
{

    public class ReadWriteLinkedQueue<T> : Queue<T>
    {

         private readonly ReadWrite _lock  = new ReadWrite();
         private readonly LinkedList<T> _list;

    }

}