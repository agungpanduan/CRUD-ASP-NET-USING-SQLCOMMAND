using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadExcel.Common.Database
{
    public interface IPagedData<T>
    {
        IPagedData<U> Clone<U>(IList<U> data);
        long GetCurrentPage();
        IList<T> GetData();
        long GetDataCount();
        long GetPageCount();
        long GetPageSize();
    }
}