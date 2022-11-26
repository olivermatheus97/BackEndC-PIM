using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1;

namespace ConsoleApp1.Interface
{

    /// <summary>
    ///        Interface (contrato) para classes DAO
    /// </summary>
    /// <typeparam name="T"></typeparam>

    interface IDAO<T>
    {
        Boolean Insert(T t);
        Boolean Update(T t);
        Boolean Delete(T t);
        T List(long cpf);
    }
}