using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021242.DataLayer
{

    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu chung
    /// </summary>
    public interface ICommonDAL<T> where T: class
    {
        IList<T> List(int page, int pageSize, string searchValue);
        IList<T> List();
        T Get(int id);
        
        int Add(T data);
        
        bool Update(T data);
        
        bool Delete(int id);
        
        int Count(string searchValue);
        bool InUsed(int id);
    }
}
