using System.Collections.Generic;

namespace Common.File
{

    public interface IFileStorage
    {
        List<IFileStorageItem> GetItems();
        bool DeleteFile(IFileStorageItem fileItem);
    }

}