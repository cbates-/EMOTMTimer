using System;

namespace EMOTM.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}